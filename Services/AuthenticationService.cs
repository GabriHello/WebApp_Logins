using Nome.FormDataModels;
using Nome.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Nome.Services
{
    public class AuthenticationService
    {
        readonly string _connectionString;


        public AuthenticationService()
        {
            _connectionString = ConfigurationManager
                .ConnectionStrings["ConnectionStringLocal"]
                .ConnectionString;
            if (_connectionString == null)
            {
                throw new Exception("Cannot retrieve connection string");
            }


        }

        public bool CheckUserEmail(string email)
        {

            string queryString = "SELECT COUNT(*) FROM users where email = @email;";
            int countEmailInDb;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar);
                command.Parameters["@email"].Value = email;

                connection.Open();
                countEmailInDb = (int)command.ExecuteScalar();

            }

            return countEmailInDb == 0; //se è vero, questa mail inserita è valida per registrare nuovo uer
        }



        public UserModel CreateUser(SignUpFormDataModel formData)
        {
            string queryString2 =
                "INSERT INTO users(email, password) " +
                "OUTPUT INSERTED.id " +
                "values (@email, @password); ";
            string profileQuery = "INSERT INTO Profiles (id) " +
                                      "values(@id) ";

            int newUserId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(queryString2, connection);
                SqlTransaction transaction = connection.BeginTransaction();

                command.Transaction = transaction;
                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar);
                command.Parameters["@email"].Value = formData.Email;

                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar);
                command.Parameters["@password"].Value = formData.Password;


                // TODO: manage exceptions
                try
                {
                    newUserId = (int)command.ExecuteScalar();
                    SqlCommand cmd2 = new SqlCommand(profileQuery, connection);
                    cmd2.Transaction = transaction;
                    cmd2.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    cmd2.Parameters["@id"].Value = newUserId;
                    int insertCount = (int)cmd2.ExecuteNonQuery();

                    if (insertCount == 0)
                    {
                        throw new Exception("Profilo non aggiunto");
                    }

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return null;
                }
                transaction.Commit();
            }

            return new UserModel
            {
                Id = newUserId,
                Email = formData.Email,
                Password = formData.Password
            };
        }


        /// <summary>
        /// This method should return the single object corresponding to the
        /// DB row which has the 
        /// specified email
        /// </summary>
        /// <param name="email">The search key</param>
        /// <returns>Returns the UserModel object or null in case the user is not found </returns>
        /// <exception cref="Exception">In case of inconsisten DB data</exception>
        internal UserModel GetUserByEmail(LoginFormDataModel formData)
        {
            string queryString2 =
                    "SELECT * FROM users " +
                    "WHERE password = @password AND email = @email";

            UserModel userResult = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                SqlCommand command = new SqlCommand(queryString2, connection);

                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar);
                command.Parameters["@email"].Value = formData.Email;

                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar);
                command.Parameters["@password"].Value = formData.Password;

                connection.Open();

                var count = 0;


                // TODO: manage exceptions
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (count > 0)
                            throw new Exception("Data are inconsistent");

                        userResult = new UserModel();

                        userResult.Id = reader.GetInt32(0);
                        userResult.Email = reader.GetString(1);
                        userResult.Password = reader.GetString(2);


                        count++;
                    }

                }
            }

            return userResult;

        }

        public UserProfileModel GetProfileById(int id)
        {
            UserProfileModel profile = null;

            string queryString =
                               "SELECT * FROM Profiles " +
                               "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                command.Parameters["@id"].Value = id;


                // TODO: manage exceptions
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        profile = new UserProfileModel();

                        profile.Id = reader.GetInt32(reader.GetOrdinal("id"));

                        profile.Firstname = reader.IsDBNull(reader.GetOrdinal("firstname")) ?
                            null :
                            reader.GetString(reader.GetOrdinal("firstname"));

                        profile.Lastname = reader.IsDBNull(reader.GetOrdinal("lastname")) ?
                            null :
                            reader.GetString(reader.GetOrdinal("lastname"));

                        profile.BirthDate = reader.IsDBNull(reader.GetOrdinal("birthdate")) ?
                            null as DateTime? :
                            reader.GetDateTime(reader.GetOrdinal("birthdate"));

                        profile.Citizenship = reader.IsDBNull(reader.GetOrdinal("citizenship")) ?
                            null as int? :
                           reader.GetInt32(reader.GetOrdinal("citizenship"));
                    }

                }
            }
            return profile;
        }

        public void UpdateProfileInfo(UserProfileFormDataModel formData, int id)
        {
            var queryString = "update Profiles " +
                "SET " +
                "firstname = @fn, " +
                "lastname = @ln, " +
                "birthdate = @bd, "+
                "citizenship = @zs " +
                "where id=@id";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.Parameters.AddWithValue("@fn", formData.Firstname ?? SqlString.Null);
                cmd.Parameters.AddWithValue("@ln", formData.Lastname ?? SqlString.Null);
                cmd.Parameters.AddWithValue("@bd", formData.BirthDate ?? SqlDateTime.Null );
                cmd.Parameters.AddWithValue("@zs", formData.Citizenship ?? SqlInt32.Null);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteScalar();
            };
        }
    }
}
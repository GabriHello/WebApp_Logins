using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nome.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Citizenship { get; set; }

    }
}
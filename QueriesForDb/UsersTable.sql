create table users(
	id int primary key identity(1,1),
	email nvarchar(50),
	password nvarchar(50)
	);

	drop table users;
	drop table Profiles;
create table Profiles 
		(
		id int primary key identity(1,1) foreign key (id)  references users(id),
		firstname nvarchar(50),
		lastname nvarchar(50),
		birthdate date,
		citizenship int
		)

--Query to create database
create database BookStoreApp

--Table creation query of user
create Table UserTable ( 
	UserId int not null primary key identity(1,1),
	FullName varchar(100),
	EmailId varchar(100), 
	Password varchar(100),
	MobileNumber bigint
);

select * from UserTable;


--Creating stored procedure for the Registration

CREATE or ALTER procedure SPRegistration
	@FullName varchar(100),
	@EmailId varchar(50),
	@Password varchar(100),
	@MobileNumber bigint
AS
	insert into UserTable values (@FullName, @EmailId, @Password, @MobileNumber)
GO

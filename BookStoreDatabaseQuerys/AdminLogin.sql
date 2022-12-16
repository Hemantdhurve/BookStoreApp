--	create table for the Admin
create table AdminTable
	(AdminId int primary key not null identity (1,1) ,
	FullName varchar(100),
	EmailId varchar(100),
	Password varchar(100),
	MobileNumber bigint)

	select * from AdminTable

-- Insert Details

	insert into AdminTable values('Admin','adminBook@gmail.com','adminBook1',8974204201)
-- create store Procedure for the Admin Login

CREATE or ALTER PROCEDURE SPAdminLogin
(	@EmailId varchar(100),
	@Password varchar(100)
)
as
	begin
		select * from AdminTable where EmailId=@EmailId and Password = @Password;
	end

--Creating stored procedure for the Login

CREATE or ALTER procedure SPLogin
	@EmailId varchar(50),
	@Password varchar(100)
AS
BEGIN
	select * from UserTable where EmailId=@EmailId and Password=@Password
END
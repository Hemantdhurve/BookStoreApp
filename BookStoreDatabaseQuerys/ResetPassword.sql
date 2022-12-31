--Creating stored procedure for the Reset password

CREATE or ALTER procedure SPResetPassword
	@EmailId varchar(100),
	@Password varchar(100)
AS
	BEGIN	
		Update UserTable Set Password=@Password where EmailId=@EmailId
	END

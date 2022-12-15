--Creating stored procedure for the Forget password

CREATE or ALTER PROCEDURE SPForgotPass
	@EmailId varchar(100)
	AS
BEGIN
	SELECT * from UserTable where EmailId=@EmailId
END;
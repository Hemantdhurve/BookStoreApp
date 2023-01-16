-- creating stored procedure for the Retrive Customer Details for the front end API integration

create or alter procedure SPGetCustDetails @UserId int
as
	begin	
		select UserId,FullName,MobileNumber from UserTable where UserId=@UserId
	end;

exec SPGetCustDetails @userId=9;
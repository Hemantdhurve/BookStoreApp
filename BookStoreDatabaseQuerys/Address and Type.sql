--Create Type table 

create table AddressType ( TypeId int primary key not null identity(1,1), Addtype varchar(50))

--Create Table for the Address
--we need to create 1st Type table or else AddressTable throws error
create table AddressTable
(	AddressId int primary key identity(1,1),
	UserId int foreign key (UserId) references UserTable(UserId),
	TypeId int foreign key (TypeId) references AddressType(TypeId),
	Address varchar(500),
	City varchar(100),
	State varchar(100)
);

select * from AddressType;
select * from AddressTable;

--inserting values in AddressType table so that each type get a particular unique Id 
--to access while using radio button

insert into AddressType values('Home');
insert into AddressType values('Work');
insert into AddressType values('Other');


--Create stored procedure to Add Address

create procedure SPAddAddress
(	@UserId int,@TypeId int,@Address varchar(500),@City varchar(100),@State varchar(100)	)
as
	begin
		insert into AddressTable values (@UserId,@TypeId,@Address,@City,@State)
	end

--  Create stored procedure for the DELETE Address

create procedure SPDeleteAddress (@AddressId int)
as
	begin
		delete from AddressTable where AddressId=@AddressId
	end

-- create stored procedure for the Retrive All Address

create or alter procedure SPRetriveAddress (@UserId int)
as
	begin
		select u.FullName,u.MobileNumber,u.UserId,a.AddressId,a.TypeId,a.Address,a.City,a.State
		from AddressTable a full join UserTable u on a.UserId=u.UserId
		where a.UserId=@UserId
	end

exec SPRetriveAddress @UserId=3;


--Create stored procedure for Update Address

create procedure SPUpdateAddress
(	@AddressId int,@TypeId int,@Address varchar(500),@City varchar(100),@State varchar(100) )
as
	begin
		update AddressTable set TypeId=@TypeId,Address=@Address,City=@City,State=@State
			where AddressId=@AddressId
	end

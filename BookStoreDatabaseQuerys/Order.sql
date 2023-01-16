--create table for the Order

create table OrdersTable
(	OrderId int identity (1,1) primary key,
	UserId int foreign key (UserId) references UserTable(UserId),
	BookId int foreign key (BookId) references BookTable(BookId),
	AddressId int foreign key references AddressTable(AddressId),
	OrderQuantity int ,
	TotalPrice int,
	TotalDiscountedPrice int,
	OrderPlacedDate date
);

select * from OrdersTable;
drop table OrderTable

--Create stored procedure for Add Order

create or alter procedure spAddOrder1
(	
	@UserId int,
	@BookId int,
	@AddressId int
)
	as
		declare @TotalPrice int;
		declare @OrderQuantity int;
		declare @TotalDiscountedPrice int;
			begin
				set @TotalPrice = (select DiscountedPrice from BookTable where BookId = @BookId);
				set @OrderQuantity = (select BookQuantity from CartTable where BookId = @BookId); 
				set @TotalDiscountedPrice=(select DiscountedPrice from BookTable where BookId=@BookId);	
				set @TotalPrice = @OrderQuantity*@TotalPrice;
					begin
						insert into OrdersTable values(@UserId,@BookId,@AddressId,@OrderQuantity,@TotalPrice,@TotalDiscountedPrice,GETDATE());
					end
					begin
						update BookTable set BookQuantity = (BookQuantity - @OrderQuantity) where BookId = @BookId;
					end
					begin
						delete from CartTable where BookId = @BookId and UserId = @UserId;
					end
			end

exec spAddOrder1 @UserId=3,@BookId=3,@AddressId=64;
select * from OrdersTable;


--insert into OrderTable values(3,6,1,4,5,10,50,GETDATE());

select * from CartTable
select * from BookTable
select * from AddressTable


--Create stored procedure for Delete Order

create procedure SPDeleteOrder (@OrderId int)
as
	begin
		delete from OrdersTable where OrderId=@OrderId
	end

select * from BookTable
-- Create stored Procedure for the Retrive Order All

create or alter procedure SPRetriveOrder (@UserId int)
as
	begin
		select b.BookTitle,b.Author,b.Image, o.OrderId,o.UserId,o.BookId,o.AddressId,o.OrderQuantity,o.TotalPrice,o.TotalDiscountedPrice,o.OrderPlacedDate
		from OrdersTable o inner join BookTable b on b.BookId=o.BookId
		where o.UserId=@UserId
	end

exec SPRetriveOrder @UserId=3;
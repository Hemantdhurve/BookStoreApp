--create table for the Order

create table OrderTable
(	OrderId int identity (1,1) primary key,
	UserId int foreign key (UserId) references UserTable(UserId),
	BookId int foreign key (BookId) references BookTable(BookId),
	CartId int foreign key references CartTable(CartId),
	AddressId int foreign key (AddressId) references AddressTable(AddressId),
	OrderQuantity int,
	TotalPrice int,
	TotalDiscountedPrice int,
	OrderPlacedDate date
);

select * from OrderTable;
drop table OrderTable

--Create stored procedure for Add Order

create or alter procedure SPAddOrder 
(	@UserId int,
	@BookId int,
	@cartId int,
	@AddressId int
)
as
	declare @OrderQuantity int;
	declare @TotalPrice int;
	declare @TotalDiscountedPrice int;	
	declare @TotalAmount int;
	declare @BookTotalQuantity int;
	begin 
		--if (exists (select * from CartTable where CartId=@cartId))
		--	begin
				set @OrderQuantity=(select BookQuantity from CartTable where CartId=@cartId);
				set @BookTotalQuantity=(select BookQuantity from BookTable b inner join CartTable c on b.BookId=c.BookId where c.cartId=@cartId);
				set @TotalPrice=(select ActualPrice from BookTable where BookId=@BookId);
				set @TotalDiscountedPrice=(select DiscountedPrice from BookTable where BookId=@BookId);				 
				set @TotalAmount=@TotalDiscountedPrice*@OrderQuantity;	
				
					if(@OrderQuantity <= @BookTotalQuantity)
						begin
							insert into OrderTable values (@UserId,@BookId,@CartId,@AddressId,@OrderQuantity,@TotalAmount,@TotalDiscountedPrice,GETDATE())
						
							begin
								update CartTable set BookQuantity=BookQuantity-@OrderQuantity where BookId=@BookId
							end
							begin
								delete from CartTable where BookId=@BookId
							end
							print 'Executed stored procedure'
						end						
					else
						begin
							print 'Please Update Quantity of Book'
						end
			
	end

	
exec SPAddOrder @UserId=3,@BookId=7,@CartId=6,@AddressId=4;
select * from OrderTable;

--insert into OrderTable values(3,6,1,4,5,10,50,GETDATE());

select * from CartTable
select * from BookTable
select * from AddressTable


--Create stored procedure for Delete Order

create procedure SPDeleteOrder (@OrderId int)
as
	begin
		delete from OrderTable where OrderId=@OrderId
	end

select * from BookTable
-- Create stored Procedure for the Retrive Order All

create or alter procedure SPRetriveOrder (@UserId int)
as
	begin
		select b.BookTitle,b.Author,b.Image, o.OrderId,o.UserId,o.BookId,o.CartId,o.AddressId,o.OrderQuantity,o.TotalPrice,o.TotalDiscountedPrice,o.OrderPlacedDate
		from OrderTable o inner join BookTable b on b.BookId=o.BookId
		where o.UserId=@UserId
	end

exec SPRetriveOrder @UserId=3;
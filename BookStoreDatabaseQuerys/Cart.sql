--Create Table for the Cart  
--Take Foreign key as BookId and UserId and Primary key as CartId

create table CartTable
(	CartId int primary key not null identity(1,1),
	BookId int foreign key (BookId) references BookTable(BookId),
	UserId int foreign key (UserId) references UserTable(UserId),
	BookQuantity int default 1
)

select * from CartTable;


--Create stored Procedure for the Add to cart

create procedure SPCartAdition
(	@BookId int,
	@UserId int,
	@BookQuantity int
)
as
	begin
		if not exists (select * from CartTable where BookId=@BookId and UserId=@UserId)
			begin
				insert into CartTable(BookId,UserId) values (@BookId,@UserId)
			end
	end


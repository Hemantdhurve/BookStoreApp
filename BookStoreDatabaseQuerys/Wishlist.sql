--Create table for the Wishlist

create table WishTable 
(	WishlistId int primary key not null identity(1,1),
	BookId int foreign key (BookId) references BookTable(BookId),
	UserId int foreign key (UserId) references UserTable(UserId)
)

select * from WishTable;

-- Create stored procedure for the Add to Wishlist

create or alter procedure SPAddWishlist
(	@BookId int,@UserId int	)
as
	begin
		if not exists(select * from WishTable where BookId=@BookId and UserId=@UserId )
		begin
			insert into WishTable(BookId,UserId) values(@BookId,@UserId)
		end
	end


--Create stored procedure for Delete Wishlist

create procedure SPDeleteWishlist @WishlistId int
as
	begin 
		delete from WishTable where WishlistId=@WishlistId
	end

--Create stored procedure for Retrive All Wishlist
--performing join on bookTable and WishTable

create or alter procedure SPRetriveAllWishlist (@UserId int)
as
	begin 
		select w.WishlistId,w.BookId,w.UserId,b.BookTitle,b.Author,b.DiscountedPrice,b.ActualPrice,b.Image 
		from WishTable w full join BookTable b on w.BookId=b.BookId
		where w.UserId=@UserId
	end

	--Executing Procedure for a particular UserId
exec SPRetriveAllWishlist @UserId=3;
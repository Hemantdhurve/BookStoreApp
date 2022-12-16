--create Table for the book

create table BookTable
(   BookId int primary key not null identity(1,1),
	BookTitle varchar(100) not null,
	Author varchar(100),
	Rating float,
	RatedCount int,
	DiscountedPrice int not null,
	ActualPrice int not null,
	Description varchar(max) not null,
	BooksQuantity int not null,
	Image varchar(max) not null
);

select * from BookTable;


-- Create store procedure for the addition of book
 
 create or alter procedure SPBookAddition
(	@BookTitle varchar(100),
	@Author varchar(100),
	@Rating float,
	@RatedCount int,
	@DiscountedPrice int,
	@ActualPrice int,
	@Description varchar(max),
	@BookQuantity int,
	@Image varchar(max)
)
as
	begin
		insert into BookTable values
		(	@BookTitle,
			@Author,
			@Rating,
			@RatedCount,
			@DiscountedPrice,
			@ActualPrice,
			@Description,
			@BookQuantity,
			@Image
		)
	end

-- Create Store Procedure for the Retrive Book By Id

create or alter procedure SPRetriveBookById
(	@BookId int  )
as
	begin
		select * from BookTable where @BookId=BookId
	end


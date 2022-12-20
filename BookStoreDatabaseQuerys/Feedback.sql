--create table for the Feedback

create table FeedbackTable
(	FeedbackId int identity(1,1) primary key,
	UserId int foreign key (UserId) references UserTable(UserId),
	BookId int foreign key (BookId) references BookTable(BookId),
	Rating float,
	Comment varchar(max)
);

select * from FeedbackTable;

--Create stored procedure for Add Feedback

create procedure SPAddFeedback 
(	@UserId int,@BookId int,@Rating float,@Comment varchar(max)  )
as
	begin
		insert into FeedbackTable (UserId,BookId,Rating,Comment) 
			   values(@UserId,@BookId,@Rating,@Comment)
	end

--Create stored procedure for the Retrive all Feedback

create procedure SPRetriveFeedback (@BookId int)
as
	begin
		select u.FullName,f.FeedbackId,f.UserId,f.BookId,f.Rating,f.Comment from FeedbackTable f
		inner join UserTable u on f.UserId=u.UserId where BookId=@BookId
	end

	exec SPRetriveFeedback @BookId=3
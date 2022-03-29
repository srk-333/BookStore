/**** Create DataBase ****/
Create Database BookStore;

-- Use BookStore Database
Use BookStore;

---- Create User Table ----
Create Table Users
(
	UserId int Identity(1,1) primary key,
	FullName Varchar(Max) not null,
	Email varchar(Max) not null,
	Password varchar(Max) not null,
	MobileNumber varchar(30) 
);

/******* Stored Procedures For User Api ********/
-- Procedure for User Registration --
create or alter Proc UserRegister
(
	@FullName Varchar(Max),
	@Email varchar(Max),
	@Password varchar(Max),
	@MobileNumber varchar(30) 
)
as
begin
	if( select UserId from Users where Email=@Email) is not null
		begin 
			select 1
		end
	else
		begin
			Insert Into Users (FullName, Email, Password, MobileNumber)
			Values (@FullName, @Email, @Password,@MobileNumber);
		end
End;

-- Procedure For User Login --
create or alter Proc UserLogin
(
	@Email varchar(Max),
	@Password varchar(Max)
)
as
begin
	select * from Users
	where
		Email = @Email
		and
		Password = @Password
End;

-- Procedure For Forgot Password --
create or alter Proc UserForgotPassword
(
	@Email varchar(Max)
)
as
begin
	Update Users 
	set 
		Password ='Null'
	where 
		Email = @Email;
    select * from Users where Email = @Email;
End;

-- Procedure For Reset Password --
CREATE or ALTER PROC UserResetPassword
(
	@Email varchar(Max),
	@Password varchar(Max)
)
AS
BEGIN
	UPDATE Users 
	SET 
		Password = @Password 
	WHERE 
		Email = @Email;
End;

/********* Create Book Table ********/
create table Books
(
	bookId int Identity(1,1) PRIMARY KEY,
	bookName varchar(Max) not null,
	authorName varchar(250) not null,
    rating int,   
	totalRating int,
	discountPrice Decimal,
	originalPrice Decimal,
	description varchar(Max) not null,
	bookImage varchar(250),
	BookCount int not null
);

/****** Stored Procedure For Books Table ******/
-- Procedure To Add Book
create or alter proc AddBook
(
	@bookName varchar(Max),
	@authorName varchar(250),
	@rating int,
	@totalRating int,
	@discountPrice Decimal,
	@originalPrice Decimal,
	@description varchar(Max),
	@bookImage varchar(250),
	@bookCount int
)
as
BEGIN
	Insert into Books (bookName, authorName, rating, totalRating, 
	discountPrice, originalPrice, description, bookImage, BookCount)
	values (@bookName, @authorName, @rating, @totalRating, @discountPrice,
	@originalPrice, @description, @bookImage, @bookCount);
End;

-- Procedure To Update Book --
create or alter proc UpdateBook
(
    @bookId int,
	@bookName varchar(Max),
	@authorName varchar(250),
	@rating int,
	@totalRating int,
	@discountPrice Decimal,
	@originalPrice Decimal,
	@description varchar(Max),
	@bookImage varchar(250),
	@bookCount int
)
as
BEGIN
   Update Books set bookName = @bookName, 
					authorName = @authorName,
					rating = @rating, 
					totalRating = @totalRating, 
					discountPrice= @discountPrice,
					originalPrice = @originalPrice,
					description = @description,
					bookImage =@bookImage,
					BookCount = @bookCount
				where 
					bookId = @bookId;
End;

-- Procedure To Delete a Book
create or alter proc DeleteBook
(
    @bookId int
)
as
BEGIN
	Delete Books 
		where 
		bookId = @bookId;
End;

-- Procedure To Get a Book by Its BookId
create or alter proc GetBookByBookId
(
    @bookId int
)
as
BEGIN
	select * from Books
	where
		bookId = @bookId;
End;

-- Procedure To Get All Book From Table
create or alter proc GetAllBook
as
BEGIN
	select * from Books;
End;

/******* Create Cart Table *******/
create Table Carts
(
	CartId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Quantity INT default 1,
	UserId INT FOREIGN KEY REFERENCES Users(UserId) on delete no action,
	BookId INT FOREIGN KEY REFERENCES Books(bookId) on delete no action
);

/******* Stored Procedure For Cart Table ********/
-- Procedure To Add In Cart
create or alter Proc AddCart
(
	@Quantity int,
	@UserId int,
	@BookId int
)
as
BEGIN
	if(Exists (select * from Books where bookId = @BookId))
	begin
		Insert Into Carts (Quantity, UserId, BookId)
		Values (@Quantity, @UserId, @BookId);
	end
	else
	begin
		select 1
	end			 
End;

-- Procedure To Delete from Cart
create or alter Proc DeleteCart
(
	@CartId int,
	@UserId int
)
as
BEGIN
	Delete Carts 
	where CartId = @CartId
	      and
	      UserId = @UserId;; 
End;

-- Procedure To Update Cart
create or alter Proc UpdateCart
(
	@Quantity int,
	@BookId int,
	@UserId int,
	@CartId int
)
as
BEGIN
	update Carts 
	set BookId = @BookId,
	Userid = @UserId,
	Quantity = @Quantity 
	where CartId = @CartId;
End;

-- Procedure To Get rows from Cart by UserId
create or alter Proc GetCartbyUser
(
	@UserId int
)
as
BEGIN
	Select CartId, Quantity, UserId,c.BookId,
	bookName, authorName, discountPrice, originalPrice, bookImage
	from Carts c
	join Books b on
	c.BookId = b.bookId
	where
	UserId = @UserId;
End;

/******* Create WishList Table *******/
create Table Wishlist
(
	WishlistId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId) on delete no action,
	bookId INT NOT NULL FOREIGN KEY REFERENCES Books(bookId) on delete no action
);


/******* Stored Procedure For Wishlist Table ********/
-- Procedure To Add In Wishlist
create or alter proc AddInWishlist
(
	@UserId int,
	@BookId int
)
as
BEGIN
	If Exists (Select * from Wishlist where UserId = @UserId and bookId = @BookId)
		begin 
			select 2;
		end
	Else
		begin 
			if Exists (select * from Books where bookId = @BookId)
				begin
					Insert into Wishlist(UserId, bookId)
					values (@UserId , @BookId);
				end
			else
				begin
					select 1;
				end
		end
End;

-- Procedure To Delete from Wishlist
create or alter proc DeleteFromWishlist
(
	@WishlistId int,
	@UserId int
)
as
BEGIN 
	Delete Wishlist 
	where WishlistId = @WishlistId
		  and
		  UserId = @UserId;
End;	 

-- Procedure To Get Records from Wishlist
create or alter proc GetAllRecordsFromWishlist
(
	@UserId int
)
as
BEGIN
	select w.WishlistId,w.UserId,w.bookId,
	b.bookName,b.authorName,b.discountPrice,b.originalPrice,
	b.bookImage
	from Wishlist w
	Inner join Books b
	on w.bookId = b.bookId
	where 
		UserId = @UserId;
END;

/******* Create Address Type Table *******/
create Table AddressType
(
	TypeId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	TypeName varchar(255) not null
);

--Insert fix Record for AddressType
Insert into AddressType
values('Home'),('Office'),('Other');

/******* Create Address Table *******/
create Table AddressTab
(
	AddressId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Address varchar(max) not null,
	City varchar(100) not null,
	State varchar(100) not null,
	TypeId int not null 
	FOREIGN KEY (TypeId) REFERENCES AddressType(TypeId),
	UserId INT NOT NULL
	FOREIGN KEY (UserId) REFERENCES Users(UserId),
);

/******* Stored Procedure For Address Table ********/
-- Procedure To Add Address
create or alter proc AddAddress
(
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
)
as
BEGIN
	If Exists (select * from AddressType where TypeId = @TypeId)
		begin
			Insert into AddressTab 
			values(@Address, @City, @State, @TypeId, @UserId);
		end
	Else
		begin
			select 2
		end
End;

-- Procedure To Update Address
create or alter proc UpdateAddress
(
	@AddressId int,
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
)
as
BEGIN
	If Exists (select * from AddressType where TypeId = @TypeId)
		begin
			Update AddressTab set
			Address = @Address, City = @City,
			State = @State , TypeId = @TypeId,
			UserId = @UserId
			where
				AddressId = @AddressId
		end
	Else
		begin
			select 2
		end
End;

-- Procedure To Delete a Address
create or alter Proc DeleteAddress
(
	@AddressId int,
	@UserId int
)
as
BEGIN
	Delete AddressTab
	where 
		AddressId = @AddressId 
	and
		UserId = @UserId;
End;

-- Procedure To Get All Address
create or alter Proc GetAllAddress
(
	@UserId int
)
as
BEGIN
	Select Address, City, State,a1.UserId, a2.TypeId
	from AddressTab a1
    Inner join AddressType a2 on a2.TypeId = a1.TypeId 
	where 
	UserId = @UserId;
END;

/******* Create Feedback Table *******/
create Table FeedbackTab
(
	FeedbackId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Comment varchar(max) not null,
	Rating int not null,
	bookId int not null 
	FOREIGN KEY (bookId) REFERENCES Books(bookId),
	UserId INT NOT NULL
	FOREIGN KEY (UserId) REFERENCES Users(UserId),
);

/******* Stored Procedures For Feedback Table *******/
-- Procedure to Add Feedback ---
create or alter Proc AddFeedback
(
	@Comment varchar(max),
	@Rating int,
	@BookId int,
	@UserId int
)
as
Declare @AverageRating int;
BEGIN
	IF (EXISTS(SELECT * FROM FeedbackTab WHERE bookId = @BookId and UserId=@UserId))
		select 1;
	Else
	Begin
		IF (EXISTS(SELECT * FROM Books WHERE bookId = @BookId))
		Begin  select * from FeedbackTab
			Begin try
				Begin transaction
					Insert into FeedbackTab(Comment, Rating, bookId, UserId) values(@Comment, @Rating, @BookId, @UserId);		
					set @AverageRating = (Select AVG(Rating) from FeedbackTab where bookId = @BookId);
					Update Books set rating = @AverageRating,  totalRating = totalRating + 1 
								 where  bookId = @BookId;
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		End
		Else
		Begin
			Select 2; 
		End
	End
END;

-- Procedure to Delete Feedback ---
create or alter Proc DeleteFeedback
(
	@FeedbackId int,
	@UserId int
)
as
BEGIN
	Delete FeedbackTab
		where
			FeedbackId = @FeedbackId
			and
			UserId = @UserId;
END;

-- Procedure to Get All Feedback ---
create or alter Proc GetAllFeedback
(
	@BookId int
)
as
BEGIN
	Select FeedbackId, Comment, Rating, bookId, u.FullName
	From Users u
	Inner Join FeedbackTab f
	on f.UserId = u.UserId
	where
	 BookId = @BookId;
END;

-- Procedure to Update Feedback ---
create or alter Proc UpdateFeedback
(
	@Comment varchar(max),
	@Rating int,
	@BookId int,
	@FeedbackId int,
	@UserId int
)
as
Declare @AverageRating int;
BEGIN
	IF (EXISTS(SELECT * FROM FeedbackTab WHERE bookId = @BookId and UserId=@UserId))
		select 1;
	Else
	Begin
		IF (EXISTS(SELECT * FROM Books WHERE bookId = @BookId))
		Begin
			Begin try
				Begin transaction
					Update FeedbackTab set Comment = @Comment, Rating = @Rating, UserId = @UserId, bookId = @BookId
									where FeedbackId = @FeedbackId;	
					select @AverageRating = AVG(Rating) from FeedbackTab 
									where bookId = @BookId;
					Update Books set rating = @AverageRating,  totalRating = totalRating+1 
								    where bookId = @BookId;
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		End
		Else
		Begin
			Select 2; 
		End
	End
END;

select * from Users
select * from Books
select * from FeedbackTab

Exec AddFeedback
@Comment = 'Good Book',
@Rating = 4,
@BookId = 4,
@UserId = 3;
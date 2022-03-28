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
	Select CartId, Quantity, UserId,
	bookName, authorName, discountPrice, originalPrice, bookImage
	from Carts c
	join Books b on
	c.BookId = b.bookId
	where
	UserId = @UserId;
End;
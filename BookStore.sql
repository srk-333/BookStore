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


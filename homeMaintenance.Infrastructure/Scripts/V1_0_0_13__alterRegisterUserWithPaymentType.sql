BEGIN TRANSACTION;
GO

ALTER PROCEDURE InsertUser 
@FullName                     NVARCHAR(50), 
@PhoneNumber                  NVARCHAR(50), 
@Email                        NVARCHAR(50), 
@Password                     NVARCHAR(64), 
@City                         NVARCHAR(50), 
@UserRole                     NVARCHAR(50), 
@Experience                   FLOAT, 
@Price                        FLOAT,
@BirthDate                    DATETIME,
@PositionId					  UNIQUEIDENTIFIER,
@Avatar						  NVARCHAR(MAX),
@PaymentType				  INT
AS 
BEGIN 
SET NOCOUNT ON 

INSERT INTO dbo.Users (FullName, PhoneNumber, Email, [Password], City, UserRole, CreatedAt, Experience, Price, BirthDate, PositionId, Avatar, PaymentType) VALUES 
(@FullName, @PhoneNumber, @Email, @Password, @City, @UserRole, GETUTCDATE(), @Experience, @Price, @BirthDate, @PositionId, @Avatar, @PaymentType);

END
GO;

COMMIT;
GO;
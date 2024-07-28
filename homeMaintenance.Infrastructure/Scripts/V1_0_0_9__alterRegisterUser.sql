BEGIN TRANSACTION;
GO

EXEC sp_rename 'dbo.Users.Address', 'City', 'COLUMN';
EXEC sp_rename 'dbo.Users.Image', 'Avatar', 'COLUMN';

GO;

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
@Avatar						  NVARCHAR(MAX)
AS 
BEGIN 
SET NOCOUNT ON 

INSERT INTO dbo.Users (FullName, PhoneNumber, Email, [Password], City, UserRole, CreatedAt, Experience, Price, BirthDate, PositionId, Avatar) VALUES 
(@FullName, @PhoneNumber, @Email, @Password, @City, @UserRole, GETUTCDATE(), @Experience, @Price, @BirthDate, @PositionId, @Avatar);

END
GO;

COMMIT;
GO;
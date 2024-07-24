BEGIN TRANSACTION;
GO

ALTER TABLE dbo.UserRoles
DROP COLUMN IsDeleted;

INSERT INTO dbo.UserRoles (Id, RoleName) VALUES (1, 'Customer'), (2, 'Employee as individual'), (3, 'Employee as business');

EXEC sp_rename 'dbo.Users.City', 'Address', 'COLUMN';

ALTER TABLE dbo.Users 
ADD CONSTRAINT DF_IsDeleted DEFAULT 0 FOR IsDeleted;

ALTER TABLE dbo.Users 
ADD BirthDate DATETIME NOT NULL;
GO;

CREATE PROCEDURE InsertUser 
@FullName                     NVARCHAR(50), 
@PhoneNumber                  NVARCHAR(50), 
@Email                        NVARCHAR(50), 
@Password                     NVARCHAR(50), 
@Address                      NVARCHAR(50), 
@UserRole                     NVARCHAR(50), 
@Experience                   FLOAT, 
@Price                        FLOAT,
@BirthDate                    DATETIME,
@PositionId					  UNIQUEIDENTIFIER
AS 
BEGIN 
SET NOCOUNT ON 

INSERT INTO dbo.Users (FullName, PhoneNumber, Email, [Password], [Address], UserRole, CreatedAt, Experience, Price, BirthDate, PositionId) VALUES 
(@FullName, @PhoneNumber, @Email, @Password, @Address, @UserRole, GETUTCDATE(), @Experience, @Price, @BirthDate, @PositionId);

END
GO;

COMMIT;
GO;
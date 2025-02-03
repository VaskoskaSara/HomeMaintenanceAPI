﻿BEGIN TRANSACTION;
GO

ALTER TABLE dbo.Users
ADD PositionId UNIQUEIDENTIFIER NOT NULL;

ALTER TABLE dbo.Users
ADD CONSTRAINT FK_Users_Positions FOREIGN KEY (PositionId) REFERENCES dbo.Positions(Id);

ALTER TABLE dbo.UserRoles
DROP COLUMN IsDeleted;

INSERT INTO dbo.UserRoles (Id, RoleName) VALUES (1, 'Customer'), (2, 'Employee as individual'), (3, 'Employee as business');

ALTER TABLE dbo.Users 
ADD CONSTRAINT DF_IsDeleted DEFAULT 0 FOR IsDeleted;

ALTER TABLE dbo.Users 
ADD BirthDate DATETIME NOT NULL;
GO;

CREATE OR ALTER PROCEDURE InsertUser 
@FullName                     NVARCHAR(50), 
@PhoneNumber                  NVARCHAR(50), 
@Email                        NVARCHAR(50), 
@Password                     NVARCHAR(64), 
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

END; GO;
GO;

COMMIT;
GO;
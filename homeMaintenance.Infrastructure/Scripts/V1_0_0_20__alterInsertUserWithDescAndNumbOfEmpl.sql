﻿BEGIN TRANSACTION;
GO

ALTER TABLE dbo.Users
ADD NumberOfEmployees INT NULL;

ALTER TABLE dbo.Users
ADD [Description] VARCHAR(MAX) NULL;

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
@Avatar						  NVARCHAR(MAX),
@PaymentType				  INT,
@NumberOfEmployees			  INT,
@Description				  NVARCHAR(MAX),
@Address                      NVARCHAR(MAX),
@NewId                        UNIQUEIDENTIFIER OUTPUT
AS 
BEGIN 
    SET NOCOUNT ON;

    SET @NewId = NEWID();

    INSERT INTO dbo.Users 
    (Id, FullName, PhoneNumber, Email, [Password], City, UserRole, CreatedAt, Experience, Price, BirthDate, PositionId, Avatar, PaymentType, NumberOfEmployees, [Description], [Address]) 
    VALUES 
    (@NewId, @FullName, @PhoneNumber, @Email, @Password, @City, @UserRole, GETUTCDATE(), @Experience, @Price, @BirthDate, @PositionId, @Avatar, @PaymentType, @NumberOfEmployees, @Description, @Address);
    
    SELECT @NewId AS NewUserId;
END; GO;
GO;

COMMIT;
GO;
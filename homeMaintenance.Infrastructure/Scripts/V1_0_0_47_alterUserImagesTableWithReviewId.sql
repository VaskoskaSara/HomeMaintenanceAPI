﻿BEGIN TRANSACTION;
GO

ALTER TABLE dbo.UserImages
ADD CONSTRAINT FK_UserImages_Reviews FOREIGN KEY (ReviewId) REFERENCES dbo.Reviews(Id);
GO;

  ALTER PROCEDURE [dbo].[InsertReview]
  @Comment VARCHAR(MAX),
  @UserId UNIQUEIDENTIFIER, 
  @Rating INT,
  @EmployeeId UNIQUEIDENTIFIER,
  @UserPaymentId VARCHAR(MAX),
  @NewId         UNIQUEIDENTIFIER OUTPUT
  AS 
  BEGIN
   SET NOCOUNT ON;

   SET @NewId = NEWID();

   INSERT INTO dbo.Reviews(Id, Comment, UserId, Rating, EmployeeId, UserPaymentId, CreatedAt)
   VALUES (@NewId, @Comment, @UserId, @Rating, @EmployeeId, @UserPaymentId, GETDATE())

    SELECT @NewId;
  END;
  GO;

  
CREATE OR ALTER PROCEDURE InsertImages 
    @Image NVARCHAR(100),
    @ImageOrigin INT,
    @UserId UNIQUEIDENTIFIER,
	@EmployeeId UNIQUEIDENTIFIER NULL,
    @ReviewId UNIQUEIDENTIFIER NULL
AS 
BEGIN 
    SET NOCOUNT ON; 

    BEGIN TRY
        INSERT INTO dbo.UserImages (Id, Image, ImageOrigin, [UserId], EmployeeId, ReviewId) 
        VALUES (NEWID(), @Image, @ImageOrigin, @UserId, @EmployeeId, @ReviewId);
    END 
    
    TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END
    CATCH
END;
GO;

COMMIT;
GO;

BEGIN TRANSACTION;

COMMIT;
GO 

CREATE OR ALTER PROCEDURE InsertImages 
    @Image NVARCHAR(100),
    @ImageOrigin INT,
    @UserId UNIQUEIDENTIFIER
AS 
BEGIN 
    SET NOCOUNT ON; 

    BEGIN TRY
        INSERT INTO dbo.UserImages (Id, Image, ImageOrigin, [UserId]) 
        VALUES (NEWID(), @Image, @ImageOrigin, @UserId);
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END;
GO 

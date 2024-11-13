BEGIN TRANSACTION;
GO

ALTER TABLE [dbo].[UserImages] ADD EmployeeId UNIQUEIDENTIFIER 
GO;

ALTER PROCEDURE GetBookingsByUser @Id UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT u.Id AS EmployeeId
		,u.FullName
		,u.Avatar
		,up.Amount
		,up.StartDateTime
		,up.EndDateTime
		,up.PaymentId as PaymentId,
		r.PaymentId as rg
		,CASE 
			WHEN r.EmployeeId IS NOT NULL
				THEN 1
			ELSE 0
			END AS IsEmployeeReviewed
	FROM dbo.UserPayment up
	JOIN dbo.Users u ON up.EmployeeId = u.Id
	LEFT JOIN dbo.Reviews r ON r.EmployeeId = u.Id and r.UserId=@Id
	AND (r.PaymentId = up.PaymentId OR (r.PaymentId IS NULL AND up.PaymentId IS NULL))
	WHERE up.UserId = @Id
END
GO;


CREATE OR ALTER PROCEDURE InsertImages 
    @Image NVARCHAR(100),
    @ImageOrigin INT,
    @UserId UNIQUEIDENTIFIER,
	@EmployeeId UNIQUEIDENTIFIER NULL
AS 
BEGIN 
    SET NOCOUNT ON; 

    BEGIN TRY
        INSERT INTO dbo.UserImages (Id, Image, ImageOrigin, [UserId], EmployeeId) 
        VALUES (NEWID(), @Image, @ImageOrigin, @UserId, @EmployeeId);
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END;
GO;

COMMIT;
GO;

BEGIN TRANSACTION
GO

ALTER TABLE dbo.NotificationReviews 
ADD StartDateTime DATETIME NOT NULL
GO;

ALTER TABLE dbo.NotificationReviews
ADD EndDateTime DATETIME NOT NULL
GO;

CREATE OR ALTER PROCEDURE CheckBookingsForYesterday
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Yesterday DATE = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE);

    IF EXISTS (SELECT 1 FROM dbo.UserPayment WHERE CAST(EndDateTime AS DATE) = @Yesterday)
    BEGIN
        INSERT INTO NotificationReviews (UserId, EmployeeId, PaymentId, Message, CreatedAt, StartDateTime, EndDateTime)
        SELECT UserId, EmployeeId, PaymentId, 'Condition met!', GETDATE(), StartDateTime, EndDateTime
        FROM dbo.UserPayment
        WHERE CAST(EndDateTime AS DATE) = @Yesterday;
    END;
END; 
GO;

COMMIT 
GO
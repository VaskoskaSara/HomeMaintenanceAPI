ALTER TABLE dbo.NotificationResults 
ADD StartDateTime DATETIME NOT NULL
GO;

ALTER TABLE dbo.NotificationResults 
ADD EndDateTime DATETIME NOT NULL
GO;

CREATE OR ALTER PROCEDURE CheckBookingsForYesterday
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Yesterday DATE = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE);

    IF EXISTS (SELECT 1 FROM dbo.UserPayment WHERE CAST(EndDateTime AS DATE) = @Yesterday)
    BEGIN
        INSERT INTO NotificationResults (UserId, EmployeeId, PaymentId, Message, CreatedAt, StartDateTime, EndDateTime)
        SELECT UserId, EmployeeId, PaymentId, 'Condition met!', GETDATE(), StartDateTime, EndDateTime
        FROM dbo.UserPayment
        WHERE CAST(EndDateTime AS DATE) = @Yesterday;
    END
END

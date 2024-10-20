CREATE OR ALTER PROCEDURE CheckBookingsForYesterday
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Yesterday DATE = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE);

    IF EXISTS (SELECT 1 FROM dbo.UserPayment WHERE CAST(EndDateTime AS DATE) = @Yesterday)
    BEGIN
        INSERT INTO NotificationResults (UserId, EmployeeId, PaymentId, Message, CreatedAt)
        SELECT UserId, EmployeeId, PaymentId, 'Condition met!', GETDATE()
        FROM dbo.UserPayment
        WHERE CAST(EndDateTime AS DATE) = @Yesterday;
    END
END

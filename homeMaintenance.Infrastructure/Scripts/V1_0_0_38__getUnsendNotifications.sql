CREATE OR ALTER PROCEDURE GetUnsendNotifications
@UserId UNIQUEIDENTIFIER
AS  
BEGIN
    SET NOCOUNT ON;

    SELECT Id, [Message], EmployeeId, PaymentId, CreatedAt
    FROM dbo.NotificationResults
    WHERE UserId = @UserId
END

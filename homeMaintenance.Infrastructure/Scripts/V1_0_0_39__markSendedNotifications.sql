CREATE OR ALTER PROCEDURE UpdateNotificationResults
@Id INT 
AS  
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.NotificationResults
    SET Sent = 1
    WHERE @Id = Id
END

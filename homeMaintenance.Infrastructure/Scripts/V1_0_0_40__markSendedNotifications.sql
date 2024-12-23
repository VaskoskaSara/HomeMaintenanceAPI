CREATE OR ALTER PROCEDURE UpdateNotificationReviews
@Id INT 
AS  
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.NotificationReviews
    SET Sent = 1
    WHERE @Id = Id
END; GO;

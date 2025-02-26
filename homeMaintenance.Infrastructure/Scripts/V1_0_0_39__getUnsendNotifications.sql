CREATE OR ALTER PROCEDURE [dbo].[GetUnsendNotifications]
@UserId UNIQUEIDENTIFIER
AS  
BEGIN
    SET NOCOUNT ON;

  SELECT distinct nr.Id, nr.UserId, nr.EmployeeId, nr.PaymentId as UserPaymentId, nr.CreatedAt, nr.StartDateTime, nr.EndDateTime
 FROM dbo.NotificationReviews nr
 WHERE nr.UserId = @UserId and nr.[Sent] = 0;
END;
GO;

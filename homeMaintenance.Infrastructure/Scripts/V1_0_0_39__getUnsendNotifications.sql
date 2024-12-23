CREATE OR ALTER PROCEDURE GetUnsendNotifications
@UserId UNIQUEIDENTIFIER
AS  
BEGIN
    SET NOCOUNT ON;

  SELECT distinct nr.Id, nr.[Message], nr.EmployeeId, nr.PaymentId, nr.CreatedAt, up.Id as UserPaymentId, nr.StartDateTime, nr.EndDateTime
 FROM dbo.NotificationReviews nr
 left JOIN dbo.UserPayment up ON
 (nr.PaymentId = up.PaymentId or (nr.PaymentId is null and up.PaymentId is null)) and
  nr.EmployeeId = up.EmployeeId 
 and up.UserId = nr.UserId and
 nr.StartDateTime = up.StartDateTime and
 nr.EndDateTime = up.EndDateTime
 WHERE nr.UserId = @UserId and nr.[Sent] = 0;
END; GO;

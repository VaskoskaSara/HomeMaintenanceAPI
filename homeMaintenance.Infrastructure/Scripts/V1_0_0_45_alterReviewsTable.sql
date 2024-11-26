BEGIN TRANSACTION;
GO

ALTER TABLE dbo.Reviews 
ADD UserPaymentId UNIQUEIDENTIFIER NOT NULL
GO;

ALTER TABLE dbo.Reviews
ADD CONSTRAINT FK_Reviews_UserPayment FOREIGN KEY (UserPaymentId) REFERENCES dbo.UserPayment(Id);
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
	,up.PaymentId as PaymentId
	,up.Id as UserPaymentId
	,r.PaymentId as rg
	,CASE 
		WHEN r.EmployeeId IS NOT NULL
			THEN 1
		ELSE 0
		END AS IsEmployeeReviewed
FROM dbo.UserPayment up
JOIN dbo.Users u ON up.EmployeeId = u.Id
LEFT JOIN dbo.Reviews r ON r.EmployeeId = u.Id and r.UserId=@Id and r.UserPaymentId = up.Id
AND (r.PaymentId = up.PaymentId OR (r.PaymentId IS NULL AND up.PaymentId IS NULL))
WHERE up.UserId = @Id
END;
GO;

  ALTER PROCEDURE InsertReview
  @Comment VARCHAR(MAX),
  @UserId UNIQUEIDENTIFIER, 
  @Rating INT,
  @EmployeeId UNIQUEIDENTIFIER,
  @PaymentId VARCHAR(MAX),
  @UserPaymentId UNIQUEIDENTIFIER
  AS 
  BEGIN
   
   INSERT INTO dbo.Reviews(Comment, UserId, Rating, EmployeeId, PaymentId, CreatedAt, UserPaymentId)
   VALUES (@Comment, @UserId, @Rating, @EmployeeId, @PaymentId, GETDATE(), @UserPaymentId)
  END
  GO;





COMMIT;
GO;

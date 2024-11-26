BEGIN TRANSACTION;
GO

CREATE OR ALTER PROCEDURE GetReviewsByUserId @Id UNIQUEIDENTIFIER
AS
BEGIN
SELECT DISTINCT users.Id as UserId
	,users.FullName
	,users.Avatar
	,reviews.Comment
	,reviews.Rating
	,images.[Image] as Photo
	,reviews.UserPaymentId
FROM dbo.Reviews reviews
JOIN dbo.Users users ON reviews.UserId = users.Id
LEFT JOIN dbo.UserPayment payment ON payment.UserId = reviews.UserId
	AND payment.EmployeeId = @Id and payment.PaymentId = reviews.PaymentId and payment.Id = reviews.UserPaymentId
LEFT JOIN dbo.UserImages images ON reviews.UserId = images.UserId
	AND ImageOrigin = 2
	AND images.EmployeeId = @Id
WHERE reviews.EmployeeId = @Id
END 
GO;

COMMIT;
GO;

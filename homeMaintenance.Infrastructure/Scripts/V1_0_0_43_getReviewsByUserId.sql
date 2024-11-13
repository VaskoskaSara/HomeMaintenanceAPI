BEGIN TRANSACTION;
GO

CREATE PROCEDURE GetReviewsByUserId @Id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT users.Id as UserId
		,users.FullName
		,users.Avatar
		,reviews.Comment
		,reviews.Rating
		,images.[Image] as Photo
	FROM dbo.Reviews reviews
	JOIN dbo.Users users ON reviews.UserId = users.Id
	LEFT JOIN dbo.UserPayment payment ON payment.UserId = reviews.UserId
		AND payment.EmployeeId = @Id and payment.PaymentId = reviews.PaymentId
	LEFT JOIN dbo.UserImages images ON reviews.UserId = images.UserId
		AND ImageOrigin = 2
		AND images.EmployeeId = @Id
	WHERE reviews.EmployeeId = @Id
END 
GO;

COMMIT;
GO;

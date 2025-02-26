BEGIN TRANSACTION
GO

ALTER TABLE dbo.Reviews 
ADD UserPaymentId UNIQUEIDENTIFIER NOT NULL
GO;

ALTER TABLE dbo.Reviews
ADD CONSTRAINT FK_Reviews_UserPayment FOREIGN KEY (UserPaymentId) REFERENCES dbo.UserPayment(Id);
GO;

ALTER TABLE dbo.UserImages 
ADD ReviewId UNIQUEIDENTIFIER
GO;

CREATE OR ALTER PROCEDURE [dbo].[GetReviewsByUserId] @Id UNIQUEIDENTIFIER
AS
BEGIN
SELECT DISTINCT reviews.UserId
	,users.FullName
	,users.Avatar
	,reviews.Comment
	,reviews.Rating
	,images.[Image] as Photo
	,reviews.UserPaymentId
FROM dbo.Reviews reviews
JOIN dbo.Users users ON reviews.UserId = users.Id
LEFT JOIN dbo.UserPayment payment ON payment.Id = reviews.UserPaymentId
LEFT JOIN dbo.UserImages images ON reviews.UserId = images.UserId
	AND ImageOrigin = 2
	AND images.EmployeeId = @Id
	AND images.reviewId = reviews.Id
WHERE reviews.EmployeeId = @Id
END;
GO;

COMMIT 
GO

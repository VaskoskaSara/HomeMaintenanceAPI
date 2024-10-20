  CREATE PROCEDURE InsertReview
  @Comment VARCHAR(MAX),
  @UserId UNIQUEIDENTIFIER, 
  @Rating INT,
  @EmployeeId UNIQUEIDENTIFIER,
  @PaymentId VARCHAR(MAX)
  AS 
  BEGIN
   
   INSERT INTO dbo.Reviews(Comment, UserId, Rating, EmployeeId, PaymentId, CreatedAt)
   VALUES (@Comment, @UserId, @Rating, @EmployeeId, @PaymentId, GETDATE())

  END
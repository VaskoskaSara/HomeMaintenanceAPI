BEGIN TRANSACTION;
GO

CREATE OR ALTER PROCEDURE GetUserNameById
    @Id UNIQUEIDENTIFIER
AS  
BEGIN
    SET NOCOUNT ON;

    SELECT 
    FullName
    FROM 
    dbo.Users
    WHERE 
    Id = @Id
END; 
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
	,u.[Address]
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

COMMIT;
GO;
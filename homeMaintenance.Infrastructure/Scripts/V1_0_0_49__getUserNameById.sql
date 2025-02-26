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

ALTER PROCEDURE [dbo].[GetBookingsByUser] @Id UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

SELECT u.Id AS EmployeeId
	,u.FullName
	,u.Avatar
	,up.Amount
	,up.StartDateTime
	,up.EndDateTime
	,up.Id as UserPaymentId
	,u.[Address]
	,CASE 
		WHEN r.EmployeeId IS NOT NULL
			THEN 1
		ELSE 0
		END AS IsEmployeeReviewed
FROM dbo.UserPayment up
JOIN dbo.Users u ON up.EmployeeId = u.Id
LEFT JOIN dbo.Reviews r ON r.EmployeeId = u.Id and r.UserId=@Id and r.UserPaymentId = up.Id
WHERE up.UserId = @Id
END;
GO;

COMMIT;
GO;
CREATE OR ALTER PROCEDURE GetBookingsByEmployee
    @Id UNIQUEIDENTIFIER
AS  
BEGIN
    SET NOCOUNT ON;

    SELECT 
    u.Id AS UserId,
    u.FullName,
	u.Avatar,
	up.Amount,
	up.StartDateTime,
	up.EndDateTime
    FROM 
    dbo.UserPayment up
    JOIN 
    dbo.Users u ON up.UserId = u.Id
    WHERE 
    up.EmployeeId =@Id
END; GO;

BEGIN TRANSACTION;
GO

CREATE PROCEDURE GetBookingsByUser
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
    dbo.Users u ON up.EmployeeId = u.Id
    WHERE 
    up.UserId =@Id
END
GO;

COMMIT;
GO;
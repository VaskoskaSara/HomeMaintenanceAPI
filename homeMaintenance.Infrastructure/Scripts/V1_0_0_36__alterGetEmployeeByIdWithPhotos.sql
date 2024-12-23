CREATE OR ALTER PROCEDURE GetEmployeeById
    @Id UNIQUEIDENTIFIER
AS  
BEGIN
    SET NOCOUNT ON;

    SELECT u.Id, FullName, PhoneNumber, Email, City, r.RoleName, Experience, Price, pt.PaymentType, Avatar, PositionName, BirthDate, NumberOfEmployees, [Description] 
    FROM dbo.Users u
	LEFT JOIN dbo.UserRoles r on u.UserRole = r.Id
	LEFT JOIN dbo.PaymentTypes pt ON u.PaymentType = pt.Id 
	LEFT JOIN dbo.Positions p ON u.PositionId = p.Id 
    WHERE u.Id = @Id AND r.RoleName != 'Customer';

    SELECT [Image] FROM dbo.UserImages where UserId = @Id and ImageOrigin=1
END; GO;

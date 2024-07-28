ALTER PROCEDURE GetEmployees
AS  
BEGIN
	SELECT Id, FullName, City, Experience, Price, PositionId, Avatar
	FROM dbo.Users users
	WHERE (UserRole <> (SELECT Id FROM dbo.UserRoles WHERE RoleName = 'Customer')) and IsDeleted = 0
END 

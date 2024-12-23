BEGIN TRANSACTION
GO

CREATE OR ALTER PROCEDURE GetEmployees
AS  
BEGIN
	SELECT Id, FullName, City, Experience, Price, PositionId
	FROM dbo.Users users
	WHERE (UserRole <> (SELECT Id FROM dbo.UserRoles WHERE RoleName = 'Customer')) and IsDeleted = 0
END; 
GO;

COMMIT 
GO
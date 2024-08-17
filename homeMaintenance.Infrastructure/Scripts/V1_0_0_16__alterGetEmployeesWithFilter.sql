ALTER PROCEDURE GetEmployees
	@City NVARCHAR(100),
    @Experience INT,
    @Price INT,
    @ByContract BIT = 0
AS  
BEGIN
	SELECT Id, FullName, City, Experience, Price, PositionId
	FROM dbo.Users users
	WHERE (UserRole <> (SELECT Id FROM dbo.UserRoles WHERE RoleName = 'Customer')) and IsDeleted = 0 and 
	(
	  (@City IS NULL OR City = @City) AND
      (@Experience IS NULL OR Experience = @Experience) AND
      (@Price IS NULL OR Price = @Price) AND
      (PaymentType IN (1, 2, 3) OR @ByContract = 1)
	)
END 

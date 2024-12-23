ALTER PROCEDURE [dbo].[GetEmployees]
    @Cities NVARCHAR(MAX) = NULL,  
    @Experience INT = NULL,        
    @Price INT = NULL,             
    @ExcludeByContract BIT = NULL         
AS  
BEGIN
    DECLARE @CityList TABLE (City NVARCHAR(100));
    
    IF @Cities IS NOT NULL
    BEGIN
        INSERT INTO @CityList (City)
        SELECT value FROM STRING_SPLIT(@Cities, ','); 
    END;

    SELECT Id, FullName, City, Experience, Price, PositionId, Avatar
    FROM dbo.Users users
    WHERE (UserRole <> (SELECT Id FROM dbo.UserRoles WHERE RoleName = 'Customer')) 
      AND IsDeleted = 0 
      AND (
            (@Cities IS NULL OR City IN (SELECT City FROM @CityList)) AND
            (@Experience IS NULL OR Experience >= @Experience) AND
            (
                (@ExcludeByContract = 0 AND 
                    (
                        (Price IS NULL AND PaymentType = 3) OR 
                        (Price < @Price AND Price IS NOT NULL) OR
						(@Price IS NULL AND PaymentType <> 3)
                    )) OR 
                (@ExcludeByContract = 1 AND 
                    (
                        (Price < @Price AND Price IS NOT NULL AND PaymentType <> 3) OR 
						(@Price IS NULL AND PaymentType <> 3)
                    ))
            )
    );
END; 
GO;
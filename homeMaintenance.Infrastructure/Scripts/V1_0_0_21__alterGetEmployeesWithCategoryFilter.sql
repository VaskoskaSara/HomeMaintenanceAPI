USE [homeMaintenance]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetEmployees]
    @Cities NVARCHAR(MAX) = NULL,  
    @Experience INT = NULL,        
    @Price INT = NULL,             
    @ExcludeByContract BIT = NULL,
	@CategoryIds NVARCHAR(MAX) = NULL
AS  
BEGIN
    DECLARE @CityList TABLE (City NVARCHAR(100));
    DECLARE @CategoryList TABLE (CategoryId UNIQUEIDENTIFIER);

    IF @Cities IS NOT NULL
    BEGIN
        INSERT INTO @CityList (City)
        SELECT value FROM STRING_SPLIT(@Cities, ','); 
    END

	IF @CategoryIds IS NOT NULL
    BEGIN
        INSERT INTO @CategoryList (CategoryId)
        SELECT CAST(value AS UNIQUEIDENTIFIER) 
        FROM STRING_SPLIT(@CategoryIds, ','); 
    END

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
            ) AND
            (@CategoryIds IS NULL OR PositionId IN (SELECT CategoryId FROM @CategoryList))
    );
END

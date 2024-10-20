BEGIN TRANSACTION;
GO

ALTER PROCEDURE GetUserByEmail 
@Email                        NVARCHAR(50)
AS 
BEGIN 
SET NOCOUNT ON 

SELECT Id, Email, [Password], UserRole, Avatar FROM dbo.Users WHERE Email = @Email AND IsDeleted = 0

END
GO;

COMMIT;
GO;
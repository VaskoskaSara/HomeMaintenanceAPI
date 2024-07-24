BEGIN TRANSACTION;
GO

CREATE PROCEDURE GetUserByEmail 
@Email                        NVARCHAR(50)
AS 
BEGIN 
SET NOCOUNT ON 

SELECT Email, [Password] FROM dbo.Users WHERE Email = @Email AND IsDeleted = 0

END
GO;

COMMIT;
GO;
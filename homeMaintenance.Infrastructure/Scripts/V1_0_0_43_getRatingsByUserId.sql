CREATE OR ALTER PROCEDURE GetRatingsByEmployeeId
  @Id UNIQUEIDENTIFIER
  AS
  BEGIN
	
	SELECT Rating FROM dbo.Reviews WHERE EmployeeId = @Id
  
  END; GO;
BEGIN TRANSACTION;
GO

ALTER TABLE dbo.UserImages
ADD ImageOrigin INT CHECK (ImageOrigin IN (1, 2));

ALTER TABLE dbo.UserImages DROP CONSTRAINT PK_ReviewImages;

ALTER TABLE dbo.UserImages
DROP CONSTRAINT FK_ReviewImages_Reviews;

ALTER TABLE dbo.UserImages
DROP COLUMN ReviewId;

GO;

CREATE OR ALTER PROCEDURE InsertImages 
@Image		  NVARCHAR(100),
@ImageOrigin INT
AS 
BEGIN 
SET NOCOUNT ON 

INSERT INTO dbo.UserImages (Id, Image, ImageOrigin) VALUES (NEWID(), @Image, @ImageOrigin)

END; GO;; GO;

COMMIT;
GO;

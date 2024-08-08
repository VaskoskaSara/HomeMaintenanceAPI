BEGIN TRANSACTION;

ALTER TABLE dbo.Users 
ALTER COLUMN PositionId uniqueidentifier NULL

ALTER TABLE dbo.Users 
ADD PaymentType int NULL

CREATE TABLE dbo.PaymentTypes
	(
	Id int NOT NULL,
	PaymentType nvarchar(50) NOT NULL,
	IsDeleted bigint DEFAULT 0 NOT NULL 
	)  ON [PRIMARY]

ALTER TABLE dbo.PaymentTypes ADD CONSTRAINT
	PK_PaymentTypes PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	
ALTER TABLE dbo.Users
ADD CONSTRAINT FK_Users_PaymentTypes FOREIGN KEY (PaymentType) REFERENCES dbo.PaymentTypes(Id);


INSERT INTO dbo.PaymentTypes (Id, PaymentType) VALUES (1, 'Hourly'), (2, 'Overall'), (3, 'ByContract');

COMMIT;
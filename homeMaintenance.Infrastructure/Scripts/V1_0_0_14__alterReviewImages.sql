BEGIN TRANSACTION;
GO

EXEC sp_rename 'ReviewImages', 'UserImages';

COMMIT;
GO;
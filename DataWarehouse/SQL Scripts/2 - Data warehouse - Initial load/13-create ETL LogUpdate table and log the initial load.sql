USE [sep4-DWH]
GO

EXEC sp_changedbowner 'sa';
GO

--- CREATE ETL.LogUpdate table && insert data of the last day that dim tables have been updated / last day recorded in the source system

CREATE TABLE ETL.LogUpdate (
	"Table" nvarchar(50) NULL,
	"LastLoadDate" int NULL,
) ON [PRIMARY]
GO

INSERT INTO ETL.LogUpdate ("Table", "LastLoadDate") VALUES ('DimUser', 20210920)
INSERT INTO ETL.LogUpdate ("Table", "LastLoadDate") VALUES ('DimPlant', 20210920)
INSERT INTO ETL.LogUpdate ("Table", "LastLoadDate") VALUES ('FactTable', 20210920)

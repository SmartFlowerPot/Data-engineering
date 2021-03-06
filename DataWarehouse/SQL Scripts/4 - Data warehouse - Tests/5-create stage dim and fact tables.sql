USE [test-sep4-DWH]

EXEC sp_changedbowner 'sa';
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[stage].[DimPlant]') AND type in (N'U'))
BEGIN
CREATE TABLE stage.DimPlant (
 PlantEUI nvarchar(50),
 PlantType nvarchar(50),
 DateOfBirth datetime,
 Age INT
);

END
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[stage].[DimUser]') AND type in (N'U'))
BEGIN
CREATE TABLE stage.DimUser (
 Username nvarchar(50),
 DateOfBirth datetime,
 Gender nvarchar(50),
 Region nvarchar(50),
 Age INT
);
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[stage].[FactTable]') AND type in (N'U'))
BEGIN
CREATE TABLE stage.FactTable (
 TimeStamp datetime,
 USER_ID INT,
 Username nvarchar(50),
 PLANT_ID INT,
 PlantEUI nvarchar(50),
 Temperature decimal(18, 2),
 CO2 decimal(18, 2),
 Humidity decimal(18, 2),
 LightLevel decimal(18, 2)
);

END
GO



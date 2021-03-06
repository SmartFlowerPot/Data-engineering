USE [sep4-DWH]


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[edw].[DimPlant]') AND type IN (N'U'))
BEGIN
CREATE TABLE edw.DimPlant (
 PLANT_ID INT IDENTITY(1,1) NOT NULL,
 PlantEUI nvarchar(50),
 PlantType nvarchar(50),
 DateOfBirth datetime,
 Age INT,
 ValidFrom INT,
 ValidTo INT
);

ALTER TABLE edw.DimPlant ADD CONSTRAINT PK_DimPlant PRIMARY KEY (PLANT_ID);
END
GO



IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[edw].[DimUser]') AND type IN (N'U'))
BEGIN
CREATE TABLE edw.DimUser (
 USER_ID INT IDENTITY(1,1) NOT NULL,
 Username nvarchar(50),
 DateOfBirth datetime,
 Gender nvarchar(50),
 Region nvarchar(50),
 Age INT,
 ValidFrom INT,
 ValidTo INT
);

ALTER TABLE edw.DimUser ADD CONSTRAINT PK_DimUser PRIMARY KEY (USER_ID);
END
GO



IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[edw].[FactTable]') AND type IN (N'U'))
BEGIN
CREATE TABLE edw.FactTable (
 TIME_ID INT NOT NULL,
 DATE_ID INT NOT NULL,
 USER_ID INT NOT NULL,
 PLANT_ID INT NOT NULL,
 Temperature decimal(18, 2),
 CO2 decimal(18, 2),
 Humidity decimal(18, 2),
 LightLevel decimal(18, 2),
 Timestamp datetime
);

ALTER TABLE edw.FactTable ADD CONSTRAINT PK_FactTable PRIMARY KEY (TIME_ID,DATE_ID,USER_ID,PLANT_ID);


ALTER TABLE edw.FactTable ADD CONSTRAINT FK_FactTable_TIME_ID FOREIGN KEY (TIME_ID) REFERENCES edw.DimTime (TIME_ID);
ALTER TABLE edw.FactTable ADD CONSTRAINT FK_FactTable_DATE_ID FOREIGN KEY (DATE_ID) REFERENCES edw.DimDate (DATE_ID);
ALTER TABLE edw.FactTable ADD CONSTRAINT FK_FactTable_USER_ID FOREIGN KEY (USER_ID) REFERENCES edw.DimUser (USER_ID);
ALTER TABLE edw.FactTable ADD CONSTRAINT FK_FactTable_PLANT_ID FOREIGN KEY (PLANT_ID) REFERENCES edw.DimPlant (PLANT_ID);
END
GO


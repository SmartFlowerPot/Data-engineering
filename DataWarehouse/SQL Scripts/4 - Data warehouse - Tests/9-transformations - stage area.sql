USE [test-sep4-DWH]
go


/******** stage.DimUser *********/

/** Calculate Age **/
UPDATE [test-sep4-DWH].stage.DimUser
SET Age = (CONVERT(int,CONVERT(char(8), getdate(),112))-CONVERT(char(8),[test-sep4-DWH].stage.DimUser.DateOfBirth,112))/10000
FROM [test-sep4-DWH].stage.DimUser
go

/** check N/A values for Gender **/
UPDATE [test-sep4-DWH].stage.DimUser
SET Gender = 'N/A'
FROM [test-sep4-DWH].stage.DimUser
WHERE Gender is NULL
go

/** check N/A values for Region **/
UPDATE [test-sep4-DWH].stage.DimUser
SET Region = 'N/A'
FROM [test-sep4-DWH].stage.DimUser
WHERE Region is NULL
go


/******** stage.DimPlant *********/

/** Calculate Age **/
UPDATE [test-sep4-DWH].stage.DimPlant
SET Age = (CONVERT(int,CONVERT(char(8), getdate(),112))-CONVERT(char(8),[test-sep4-DWH].stage.DimPlant.DateOfBirth,112))/10000
FROM [test-sep4-DWH].stage.DimPlant
go

/** check N/A values for Plant Type **/
UPDATE [test-sep4-DWH].stage.DimPlant
SET PlantType = 'N/A'
FROM [test-sep4-DWH].stage.DimPlant
WHERE PlantType is NULL
go


/***** remove null values from Measurements ******/

UPDATE [test-sep4-DWH].stage.FactTable
SET Temperature=-999
FROM [test-sep4-DWH].stage.FactTable
WHERE Temperature IS NULL
go

UPDATE [test-sep4-DWH].stage.FactTable
SET Humidity=-999
FROM [test-sep4-DWH].stage.FactTable
WHERE Humidity IS NULL
go

UPDATE [test-sep4-DWH].stage.FactTable
SET CO2=-999
FROM [test-sep4-DWH].stage.FactTable
WHERE CO2 IS NULL
go

UPDATE [test-sep4-DWH].stage.FactTable
SET LightLevel=-999
FROM [test-sep4-DWH].stage.FactTable
WHERE LightLevel IS NULL
go
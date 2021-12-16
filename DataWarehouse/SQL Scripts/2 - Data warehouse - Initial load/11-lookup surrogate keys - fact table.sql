USE [sep4-DWH]
GO


/** LOOKUP SURROGATE KEYS **/

/** Username **/
UPDATE [sep4-DWH].stage.FactTable
SET USER_ID = edw.USER_ID
FROM [sep4-DWH].edw.DimUser edw
WHERE stage.FactTable.Username=edw.Username
GO


/** PlantEUI **/
UPDATE [sep4-DWH].stage.FactTable
SET PLANT_ID = edw.PLANT_ID
FROM [sep4-DWH].edw.DimPlant edw
WHERE stage.FactTable.PlantEUI=edw.PlantEUI
GO


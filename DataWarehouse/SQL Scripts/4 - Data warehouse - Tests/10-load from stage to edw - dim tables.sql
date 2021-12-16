USE [test-sep4-DWH]
GO

/* load data from stage to edw - dim tables */

/** edw.DimUser **/
INSERT INTO [test-sep4-DWH].edw.DimUser
           ([Username]
           ,[DateOfBirth]
           ,[Gender]
           ,[Region]
           ,[Age])

SELECT      [Username]
           ,[DateOfBirth]
           ,[Gender]
           ,[Region]
           ,[Age]
FROM [test-sep4-DWH].stage.DimUser
GO



/** edw.DimPlant **/
INSERT INTO [test-sep4-DWH].edw.DimPlant
           ([PlantEUI]
		   ,[PlantType]
           ,[DateOfBirth]
           ,[Age])

SELECT [PlantEUI]
	  ,[PlantType]
      ,[DateOfBirth]
      ,[Age]
FROM [test-sep4-DWH].[stage].[DimPlant]
GO



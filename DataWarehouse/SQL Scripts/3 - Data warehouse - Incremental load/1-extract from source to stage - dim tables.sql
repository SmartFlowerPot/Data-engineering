USE [sep4-DWH]



/**** from source db, load into stage.DimUser ******/
truncate table [sep4-DWH].[stage].[DimUser]

INSERT INTO [stage].[DimUser]
           ([Username]
           ,[DateOfBirth]
           ,[Gender]
           ,[Region])

SELECT		a.Username,
			a.DateOfBirth,
			a.Gender,
			a.Region

FROM [sep4-db].dbo.Accounts a
GO


/**** from source table, load into stage.DimPlants  ******/
truncate table [sep4-DWH].[stage].[DimPlant]

INSERT INTO [sep4-DWH].stage.[DimPlant]
           ([PlantEUI],
		   [PlantType],
           [DateOfBirth])

SELECT		p.EUI,
			p.PlantType,
			p.DOB

FROM [sep4-db].dbo.Plants p
GO	  


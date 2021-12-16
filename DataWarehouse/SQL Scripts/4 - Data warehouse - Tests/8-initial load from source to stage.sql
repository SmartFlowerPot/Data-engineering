USE [test-sep4-DWH]


/**** from test source db, load into stage.DimUser ******/
truncate table [test-sep4-DWH].[stage].[DimUser]

INSERT INTO [stage].[DimUser]
           ([Username]
           ,[DateOfBirth]
           ,[Gender]
           ,[Region])

SELECT		a.Username,
			a.DateOfBirth,
			a.Gender,
			a.Region

FROM [test-sep4-db].dbo.Accounts a
GO


/**** from test source table, load into stage.DimPlants  ******/
truncate table [test-sep4-DWH].[stage].[DimPlant]

INSERT INTO [test-sep4-DWH].stage.[DimPlant]
           ([PlantEUI],
		   [PlantType],
           [DateOfBirth])

SELECT		p.EUI,
			p.PlantType,
			p.DOB

FROM [test-sep4-db].dbo.Plants p
GO	  


/**** from test source table, load into stage.FactTable  ******/
truncate table [test-sep4-DWH].[stage].[FactTable]

INSERT INTO [stage].[FactTable]
           ([TimeStamp]
           ,[Username]
           ,[PlantEUI]
           ,[Temperature]
           ,[CO2]
           ,[Humidity]
           ,[LightLevel])

SELECT	m.TimeStamp,
		p.AccountUsername,
		m.PlantEUI,
		m.Temperature,
		m.CO2,
		m.Humidity,
		m.Light
		

FROM [test-sep4-db].dbo.Accounts a
inner join [test-sep4-db].dbo.Plants p
on a.Username = p.AccountUsername
inner join [test-sep4-db].dbo.Measurements m
on p.EUI = m.PlantEUI

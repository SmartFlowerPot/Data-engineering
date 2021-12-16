USE [sep4-DWH]
GO

truncate table [sep4-DWH].[stage].[FactTable]

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
		

FROM [sep4-db].dbo.Accounts a
inner join [sep4-db].dbo.Plants p
on a.Username = p.AccountUsername
inner join [sep4-db].dbo.Measurements m
on p.EUI = m.PlantEUI

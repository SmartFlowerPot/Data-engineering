USE [sep4-DWH]
GO


--- #### EXTRACT FROM SOURCE INTO STAGE DATA WITH TIMESTAMP > LastLoadData ####

/*		get LastLoadDate	*/	
DECLARE @LastLoadDate datetime
SET @LastLoadDate = (SELECT CalendarDate FROM edw.DimDate
					 WHERE DATE_ID in (SELECT MAX(LastLoadDate) FROM ETL.LogUpdate WHERE [Table]='FactTable'))


TRUNCATE TABLE stage.FactTable

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
where convert(date, m.TimeStamp,101) > (@LastLoadDate)
GO



--- #### DATA CLEASING  ####


/***** remove null values from FactTable - Measurements ******/

UPDATE [sep4-DWH].stage.FactTable
SET Temperature=-999
FROM [sep4-DWH].stage.FactTable
WHERE Temperature IS NULL
go

UPDATE [sep4-DWH].stage.FactTable
SET Humidity=-999
FROM [sep4-DWH].stage.FactTable
WHERE Humidity IS NULL
go

UPDATE [sep4-DWH].stage.FactTable
SET CO2=-999
FROM [sep4-DWH].stage.FactTable
WHERE CO2 IS NULL
go

UPDATE [sep4-DWH].stage.FactTable
SET LightLevel=-999
FROM [sep4-DWH].stage.FactTable
WHERE LightLevel IS NULL
go





--- #### KEY LOOKUP  ####

USE [sep4-DWH]
GO

-- User
UPDATE stage.FactTable
SET stage.FactTable.USER_ID = edwUser.USER_ID
FROM stage.FactTable stgFact
right join  
edw.DimUser edwUser
on stgFact.Username = edwUser.Username
where edwUser.ValidTo = 99991231
GO


-- Plant
UPDATE stage.FactTable
SET stage.FactTable.PLANT_ID = edwPlant.PLANT_ID
FROM stage.FactTable stgFact
right join  
edw.DimPlant edwPlant
on stgFact.PlantEUI = edwPlant.PlantEUI
where edwPlant.ValidTo = 99991231
GO


--- #### LOAD INTO EDW FROM STAGE ####


INSERT INTO [sep4-DWH].[edw].[FactTable]
           ([TIME_ID]
           ,[DATE_ID]
           ,[USER_ID]
           ,[PLANT_ID]
           ,[Temperature]
           ,[CO2]
           ,[Humidity]
           ,[LightLevel]
           ,[Timestamp])

SELECT dt.TIME_ID
      ,dd.DATE_ID
      ,USER_ID
	  ,PLANT_ID
	  ,Temperature
	  ,CO2
	  ,Humidity
	  ,LightLevel
	  ,TimeStamp

FROM [sep4-DWH].stage.FactTable ft
  
inner join edw.DimDate dd
on cast(ft.TimeStamp as date)=dd.CalendarDate

inner join edw.DimTime dt
on cast(ft.TimeStamp as time)=dt.FullTimeString
GO



--- #### Update LogUpdate table ###


use [sep4-DWH]
DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)
insert into ETL.LogUpdate("Table", "LastLoadDate") values ('FactTable', @NewLoadDate)
GO
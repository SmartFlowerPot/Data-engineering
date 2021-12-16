USE [test-sep4-DWH]
GO

/** edw.FactTable :: load data from stage area into edw Fact Table **/

INSERT INTO [test-sep4-DWH].[edw].[FactTable]
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

  FROM [test-sep4-DWH].stage.FactTable ft
  
  inner join edw.DimDate dd
  on cast(ft.TimeStamp as date)=dd.CalendarDate

  inner join edw.DimTime dt
  on cast(ft.TimeStamp as time)=dt.FullTimeString

GO

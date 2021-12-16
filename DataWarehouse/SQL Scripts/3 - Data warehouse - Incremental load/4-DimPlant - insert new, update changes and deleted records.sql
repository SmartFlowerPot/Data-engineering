USE [sep4-DWH]
GO

--- DimPlant -> insert new, update changes and deleted records
GO

--- declare variables -> Last updated variables
DECLARE @LastLoadDate int
SET @LastLoadDate = (SELECT MAX([LastLoadDate]) FROM ETL.LogUpdate WHERE "Table" = 'DimPlant')

DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)

DECLARE @FutureDate int
SET @FutureDate = 99991231


--- INSERT NEW records from stage to edw 
--- ### WHEN NEW RECORDS FROM SOURCE DB HAVE BEEN LOADED INTO STAGE ALREADY
/*	start get new  */
INSERT INTO [edw].[DimPlant]
           ([PlantEUI]
		   ,[PlantType]
           ,[DateOfBirth]
           ,[Age]
           ,[ValidFrom]
           ,[ValidTo])
SELECT [PlantEUI]
	  ,[PlantType]
      ,[DateOfBirth]
      ,[Age]
	  ,@NewLoadDate
	  ,@FutureDate
FROM [stage].DimPlant
--- select only new inserted [PlantEUI] into stage / select all [PlantEUI] except the ones already in edw.DimPlants 
WHERE [PlantEUI] in (SELECT [PlantEUI]
					FROM stage.DimPlant 
					EXCEPT 
					SELECT [PlantEUI] 
					FROM edw.DimPlant
					WHERE ValidTo=99991231)


INSERT INTO ETL.LogUpdate ("Table", "LastLoadDate") VALUES ('DimPlant', @NewLoadDate)
/*	stop get new	*/
GO



--- ### WHEN NEW RECORDS WITH UPDATED FIELD VALUES && those are loaded into stage from source already
GO
/*		declare variables		*/
DECLARE @LastLoadDate int
SET @LastLoadDate = (SELECT MAX([LastLoadDate]) FROM ETL."LogUpdate" WHERE "Table" = 'DimPlant')

DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)

DECLARE @FutureDate int
SET @FutureDate = 99991231

/*	start get changed	*/
SELECT [PlantEUI]  --- get all records in stage
       ,[PlantType]
	   ,[DateOfBirth]
       ,[Age]
  INTO #tmp
  FROM stage.DimPlant 
  EXCEPT                  
  --- except the ones that already exist in edw
  SELECT [PlantEUI]
	   ,[PlantType]
       ,[DateOfBirth]
       ,[Age]
  FROM edw.DimPlant
  WHERE ValidTo=99991231
  -- then except new records
  EXCEPT
  SELECT [PlantEUI]
	   ,[PlantType]
       ,[DateOfBirth]
       ,[Age]
  FROM stage.DimPlant 
  WHERE [PlantEUI] in (select [PlantEUI] from stage.DimPlant 
												except
												select [PlantEUI] 
												from edw.DimPlant
												where ValidTo=99991231)
INSERT INTO [edw].[DimPlant]
           ([PlantEUI]
		   ,[PlantType]
           ,[DateOfBirth]
           ,[Age]
           ,[ValidFrom]
           ,[ValidTo])
SELECT  [PlantEUI]
	   ,[PlantType]
       ,[DateOfBirth]
       ,[Age]
	  ,@NewLoadDate
	  ,@FutureDate
FROM #tmp


UPDATE edw.DimPlant 
SET ValidTo = @NewLoadDate-1
WHERE [PlantEUI] in (select [PlantEUI] from #tmp) and edw.DimPlant.ValidFrom<@NewLoadDate

DROP TABLE IF EXISTS #tmp

INSERT INTO ETL.LogUpdate("Table", "LastLoadDate") values ('DimPlant', @NewLoadDate)
/*	stop get changed	*/
GO



--- ### DELETED RECORDS IN THE SOURCE DB, now to be "delete" in the DWH (set to invalid -> validto = yesterday)
GO

---	declare variables		
DECLARE @LastLoadDate int
SET @LastLoadDate = (SELECT MAX([LastLoadDate]) FROM ETL.LogUpdate WHERE "Table" = 'DimPlant')

DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)

DECLARE @FutureDate int
SET @FutureDate = 99991231


/*	start get deleted	*/
UPDATE edw.DimPlant
SET ValidTo = @NewLoadDate-1
WHERE [PlantEUI] in (select [PlantEUI]
					  from edw.DimPlant 
					  where [PlantEUI] in (select [PlantEUI] 
											from edw.DimPlant 
											except 
											select [PlantEUI] 
											from stage.DimPlant))
					  and ValidTo=99991231

INSERT INTO ETL.LogUpdate("Table", "LastLoadDate") values ('DimPlant', @NewLoadDate)
/*	end get deleted	*/
GO


USE [sep4-DWH]
GO

--- DimUser -> insert new, update changes and delete
GO

--- declare variables -> Last updated variables
DECLARE @LastLoadDate int
SET @LastLoadDate = (SELECT MAX([LastLoadDate]) FROM ETL.LogUpdate WHERE "Table" = 'DimUser')

DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)

DECLARE @FutureDate int
SET @FutureDate = 99991231


--- INSERT NEW records from stage to edw ### WHEN THERE ARE NEW RECORDS FOR CUSTOMER
/*	start get new	*/
INSERT INTO [edw].[DimUser]
           ([Username]
           ,[DateOfBirth]
           ,[Gender]
           ,[Region]
           ,[Age]
           ,[ValidFrom]
           ,[ValidTo])
     
	 SELECT [Username]
           ,[DateOfBirth]
           ,[Gender]
           ,[Region]
           ,[Age]
	  ,@NewLoadDate
	  ,@FutureDate
	 
	 FROM [stage].DimUser
	 ---select only new inserted CustomerID into stage / select all CustomerID except the ones already in edw.DimUser 
	 WHERE [Username] in (SELECT [Username]
						  FROM stage.DimUser 
						  EXCEPT 
						  SELECT [Username] 
						  FROM edw.DimUser
						  WHERE ValidTo=99991231)


INSERT INTO ETL."LogUpdate" ("Table", "LastLoadDate") VALUES ('DimUser', @NewLoadDate)
/*	stop get new	*/
GO



--- ### WHEN RECORDS WITH UPDATED FIELD VALUES ARE loaded into stage from source already
GO
/*		declare variables		*/
DECLARE @LastLoadDate int
SET @LastLoadDate = (SELECT MAX([LastLoadDate]) FROM ETL.LogUpdate WHERE "Table" = 'DimUser')

DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)

DECLARE @FutureDate int
SET @FutureDate = 99991231

/*	start get changed	*/
SELECT [Username]  --- get all records in stage
      ,[DateOfBirth]
      ,[Gender]
      ,[Region]
      ,[Age]

  INTO #tmp
  FROM stage.DimUser 
  EXCEPT                  
  --- except the ones that are valid(WHERE ValidTo=99991231) and already exist in edw
  SELECT [Username]
      ,[DateOfBirth]
      ,[Gender]
      ,[Region]
      ,[Age]

  FROM edw.DimUser
  WHERE ValidTo=99991231
  -- then except new records
  EXCEPT
  SELECT [Username]
      ,[DateOfBirth]
      ,[Gender]
      ,[Region]
      ,[Age]
  FROM stage.DimUser 
  WHERE [Username] in (select [Username] from stage.DimUser 
												except
												select [Username] 
												from edw.DimUser
												where ValidTo=99991231)
INSERT INTO [edw].[DimUser]
           ([Username]
           ,[DateOfBirth]
           ,[Gender]
           ,[Region]
           ,[Age]
           ,[ValidFrom]
           ,[ValidTo])

SELECT [Username]
      ,[DateOfBirth]
      ,[Gender]
      ,[Region]
      ,[Age]
		   ,@NewLoadDate
		   ,@FutureDate
FROM #tmp


UPDATE edw.DimUser 
SET ValidTo = @NewLoadDate-1
WHERE [Username] in (select [Username] from #tmp) and edw.DimUser.ValidFrom<@NewLoadDate

DROP TABLE IF exists #tmp

INSERT INTO ETL.LogUpdate("Table", "LastLoadDate") values ('DimUser', @NewLoadDate)
/*	stop get changed	*/
GO



--- ### DELETED RECORDS IN THE SOURCE DB, now to be "deleted" (invalid) in the DWH (set the validto = yesterday, so invalid)
GO

---	declare variables		
DECLARE @LastLoadDate int
SET @LastLoadDate = (SELECT MAX([LastLoadDate]) FROM ETL.LogUpdate WHERE "Table" = 'DimUser')

DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)

DECLARE @FutureDate int
SET @FutureDate = 99991231


/*	start get deleted	*/
update edw.DimUser
set ValidTo = @NewLoadDate-1
where [Username] in (select [Username]
					  from edw.DimUser 
					  where [Username] in (select [Username] 
											from edw.DimUser 
											except 
											select [Username] 
											from stage.DimUser))
					  and ValidTo=99991231

insert into ETL.LogUpdate("Table", "LastLoadDate") values ('DimUser', @NewLoadDate)
/*	end get deleted	*/
GO
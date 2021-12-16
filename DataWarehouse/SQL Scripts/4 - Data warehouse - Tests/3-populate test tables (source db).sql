USE [test-sep4-db]
go

-- enable identity insert
--set identity_insert [dbo].[Accounts] on

-- a sample of the accounts: get by date of birth that ends in 7 (modulos operator)
INSERT INTO [test-sep4-db].dbo.Accounts 
	  ([Username]
      ,[Password]
      ,[DateOfBirth]
      ,[Gender]
      ,[Region])

SELECT [Username]
      ,[Password]
      ,[DateOfBirth]
      ,[Gender]
      ,[Region]

FROM [sep4-db].[dbo].[Accounts]
WHERE CONVERT(int,DateOfBirth) % 10 = 7

--set identity_insert [dbo].[Accounts] off
go


-- getting plants data of accounts
INSERT INTO [test-sep4-db].dbo.[Plants]
           ([EUI]
		   ,[AccountUsername]
		   ,[DOB]
           ,[PlantType])

SELECT     [EUI]
		   ,[AccountUsername]
		   ,[DOB]
           ,[PlantType]
FROM [sep4-db].dbo.Plants
WHERE AccountUsername IN (
			SELECT DISTINCT Username    -- select relevant Username, only the ones previously select in the script above (a sample of the accounts)
			FROM dbo.Accounts
			)
go


-- measurements
USE [test-sep4-db]
GO

INSERT INTO [test-sep4-db].[dbo].[Measurements]
           ([TimeStamp]
           ,[Temperature]
           ,[Humidity]
           ,[CO2]
           ,[Light]
           ,[PlantEUI])

SELECT [TimeStamp]
           ,[Temperature]
           ,[Humidity]
           ,[CO2]
           ,[Light]
           ,[PlantEUI]

FROM [sep4-db].dbo.Measurements
WHERE PlantEUI IN (
			SELECT DISTINCT EUI
			FROM [test-sep4-db].dbo.Plants)   --- select only the EUI selected previously in the plants
			
GO



USE [sep4-DWH]


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[edw].[DimDate]') AND type IN (N'U'))
BEGIN
CREATE TABLE [edw].[DimDate]
(
	[DATE_ID] [int] NOT NULL,
	[CalendarDate] [datetime] NOT NULL, 
	[Year] [int] NOT NULL, 
	[Month] [int] NOT NULL, 
	[MonthName] [nvarchar](10) NOT NULL,
	[DayNumber] [int] NOT NULL, 
	[DayOfWeek] [int] NOT NULL, 
	[WeekdayName] [nvarchar](9) NOT NULL,
	CONSTRAINT [PK_DimDate] PRIMARY KEY CLUSTERED
	(
	[DATE_ID] ASC
	)
) ON [PRIMARY]



/**** adding date data ****/
DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;

SET @StartDate = '1900-01-01'
SET @EndDate = DATEADD(YEAR,150,getdate())

while @StartDate <= @EndDate
	BEGIN 
		INSERT INTO edw.[DimDate](
			[DATE_ID],
			[CalendarDate], 
			[Year], 
			[Month], 
			[MonthName],
			[DayNumber],
			[DayOfWeek],
			[WeekdayName]
			)
			SELECT
			CONVERT(char(8), @StartDate, 112) as DATE_ID
			,@StartDate as [Date]
			,DATEPART(year, @StartDate) as Year
			,DATEPART(month, @StartDate) as Month 
			,DATENAME(month, @StartDate) as MonthName
			,DATEPART(day, @StartDate) as Day
			,DATEPART(weekday, @StartDate) as DayOfWeek
			,DATENAME(weekday,@StartDate) as WeekdayName

			SET @StartDate = DATEADD(dd,1,@StartDate)
	END

END
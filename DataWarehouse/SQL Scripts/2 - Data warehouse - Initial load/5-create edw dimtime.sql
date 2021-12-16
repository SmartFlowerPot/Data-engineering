USE [sep4-DWH]
GO

-- creating the structure of time dimension:

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[edw].[DimTime]') AND type IN (N'U'))
BEGIN
CREATE TABLE edw.DimTime(

TIME_ID int NOT NULL,

Hour int NULL,

HourShortString nvarchar(2) NULL,

Minute int NULL,

MinuteShortString nvarchar(2) NULL,

Second int NULL,

SecondShortString nvarchar(2) NULL,

FullTimeString nvarchar(8) NULL,

FullTime time(7) NULL,

DayTimeSlot nvarchar(100) NULL,

DayTimeSlotOrderKey int NULL,

CONSTRAINT PK_DimTime PRIMARY KEY CLUSTERED

(

TIME_ID ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]

END
GO




-- generating members (records) for Time Dimension:

declare @hour int

declare @minute int

declare @second int

declare @TIME_ID int

set @hour=0

set @TIME_ID=0

while @hour<24

begin

set @minute=0

while @minute<60

begin

set @second=0

while @second<60

begin

 

set @TIME_ID=(@hour*10000) + (@minute*100) + @second;

 

INSERT INTO edw.DimTime

(TIME_ID

,Hour

,HourShortString

,Minute

,MinuteShortString

,Second

,SecondShortString

,FullTimeString

,FullTime

,DayTimeSlot

,DayTimeSlotOrderKey)

 


select

(@hour*10000) + (@minute*100) + @second as TIME_ID,

@hour as Hour,

right('0'+convert(nvarchar(2),@hour),2) HourShortString,

@minute as Minute,

right('0'+convert(nvarchar(2),@minute),2) MinuteShortString,

@second as Second,

right('0'+convert(nvarchar(2),@second),2) SecondShortString,

right('0'+convert(nvarchar(2),@hour),2)+':'+

right('0'+convert(nvarchar(2),@minute),2)+':'+

right('0'+convert(nvarchar(2),@second),2) FullTimeString,


convert(time,right('0'+convert(nvarchar(2),@hour),2)+':'+

right('0'+convert(nvarchar(2),@minute),2)+':'+

right('0'+convert(nvarchar(2),@second),2)) as FullTime,


CASE

WHEN ((@TIME_ID) >= 00000 AND (@TIME_ID) <= 25959)

THEN 'Late Night (00:00 AM To 02:59 AM)'

WHEN ((@TIME_ID)  >= 30000 AND (@TIME_ID)  <= 65959)

THEN 'Early Morning(03:00 AM To 6:59 AM)'

WHEN ((@TIME_ID)  >= 70000 AND (@TIME_ID)  <= 85959)

THEN 'AM Peak (7:00 AM To 8:59 AM)'

WHEN ((@TIME_ID)  >= 90000 AND (@TIME_ID)  <= 115959)

THEN 'Mid Morning (9:00 AM To 11:59 AM)'

WHEN ((@TIME_ID)  >= 120000 AND (@TIME_ID)  <= 135959)

THEN 'Lunch (12:00 PM To 13:59 PM)'

WHEN ((@TIME_ID)  >= 140000 AND (@TIME_ID)  <= 155959)

THEN 'Mid Afternoon (14:00 PM To 15:59 PM)'

WHEN ((@TIME_ID)  >= 50000 AND (@TIME_ID)  <= 175959)

THEN 'PM Peak (16:00 PM To 17:59 PM)'

WHEN ((@TIME_ID)  >= 180000 AND (@TIME_ID)  <= 235959)

THEN 'Evening (18:00 PM To 23:59 PM)'

WHEN ((@TIME_ID)  >= 240000) THEN 'Previous Day Late Night

(24:00 PM to '+cast(  @hour as nvarchar(10)) +':00 PM )'

END   as DayTimeSlot,

CASE

WHEN ((@TIME_ID) >= 00000 AND (@TIME_ID) <= 25959)

THEN 1

WHEN ((@TIME_ID)  >= 30000 AND (@TIME_ID)  <= 65959)

THEN 2

WHEN ((@TIME_ID)  >= 70000 AND (@TIME_ID)  <= 85959)

THEN 3

WHEN ((@TIME_ID)  >= 90000 AND (@TIME_ID)  <= 115959)

THEN 4

WHEN ((@TIME_ID)  >= 120000 AND (@TIME_ID)  <= 135959)

THEN 5

WHEN ((@TIME_ID)  >= 140000 AND (@TIME_ID)  <= 155959)

THEN 6

WHEN ((@TIME_ID)  >= 50000 AND (@TIME_ID)  <= 175959)

THEN 7

WHEN ((@TIME_ID)  >= 180000 AND (@TIME_ID)  <= 235959)

THEN 8

WHEN ((@TIME_ID)  >= 240000) THEN 9

END   as DayTimeSlotOrderKey

set @second=@second+1

end

set @minute=@minute+1

end

set @hour=@hour+1

end


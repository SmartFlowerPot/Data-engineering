
-- ## TEST CASES ##

-- #############################
-- ## DATA QUALITY DIMENSIONS ##

-- completeness -> check for non nulls
-- uniqueness -> make sure that the rows are unique
-- validity -> check the range of values; examples: light, CO2 measurements cannot be negative, temperature cannot have a value of 1000 (valid within a range)
-- accuracy -> check the accuracy of the numbers

-- #############################


/** uniqueness: NUMBER OF ACCOUNTS **/
SELECT count(*) as CountAccountsOnSourceDB
FROM [test-sep4-db].dbo.Accounts

SELECT count(*) as CountAccountsOnDWH
FROM [test-sep4-DWH].edw.DimUser


/** uniqueness: NUMBER OF PLANTS **/
SELECT count(*) as CountPlantsOnSourceDB
FROM [test-sep4-db].dbo.Plants

SELECT count(*) as CountPlantsOnDWH
FROM [test-sep4-DWH].edw.DimPlant


/** uniqueness: NUMBER OF MEASUREMENTS **/
SELECT count(TimeStamp) as CountMeasurementsLinesInSourceDB
FROM [test-sep4-db].dbo.Measurements

SELECT count(Timestamp) as CountMeasurementsLinesInDWH
FROM [test-sep4-DWH].edw.FactTable




/** accuracy: CHECK SUM OF TEMPERATURE **/
SELECT sum(Temperature) as SumOfTemperatureInSourceDB
FROM [test-sep4-db].dbo.Measurements

SELECT sum(Temperature) as SumOfTemperatureInDWH
FROM [test-sep4-DWH].edw.FactTable

/** accuracy: CHECK SUM OF CO2 **/
SELECT sum(CO2) as SumOfCO2InSourceDB
FROM [test-sep4-db].dbo.Measurements

SELECT sum(CO2) as SumOfCO2InDWH
FROM [test-sep4-DWH].edw.FactTable

/** accuracy: CHECK SUM OF HUMIDITY **/
SELECT sum(Humidity) as SumOfHumidityInSourceDB
FROM [test-sep4-db].dbo.Measurements

SELECT sum(Humidity) as SumOfHumidityInDWH
FROM [test-sep4-DWH].edw.FactTable

/** accuracy: CHECK SUM OF LIGHT **/
SELECT sum(Light) as SumOfLightInSourceDB
FROM [test-sep4-db].dbo.Measurements

SELECT sum(LightLevel) as SumOfLightInDWH
FROM [test-sep4-DWH].edw.FactTable





/** completeness: CHECK NULL in ACCOUNTS / User fields **/
SELECT count(*) as CountNullsInAccountsSourceDB
FROM [test-sep4-db].dbo.Accounts
WHERE Accounts.DateOfBirth is NULL or Accounts.Gender is NULL or Accounts.Region is NULL or Accounts.Username is NULL

SELECT count(*) as CountNullsInAccountsDWH
FROM [test-sep4-DWH].edw.DimUser
WHERE DateOfBirth is NULL or Gender is NULL or Region is NULL or Username is NULL


/** completeness: CHECK empty in ACCOUNTS / User fields **/
SELECT count(*) as CountEmptyFieldssInAccountsSourceDB
FROM [test-sep4-db].dbo.Accounts
WHERE Accounts.DateOfBirth = '' or Accounts.Gender = '' or Accounts.Region = '' or Accounts.Username = ''

SELECT count(*) as CountEmptyFieldssInAccountsDWH
FROM [test-sep4-DWH].edw.DimUser
WHERE DateOfBirth = '' or Gender = '' or Region = '' or Username = ''





/** completeness: CHECK NULLs in PLANTS / Plant fields **/
SELECT count(*) as CountNullsInPlantsSourceDB
FROM [test-sep4-db].dbo.Plants
WHERE Plants.PlantType is NULL or Plants.DOB is NULL or Plants.EUI is NULL

SELECT count(*) as CountNullsInPlantsDWH
FROM [test-sep4-DWH].edw.DimPlant
WHERE PlantType is NULL or DateOfBirth is NULL or PlantEUI is NULL


/** completeness: CHECK empty in PLANTS / Plant fields **/
SELECT count(*) as CountEmptyFieldssInAccountsSourceDB
FROM [test-sep4-db].dbo.Accounts
WHERE Accounts.DateOfBirth = '' or Accounts.Gender = '' or Accounts.Region = '' or Accounts.Username = ''

SELECT count(*) as CountEmptyFieldssInAccountsDWH
FROM [test-sep4-DWH].edw.DimUser
WHERE DateOfBirth = '' or Gender = '' or Region = '' or Username = ''




/** completeness: CHECK NULLs in Measurements fields **/
SELECT count(*) as CheckNullsInMeasurementsSourceDB
FROM [test-sep4-db].dbo.Measurements
WHERE Measurements.Temperature is NULL or Measurements.CO2 is NULL or Measurements.Humidity is NULL or Measurements.Light is NULL or Measurements.TimeStamp is NULL

SELECT count(*) as CheckNullsInMeasurementFactTableDWH
FROM [test-sep4-DWH].edw.FactTable
WHERE FactTable.Temperature is NULL or FactTable.CO2 is NULL or FactTable.Humidity is NULL or FactTable.LightLevel is NULL or FactTable.Timestamp is NULL




/** validity: CHECK RANGE OF VALUES for Measurements fields **/

-- ## LOW VALUES ## 
SELECT count(*) as CountOutOfRangeLowValuesInSourceDB
FROM [test-sep4-db].dbo.Measurements
WHERE Measurements.CO2 < 0 or Measurements.Humidity < 0 or Measurements.Light < 0 or Measurements.Temperature < -40

SELECT count(*) as CountOutOfRangeLowValuesInDHW
FROM [test-sep4-DWH].edw.FactTable
WHERE FactTable.CO2 < 0 or FactTable.Humidity < 0 or FactTable.LightLevel < 0 or FactTable.Temperature < -40

-- ## HIGH VALUES ## 
SELECT count(*) as CountOutOfRangeHighValuesInSourceDB
FROM [test-sep4-db].dbo.Measurements
WHERE Measurements.CO2 > 2000 or Measurements.humidity > 500 or Measurements.Light > 5000 or Measurements.Temperature > 60

SELECT count(*) as CountOutOfRangeHighValuesInDWH
FROM [test-sep4-DWH].edw.FactTable
WHERE FactTable.CO2 > 2000 or FactTable.humidity > 500 or FactTable.LightLevel > 5000 or FactTable.Temperature > 60


USE [test-sep4-DWH]
GO


--- update data to ValidFrom and ValidTo based on last load (initial load)
update [test-sep4-DWH].edw.DimUser
set ValidFrom = 20210920, ValidTo = 99991231

update [test-sep4-DWH].edw.DimPlant
set ValidFrom = 20210920, ValidTo = 99991231


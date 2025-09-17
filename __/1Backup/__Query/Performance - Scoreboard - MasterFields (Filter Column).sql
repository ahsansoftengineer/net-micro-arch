USE [QA-HCM-ORG-CHART-DB]
 
SELECT * FROM MasterFields WHERE TableName ='Employee' AND (NAME = 'FullName' OR NAME = 'EmployeeId')

UPDATE [QA-HCM-ORG-CHART-DB].[dbo].[MasterFieldsFilter]
SET FieldDataType = 'long'
WHERE MasterFieldsId = '0E688047-7195-445B-B53B-BDCE43BBB316';

UPDATE dbo.MasterFields
SET FieldDataType = 'long'
WHERE MasterFieldsId = 'A1BD0419-C829-4AFE-9CB4-A697DF2AB7B1';
-- MasterFieldKey Come from Above Tables

USE [QA-HCM-PERFORMANCE-DB]

GO
 
INSERT INTO dbo.FieldFilter 

    (MasterFieldKey, DisplayFieldName, FieldName, IsActive, TableName, DisplayTableName) 

VALUES 

    ('F3AE963A-6653-4557-8808-05CB5A4CBB76', 'Emp Name', 'FullName', 1, 'Employee', 'Employee'),

    ('A1BD0419-C829-4AFE-9CB4-A697DF2AB7B1', 'Emp Id', 'EmployeeId', 1, 'Employee', 'Employee');

GO 
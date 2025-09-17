DECLARE @ScoreboardId AS INT = 2114
SELECT * FROM Scoreboard WHERE ScoreboardId = @ScoreboardId; -- 26847
SELECT * FROM ScoreboardParameterMapping WHERE ScoreboardId = @ScoreboardId;
SELECT * FROM ScoreboardGlobalFactorParameterMapping WHERE ScoreboardId = @ScoreboardId;
SELECT * FROM Parameter WHERE ScoreboardId = @ScoreboardId 

-- ParameterEquation Join By ParameterId
SELECT * FROM ParameterEquation 
	WHERE ParameterId IN (27260, 27261, 27262); 

-- ParameterEquationDetail Join By ParameterEquationId
SELECT * FROM ParameterEquationDetail 
	WHERE ParameterEquationId IN (3188, 3189, 3190) 
	AND OperatorTableId LIKE '________-____-____-____-____________';

SELECT * FROM Factor WHERE FactorId IN (4549, 4745, 4550, 4761, 4759, 3741, 4756);
SELECT * FROM Factor WHERE ScoreboardId = 2114 OR FactorId IN (4549, 4745, 4550, 4761, 4759, 3741, 4756);

DECLARE @ScoreboardId AS INT = 2114
SELECT * FROM Factor WHERE  FactorMasterKey 
	IN ('47fc32dc-ac6a-4eed-b32a-65f45e19bd8d',
'a3ddb033-d50a-4d21-a7a3-6611f0024d80',
'7ce1c1cf-21fa-4815-a793-aa6b6abbfcdd',
'805e1675-2a81-4bf0-a8c0-38e5e5e0177f',
'a3ddb033-d50a-4d21-a7a3-6611f0024d80',
'18cce148-0df2-4524-af06-0637e51fbc77',
'1dade8dd-23e4-4014-affd-796f51453544'
)  
	AND ((IsGlobal = 1 AND Status = 4 AND ScoreboardId  is null) 
	OR (IsGlobal != 1 AND Status = 2 AND ScoreboardId = @ScoreboardId))

-- Performance - Scoreboard - Parameter - ParameterEquation
USE [QA-HCM-PERFORMANCE-DB]
GO
ALTER PROCEDURE [dbo].[GetScoreboardParameterEquation]  
  @ScoreboardId bigint,
  @CreatedBy bigint,
  @CreatedAt datetime,
  @CompanyId bigint
AS    
BEGIN    
  
SELECT PE.*
 FROM  
  ParameterEquation PE   
 JOIN   
  ScoreboardGlobalFactorParameterMapping SGFPM ON SGFPM.ScoreboardId = @ScoreboardId  
 JOIN   
  ScoreboardParameterMapping SPM ON SPM.ScoreboardId = @ScoreboardId  
 JOIN  
  Parameter P   
   ON P.ScoreboardId = @ScoreboardId   
   AND P.ParameterMasterKey = SPM.ParameterMasterKey   
   AND P.ParameterMasterKey = SGFPM.ParameterMasterKey  
   AND PE.ParameterId = P.ParameterId  
  
END
EXEC [GetScoreboardParameterEquation] @ScoreboardId=2114, @CreatedBy=1, @CompanyId=1, @CreatedAt='';

--DROP PROCEDURE IF EXISTS GetEquationUsedFactors;

---- CHECK SP EXSIST
--IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetScoreboardParameterEquation')
--BEGIN
--    PRINT 'Procedure exists.'
--END
--ELSE
--BEGIN
--    PRINT 'Procedure does not exist.'
--END

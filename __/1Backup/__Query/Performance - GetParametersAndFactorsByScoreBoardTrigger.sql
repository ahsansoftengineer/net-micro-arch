ALTER PROCEDURE GetParametersAndFactorsByScoreBoardTrigger
@ScoreBoardTriggerId BIGINT, --1448
@CreatedBy BIGINT = 0,  
@CreatedAt DATETIME = NULL,  
@CompanyId BIGINT  = 1 
AS  
BEGIN  
  
	DECLARE @sql NVARCHAR(MAX), @factorCount INT, @parameterCount INT;
	DECLARE @factorNames TABLE (FactorRank INT, FactorName NVARCHAR(255));
	DECLARE @parameterNames TABLE (ParameterRank INT, ParameterName NVARCHAR(255));

	-- Get distinct factor names
	WITH DistinctFactors AS (
		SELECT DISTINCT F.FactorId, F.Name
		FROM ScoreCardFactorResultant SFR
		INNER JOIN Factor F ON F.FactorId = SFR.FactorId
		WHERE SFR.IsDeleted = 0 AND SFR.ScoreBoardTriggerId = @ScoreBoardTriggerId
	)
	INSERT INTO @factorNames (FactorRank, FactorName)
	SELECT ROW_NUMBER() OVER (ORDER BY Name) AS FactorRank, Name
	FROM DistinctFactors;

	-- Get distinct parameter names
	WITH DistinctParameters AS (
		SELECT DISTINCT P.ParameterId, P.Name
		FROM ParameterResultant PR
		INNER JOIN Parameter P ON P.ParameterId = PR.ParameterId
		WHERE PR.IsDeleted = 0 AND PR.ScoreBoardTriggerId = @ScoreBoardTriggerId
	)
	INSERT INTO @parameterNames (ParameterRank, ParameterName)
	SELECT ROW_NUMBER() OVER (ORDER BY Name) AS ParameterRank, Name
	FROM DistinctParameters;

	-- Count
	SELECT @factorCount = MAX(FactorRank) FROM @factorNames;
	SELECT @parameterCount = MAX(ParameterRank) FROM @parameterNames;

	-- Start building dynamic SQL
	SET @sql = N'
	WITH Factors AS (
		SELECT 
			SFR.EmployeeId,
			SFR.FactorResultantValue,
			SB.Name AS ScoreboardName,
			F.Name AS FactorName
		FROM 
			ScoreCardFactorResultant SFR
			INNER JOIN Scoreboard AS SB ON SB.ScoreboardId = SFR.ScoreboardId AND SB.Status = 4
			INNER JOIN Factor F ON F.FactorId = SFR.FactorId AND F.IsDeleted = 0
		WHERE 
			SFR.IsDeleted = 0 AND SFR.ScoreBoardTriggerId = @ScoreBoardTriggerId
	),
	DistinctScoreboard AS (
		SELECT 
			SCR.EmployeeId,
			SCR.ScoreBoardTriggerId,
			SCR.ParametersTotalScoreValue,
			SCR.TotalParametersAchievedValue,
			SCR.TotalScoreRatio,
			SCR.[Percent],
			SCR.Rank,
			SCR.Grade
		FROM 
			ScoreboardCalculatedResult SCR
		WHERE 
			SCR.IsDeleted = 0 AND SCR.ScoreBoardTriggerId = @ScoreBoardTriggerId
		GROUP BY
			SCR.EmployeeId,
			SCR.ScoreBoardTriggerId,
			SCR.ParametersTotalScoreValue,
			SCR.TotalParametersAchievedValue,
			SCR.TotalScoreRatio,
			SCR.[Percent],
			SCR.Rank,
			SCR.Grade
	),
	Parameters AS (
		SELECT 
			PR.EmployeeId,
			PR.Value,
			SB.Name AS ScoreboardName,
			P.Name AS ParameterName,
			D.ParametersTotalScoreValue,
			D.TotalParametersAchievedValue,
			D.TotalScoreRatio,
			D.[Percent],
			D.Rank,
			D.Grade
		FROM 
			ParameterResultant PR
			INNER JOIN DistinctScoreboard D ON PR.EmployeeId = D.EmployeeId AND PR.ScoreBoardTriggerId = D.ScoreBoardTriggerId
			INNER JOIN Scoreboard SB ON PR.ScoreBoardId = SB.ScoreboardId AND SB.Status = 4
			INNER JOIN Parameter P ON PR.ParameterId = P.ParameterId
		WHERE 
			PR.IsDeleted = 0 AND PR.ScoreBoardTriggerId = @ScoreBoardTriggerId
	),
	EmployeeInfo AS (
		SELECT DISTINCT E.EmployeeId, E.EmployeeName
		FROM ScoreboardTriggeredApplicationPopulationMapping E
		WHERE E.ScoreBoardTriggerId = @ScoreBoardTriggerId
	)
	SELECT 
		COALESCE(p.EmployeeId, f.EmployeeId) AS EmployeeId,
		e.EmployeeName,
		COALESCE(p.ScoreboardName, f.ScoreboardName) AS ScoreboardName,
	';

	-- Add dynamic factor columns
	DECLARE @i INT = 1;
	WHILE @i <= @factorCount
	BEGIN
		DECLARE @factorName NVARCHAR(255);
		SELECT @factorName = FactorName FROM @factorNames WHERE FactorRank = @i;
		SET @sql += N'MAX(CASE WHEN f.FactorName = ''' + @factorName + N''' THEN f.FactorResultantValue END) AS [' + @factorName + '(F)],';
		SET @i += 1;
	END

	-- Add dynamic parameter columns
	SET @i = 1;
	WHILE @i <= @parameterCount
	BEGIN
		DECLARE @parameterName NVARCHAR(255);
		SELECT @parameterName = ParameterName FROM @parameterNames WHERE ParameterRank = @i;
		SET @sql += N'MAX(CASE WHEN p.ParameterName = ''' + @parameterName + N''' THEN p.Value END) AS [' + @parameterName + '(P)],';
		SET @i += 1;
	END

	-- Add summary columns and finish query
	SET @sql = LEFT(@sql, LEN(@sql) - 1) + N', 
		p.ParametersTotalScoreValue,
		p.TotalParametersAchievedValue,
		p.TotalScoreRatio,
		p.[Percent],
		p.Rank,
		p.Grade
	FROM 
		Factors f
	FULL OUTER JOIN 
		Parameters p ON f.EmployeeId = p.EmployeeId AND f.ScoreboardName = p.ScoreboardName
	LEFT JOIN 
		EmployeeInfo e ON COALESCE(p.EmployeeId, f.EmployeeId) = e.EmployeeId
	GROUP BY
		COALESCE(p.EmployeeId, f.EmployeeId),
		e.EmployeeName,
		COALESCE(p.ScoreboardName, f.ScoreboardName),
		p.ParametersTotalScoreValue,
		p.TotalParametersAchievedValue,
		p.TotalScoreRatio,
		p.[Percent],
		p.Rank,
		p.Grade
	ORDER BY
		COALESCE(p.EmployeeId, f.EmployeeId);';
 
	EXEC SP_EXECUTESQL @sql, N'@ScoreBoardTriggerId BIGINT', @ScoreBoardTriggerId;
  
END

EXEC GetParametersAndFactorsByScoreBoardTrigger @ScoreBoardTriggerId = 1448
-- Execute final dynamic SQL
--EXEC sp_executesql @sql, N'@ScoreBoardTriggerId BIGINT', @ScoreBoardTriggerId;

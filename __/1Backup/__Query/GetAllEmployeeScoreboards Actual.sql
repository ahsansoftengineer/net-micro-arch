SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllEmployeeScoreboards]    
@EmployeeId BIGINT,    
@TYPE_EmployeeRoleProfileIds Type_IdsList READONLY,    
@CreatedBy BIGINT,    
@CreatedAt DATETIME,    
@CompanyId BIGINT    
AS    
BEGIN    
    WITH ScoreboardsAccess AS (    
        SELECT ScoreboardId    
        FROM ScoreboardOwner SRP    
        INNER JOIN @TYPE_EmployeeRoleProfileIds erp ON erp.Value = SRP.OwnerId     
    ),  
    LatestScoreBoardTrigger AS (  
        SELECT  
            ScoreBoardId,  
            StatusId,  
            PerformancePeriodDetailId,  
            ROW_NUMBER() OVER (PARTITION BY ScoreboardMasterKey ORDER BY ScoreBoardTriggerId DESC) AS RowNum  
        FROM  
            ScoreBoardTrigger  
        WHERE  
            IsDeleted = 0  
    ),  
    CTEScoreboard AS (  
        SELECT  
            S.ScoreboardId,  
            S.ScoreboardMasterKey,  
            S.ModifiedAt,  
            S.Name,    
            CONCAT(CAST(S.Version AS NVARCHAR(10)), '.', CAST(S.DraftVersion AS NVARCHAR(10))) AS Version,    
            S.Status,  
            SO.OwnerId AS ScoreboardOwnerPositionId,  
            SO.AccessLevel AS ScoreboardAccessLevel,  
            PP.Name AS PerformancePeriodName,    
            STRING_AGG(CAST(SAP.EntityId AS NVARCHAR(MAX)), ',') AS ApplicationPopulation,    
            STRING_AGG(CAST(SRP.RoleProfileId AS NVARCHAR(MAX)), ',') AS OwnerRoleProfiles,    
            S.CreatedBy AS Owner,  
            S.CreatedAt,
            S.FromDate,    
            S.ToDate,  
            LSB.StatusId AS ScoreboardTriggerStatus,  
            LSB.ClosePerformanceDay AS PerformanceClosedDate,  
            LSB.ClosePerformanceMonth AS PerformanceClosedMonth,  
            ROW_NUMBER() OVER (PARTITION BY ScoreboardMasterKey ORDER BY S.ScoreboardId DESC) AS RowNum,  
            CASE   
                WHEN ppd.PerformancePeriodDetailId IS NOT NULL THEN 1   
                ELSE 0   
            END AS OpenDateReached,  
            S.ModifiedBy  
        FROM 
            Scoreboard S    
        LEFT JOIN ScoreboardApplicationPopulation SAP ON SAP.ScoreboardId = S.ScoreboardId AND SAP.IsDeleted = 0    
        LEFT JOIN ScoreboardOwnerRoleProfile SRP ON SRP.ScoreboardId = S.ScoreboardId AND SRP.IsDeleted = 0    
        LEFT JOIN (  
            SELECT 
                ScoreBoardId, 
                StatusId, 
                lsbt.PerformancePeriodDetailId, 
                ppd.ClosePerformanceDay, 
                ppd.ClosePerformanceMonth  
            FROM 
                LatestScoreBoardTrigger lsbt 
            INNER JOIN 
                PerformancePeriodDetail ppd 
            ON 
                lsbt.PerformancePeriodDetailId = ppd.PerformancePeriodDetailId  
            WHERE 
                RowNum = 1  
        ) LSB ON LSB.ScoreboardId = S.ScoreboardId  
        INNER JOIN PerformancePeriod PP ON PP.PerformancePeriodId = S.PerformancePeriodId    
        LEFT JOIN (  
            SELECT   
                PerformancePeriodDetailId,  
                PerformancePeriodId  
            FROM   
                PerformancePeriodDetail  
            WHERE   
                TRY_CAST(CONCAT(YEAR(GETDATE()), '-', RIGHT('0' + CAST(OpenPerformanceMonth AS VARCHAR), 2), '-', RIGHT('0' + CAST(OpenPerformanceDay AS VARCHAR), 2)) AS DATE) <= CAST(GETDATE() AS DATE) 
                AND  
                TRY_CAST(CONCAT(YEAR(GETDATE()), '-', RIGHT('0' + CAST(ClosePerformanceMonth AS VARCHAR), 2), '-', RIGHT('0' + CAST(ClosePerformanceDay AS VARCHAR), 2)) AS DATE) >= CAST(GETDATE() AS DATE)  
        ) ppd ON PP.PerformancePeriodId = ppd.PerformancePeriodId  
        INNER JOIN ScoreboardOwner SO ON SO.ScoreboardId = S.ScoreboardId AND SO.OwnerId = (SELECT TOP 1 Value FROM @TYPE_EmployeeRoleProfileIds) 
        WHERE 
            S.ScoreboardTypeId = 1 AND S.IsDeleted = 0 AND SO.IsDeleted = 0  
            AND (S.CreatedBy = @EmployeeId OR S.ScoreboardId IN (SELECT ScoreboardId FROM ScoreboardsAccess))  
        GROUP BY 
            S.ScoreboardId, S.ScoreboardMasterKey, S.Name, S.Version, S.DraftVersion, S.Status, S.CreatedBy, S.CreatedAt, PP.Name, S.ModifiedAt, S.ModifiedBy,  
            S.FromDate, S.ToDate, SO.OwnerId, SO.AccessLevel, LSB.StatusId, LSB.ClosePerformanceMonth, LSB.ClosePerformanceDay, ppd.PerformancePeriodDetailId  
    ), 
    CreatedAtForMasterKey AS (
        SELECT 
            ScoreboardMasterKey,
            CreatedAt,
			Owner
        FROM (
            SELECT 
                ScoreboardMasterKey,
                CreatedAt,
				Owner,
                ROW_NUMBER() OVER (PARTITION BY ScoreboardMasterKey ORDER BY CreatedAt ASC) AS RowNum
            FROM CTEScoreboard
        ) Ranked
        WHERE RowNum = 1
    )
    SELECT 
        CSD.*, 
        CreatedAtForMasterKey.CreatedAt,
        CreatedAtForMasterKey.Owner
    FROM 
        CTEScoreboard CSD
    INNER JOIN 
        CreatedAtForMasterKey 
    ON 
        CSD.ScoreboardMasterKey = CreatedAtForMasterKey.ScoreboardMasterKey
    WHERE 
        CSD.RowNum = 1
    ORDER BY 
        CSD.ModifiedAt DESC;
END;
GO

DECLARE @RoleProfiles Type_IdsList;

-- Add actual role profile IDs linked to the employee
INSERT INTO @RoleProfiles (Value)
VALUES (3090);  -- Replace with actual role profile IDs

DECLARE @CreatedAt DATETIME = GETDATE();

EXEC [dbo].[GetAllEmployeeScoreboards]
    @EmployeeId = 4,
    @TYPE_EmployeeRoleProfileIds = @RoleProfiles,
    @CreatedBy = 12345,
    @CreatedAt = @CreatedAt,
    @CompanyId = 1;

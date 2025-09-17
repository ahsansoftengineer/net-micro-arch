USE [QA-HCM-ORG-CHART-DB];
SELECT *
  FROM [QA-HCM-ORG-CHART-DB].[dbo].[ActionPermission]
  where MVCPermission like '%,/ScoreboardTrigger/GetParametersAndFactorsByScoreBoardTrigger%'

--EXEC SP_HELPTEXT CheckMVCPermissionPresence
--EXEC SP_HELPTEXT AppendMVCPermissions

--EXEC CheckMVCPermissionPresence
--    @ActionPermissionIDs = '41585,41587',
--    @Route              = 'ScoreboardTrigger/GetParametersAndFactorsByScoreBoardTrigger';

---- Now update:
--EXEC AppendMVCPermissions
--    @ActionPermissionIDs = '41585,41587',
--    @NewRoute           = '/ScoreboardTrigger/GetParametersAndFactorsByScoreBoardTrigger';

--GO;

--ALTER PROCEDURE [dbo].[CheckMVCPermissionPresence]
--    @ActionPermissionIDs VARCHAR(MAX),  -- Comma-separated list of IDs, e.g. '31243,31245,31579'
--    @Route              NVARCHAR(500)   -- The MVC route to check, e.g. '/FormSubmission/GetQuestionsAnalyticsAggregatedMCQs'
--AS
--BEGIN
--    SET NOCOUNT ON;

--    SELECT 
--        ap.ActionPermissionID,
--        CASE 
--            WHEN ap.MVCPermission LIKE '%' + @Route + '%' 
--                THEN 'Present' 
--            ELSE 'Missing' 
--        END AS HasRoute,
--        ap.MVCPermission
--    FROM dbo.ActionPermission AS ap
--    INNER JOIN (
--        SELECT TRY_CAST(value AS INT) AS ActionPermissionID
--        FROM STRING_SPLIT(@ActionPermissionIDs, ',')
--        WHERE TRY_CAST(value AS INT) IS NOT NULL
--    ) AS ids
--      ON ap.ActionPermissionID = ids.ActionPermissionID;
--END
--GO

--CREATE PROCEDURE dbo.AppendMVCPermissions
--    @ActionPermissionIDs VARCHAR(MAX),  -- Comma-separated list of IDs, e.g. '31243,31245,31579'
--    @NewRoute           NVARCHAR(500)   -- The MVC route to append, e.g. '/FormSubmission/GetQuestionsAnalyticsAggregatedMCQs'
--AS
--BEGIN
--    SET NOCOUNT ON;

--    UPDATE [QA-HCM-ORG-CHART-DB].[dbo].[ActionPermission]
--    SET MVCPermission = CASE
--        WHEN MVCPermission IS NULL
--             OR LTRIM(RTRIM(MVCPermission)) = ''
--             THEN @NewRoute
--        WHEN MVCPermission NOT LIKE '%' + @NewRoute + '%'
--             THEN MVCPermission + ',' + @NewRoute
--        ELSE MVCPermission
--    END
--    WHERE ActionPermissionID IN (
--        SELECT TRY_CAST(value AS INT)
--        FROM STRING_SPLIT(@ActionPermissionIDs, ',')
--        WHERE TRY_CAST(value AS INT) IS NOT NULL
--    );
--END
--GO
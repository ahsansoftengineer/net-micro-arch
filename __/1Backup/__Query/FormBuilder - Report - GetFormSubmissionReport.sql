USE [QA-HCM-FORM-BUILDER-DB]
GO
/****** Object:  StoredProcedure [dbo].[GetFormSubmissionReport]    Script Date: 5/20/2025 5:28:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetFormSubmissionReport]
    @FormPublishDetailId INT,
    @FromDate DATE = NULL,
    @ToDate DATE = NULL,
    @CreatedAt DATE = NULL,
    @CreatedBy INT = NULL,
    @CompanyId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Default to current year's start and end if not provided
    IF @FromDate IS NULL
        SET @FromDate = DATEFROMPARTS(YEAR(GETDATE()), 1, 1);

    IF @ToDate IS NULL
        SET @ToDate = DATEFROMPARTS(YEAR(GETDATE()), 12, 31);

    SELECT 
        -- SubmittedBy,
        -- FormPublishId,
        FormPublishDetailId,
        CreatedAt,
        -- YEAR(CreatedAt) AS [Year],
        DATENAME(MONTH, CreatedAt) AS [Month],
        (DATEPART(WEEK, CreatedAt) - DATEPART(WEEK, DATEADD(DAY, -DAY(CreatedAt) + 1, CreatedAt)) + 1) AS [Week],
        DAY(CreatedAt) AS [Day]
    FROM 
        [FormSubmission]
    WHERE 
        FormPublishDetailId = @FormPublishDetailId
        AND IsDeleted = 0
        AND CreatedAt BETWEEN @FromDate AND @ToDate
	ORDER BY CreatedAt ASC
END
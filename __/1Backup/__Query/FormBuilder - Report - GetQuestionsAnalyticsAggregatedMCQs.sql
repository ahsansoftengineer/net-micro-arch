SELECT * FROM [FormSubmission] WHERE FormPublishDetailId = 415

SELECT *  FROM CteElementMaster WHERE CteElementMasterId IN (6,7)
SELECT *  FROM [CteElementProperties] WHERE CteElementPropertyId IN (49, 47, 234, 232)  --Label (49, 47) (Checkbox 234, 232)

SELECT * FROM [FormItems] WHERE FormDraftId = 2329 ORDER BY SequenceNo --AND CteElementMasterId IN (6,7)

SELECT * FROM FormItemSettings WHERE FormItemId IN (14864, 14866, 14869, 14871, 14873, 14874
) AND CteElementPropertyId IN (49, 47, 234, 232)


ALTER PROCEDURE GetQuestionsAnalyticsAggregatedMCQs
    @FormPublishDetailId INT,
	@CreatedAt DATE = NULL,
    @CreatedBy INT = NULL,
    @CompanyId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @FormDraftId INT;

    SELECT TOP 1 @FormDraftId = FormDraftId
    FROM FormSubmission
    WHERE FormPublishDetailId = @FormPublishDetailId; --415

    ;WITH FormItemsCTE AS (
        SELECT FormItemId, FormItemKey, SequenceNo
        FROM FormItems
        WHERE FormDraftId = @FormDraftId
          AND CteElementMasterId IN (6, 7)
    )

    SELECT 
        fi.FormItemId,
        fi.FormItemKey,
		fi.SequenceNo,
        fis.FormItemSettingId,
        fis.CteElementPropertyId,
		CASE fis.CteElementPropertyId
            WHEN 49 THEN 'Label'
            WHEN 234 THEN 'Label'
            WHEN 47 THEN 'Options'
            WHEN 232 THEN 'Options'
            ELSE 'Unknown'
        END AS [Label],
		CASE fis.CteElementPropertyId
            WHEN 49 THEN 7 --Radio
            WHEN 47 THEN 7
            WHEN 234 THEN 6 --Checkbox
            WHEN 232 THEN 6
            ELSE 'Unknown'
        END AS ElementType,
        fis.[Value]
       
    FROM FormItemsCTE fi
    INNER JOIN FormItemSettings fis
        ON fi.FormItemId = fis.FormItemId
    WHERE fis.CteElementPropertyId IN (49, 47, 234, 232)
	ORDER BY fis.FormItemId
END

EXEC GetQuestionsAnalyticsAggregatedMCQs @FormPublishDetailId = 416

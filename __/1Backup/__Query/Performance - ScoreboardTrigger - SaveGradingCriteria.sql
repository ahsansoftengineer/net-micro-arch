USE [QA-HCM-PERFORMANCE-DB]
GO

DROP PROCEDURE [dbo].[SaveGradingCriteria];
DROP TYPE [dbo].[TYPE_GradingCriteriaDetails];

CREATE PROCEDURE [dbo].[SaveGradingCriteria]  
@GradingCriteriaId BIGINT,  
@Name VARCHAR(50),  
@GradingCriteriaTypeId BIGINT,  
@TYPE_GradingCriteriaDetails TYPE_GradingCriteriaDetails READONLY,  
@CreatedBy BIGINT,  
@CreatedAt DATETIME,  
@CompanyId BIGINT,  
@ModifiedBy BIGINT,  
@ModifiedAt DATETIME,  
@IsDeleted BIGINT  
AS  
BEGIN  
   
 IF(@GradingCriteriaId IS NULL)  
 BEGIN  
  INSERT INTO [dbo].[GradingCriteria]  
      ([Name] ,[GradingCriteriaTypeId] ,[CompanyId] ,[CreatedBy], [ModifiedBy] ,[CreatedAt] ,[ModifiedAt] ,[IsDeleted])  
  VALUES  
      (@Name, @GradingCriteriaTypeId, @CompanyId, @CreatedBy, @ModifiedBy, @CreatedAt, @ModifiedAt, @IsDeleted)  
  
  SET @GradingCriteriaId = SCOPE_IDENTITY();  
 END  
  
 ELSE  
   BEGIN      
    IF EXISTS(SELECT * FROM Scoreboard WHERE Grading = @GradingCriteriaId and IsDeleted = 0 and Status in (4) )  
     BEGIN  	      	        
		 RAISERROR('The Grading is Linked with the Scoreboard.',16,1);
		 return
    END     
  
  END 	
  UPDATE GradingCriteria  
  SET Name = @Name, GradingCriteriaTypeId = @GradingCriteriaTypeId, CompanyId = @CompanyId, ModifiedBy = @ModifiedBy, CreatedAt = @CreatedAt,  
  ModifiedAt = @ModifiedAt, IsDeleted = @IsDeleted  
  WHERE GradingCriteriaId = @GradingCriteriaId  
  
  UPDATE GradingCriteriaDetails SET IsDeleted = 1 WHERE GradingCriteriaId = @GradingCriteriaId   
  
  
 INSERT INTO [dbo].[GradingCriteriaDetails]   
 SELECT @GradingCriteriaId, ValueFrom, ValueTo, Grade,GradingOrder, @CompanyId, @ModifiedBy, @ModifiedBy, @CreatedAt, @ModifiedAt, 0  
 FROM @TYPE_GradingCriteriaDetails  
END



GO



USE [QA-HCM-PERFORMANCE-DB]
GO

CREATE TYPE [dbo].[TYPE_GradingCriteriaDetails] AS TABLE(
	[GradingCriteriaDetailId] [bigint] NULL,
	[GradingCriteriaId] [bigint] NULL,
	[ValueFrom] [decimal](8,2) NULL,
	[ValueTo]   [decimal](8,2) NULL,
	[Grade] [varchar](5) NOT NULL,
	[GradingOrder] [int] NULL
);

USE [QA-HCM-PERFORMANCE-DB];
GO

ALTER TABLE [dbo].[GradingCriteriaDetails]
ALTER COLUMN [ValueFrom] NUMERIC(8,2);
GO

ALTER TABLE [dbo].[GradingCriteriaDetails]
ALTER COLUMN [ValueTo] NUMERIC(8,2);
GO


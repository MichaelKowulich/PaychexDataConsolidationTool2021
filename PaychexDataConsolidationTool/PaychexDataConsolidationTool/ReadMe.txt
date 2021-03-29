================================================================================================
EXECUTE BELOW SCRIPT IN	SQL SERVER TO ADD CPS TABLE
================================================================================================

GO
USE [PaychexDataConsolidationTool]
GO

CREATE TABLE [dbo].[CPS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [nvarchar](12) NOT NULL,
	[Status] [nvarchar](250) NOT NULL,
	[Total] [int] NOT NULL,
 CONSTRAINT [PK_CPS] PRIMARY KEY CLUSTERED 
	(
	[ID] ASC
	)
)

INSERT [dbo].[CPS] ([Date], [Status], [Total]) VALUES (N'2021-03-20', N'Inactive', 1)
GO
INSERT [dbo].[CPS] ([Date], [Status], [Total]) VALUES (N'2021-03-20', N'Active', 2)
GO
INSERT [dbo].[CPS] ([Date], [Status], [Total]) VALUES (N'2021-03-20', N'Demo', 3)
GO
INSERT [dbo].[CPS] ([Date], [Status], [Total]) VALUES (N'2021-03-20', N'Master', 4)
GO
INSERT [dbo].[CPS] ([Date], [Status], [Total]) VALUES (N'2021-03-27', N'Inactive', 1)
GO
INSERT [dbo].[CPS] ([Date], [Status], [Total]) VALUES (N'2021-03-27', N'Active', 2)
GO
INSERT [dbo].[CPS] ([Date], [Status], [Total]) VALUES (N'2021-03-27', N'Demo', 3)
GO
INSERT [dbo].[CPS] ([Date], [Status], [Total]) VALUES (N'2021-03-27', N'Master', 4)
GO
INSERT [dbo].[CPS] ([Date], [Status], [Total]) VALUES (N'2021-04-03', N'Inactive', 1)
GO
INSERT [dbo].[CPS] ([Date], [Status], [Total]) VALUES (N'2021-04-03', N'Active', 2)
GO
INSERT [dbo].[CPS] ([Date], [Status], [Total]) VALUES (N'2021-04-03', N'Demo', 3)
GO
INSERT [dbo].[CPS] ([Date], [Status], [Total]) VALUES (N'2021-04-03', N'Master', 4)
GO

CREATE PROCEDURE [dbo].[SP_Add_CPS]    
	@Date NVARCHAR(12),
    @Status NVARCHAR(250), 
	@Total INT
AS    
    BEGIN    
 DECLARE @Id as BIGINT  
        INSERT  INTO [CPS] (Date, Status, Total) VALUES (@Date, @Status, @Total );   
		SET @Id = SCOPE_IDENTITY();   
        SELECT  @Id AS CPSID;    
    END;   
GO	
	
CREATE PROCEDURE [dbo].[SP_Update_CPS] 
		@Id INT,   
		@Date NVARCHAR(12),
		@Status NVARCHAR(250),
		@Total INT
	AS    
		BEGIN    

		UPDATE [CPS] SET Date = @Date WHERE ID = @Id 
		UPDATE [CPS] SET Status = @Status WHERE ID = @Id 
	    UPDATE [CPS] SET Total = @Total WHERE ID = @Id 
      
		END;    

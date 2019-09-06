USE [SteelRepositoryDB]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[number] [nvarchar](max) NULL,
	[password] [nvarchar](max) NULL,
	[permissions] [int] NOT NULL,
	[departmentId] [int] NULL,
	[state] [int] NULL,
 CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[departmentName] [nvarchar](max) NOT NULL,
	[nodeLevel] [int] NULL,
	[parentNodeId] [int] NULL,
 CONSTRAINT [PK_dbo.Departments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BrandCode]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrandCode](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[brandCodeName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_BrandCode] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InCome]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InCome](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[brandCodeId] [int] NULL,
	[categoryId] [int] NOT NULL,
	[codeId] [int] NOT NULL,
	[batch] [nvarchar](max) NOT NULL,
	[menufactureId] [int] NULL,
	[unit] [nvarchar](max) NOT NULL,
	[amount] [float] NOT NULL,
	[unitPrice] [float] NULL,
	[priceMeasure] [nchar](10) NULL,
	[storageTime] [datetime] NOT NULL,
	[operatorId] [int] NOT NULL,
	[reviewCycle] [float] NOT NULL,
 CONSTRAINT [PK_dbo.InComes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OutCome]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OutCome](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[inventoryId] [int] NOT NULL,
	[number] [float] NOT NULL,
	[unit] [nvarchar](max) NOT NULL,
	[recipientsTime] [datetime] NOT NULL,
	[borrowerId] [int] NOT NULL,
	[price] [float] NULL,
	[instructions] [nvarchar](max) NULL,
	[projectId] [int] NULL,
	[state] [int] NULL,
 CONSTRAINT [PK_dbo.OutComes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Name]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Name](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[materialName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Names] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Model]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Model](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[modelName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Models] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialCode]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialCode](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](max) NOT NULL,
	[materialNameId] [int] NOT NULL,
	[materialModelId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.MaterialCodes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manufacturer]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[manufacturersName] [nvarchar](max) NOT NULL,
	[contact] [nvarchar](max) NULL,
	[address] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Manufacturers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UseAmountStatisticals]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UseAmountStatisticals](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[useTime] [datetime] NOT NULL,
	[userUse] [int] NOT NULL,
	[administratorLogin] [int] NOT NULL,
 CONSTRAINT [PK_UseAmountStatisticals] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecheckReportImg]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecheckReportImg](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[reportId] [int] NOT NULL,
	[img] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_RecheckReportImg] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecheckReport]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecheckReport](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[recheckTime] [datetime] NOT NULL,
	[incomeId] [int] NOT NULL,
	[recheckBasis] [nvarchar](max) NULL,
	[recheckOrderNo] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.RecheckReports] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QualityCertificationReportImg]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QualityCertificationReportImg](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[incomeId] [int] NOT NULL,
	[img] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_QualityCertificationReportImg] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Project]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[projectCode] [varchar](max) NOT NULL,
	[projectName] [varchar](max) NOT NULL,
	[projectCharge] [nvarchar](max) NULL,
	[projectNote] [nvarchar](max) NULL,
	[state] [int] NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Position]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[positionName] [nvarchar](max) NOT NULL,
	[note] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Positions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[incomeId] [int] NOT NULL,
	[amount] [float] NOT NULL,
	[unit] [nvarchar](max) NULL,
	[positionId] [int] NOT NULL,
	[consumptionAmount] [float] NOT NULL,
 CONSTRAINT [PK_dbo.Inventories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[InComeView]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[InComeView]
AS
SELECT     dbo.InCome.id AS incoId, dbo.InCome.batch, dbo.InCome.unit, dbo.InCome.amount, dbo.InCome.unitPrice, dbo.InCome.storageTime, dbo.InCome.reviewCycle, dbo.InCome.priceMeasure, 
                      dbo.MaterialCode.code, dbo.MaterialCode.id AS materId, dbo.Category.categoryName, dbo.Model.modelName, dbo.Name.materialName, dbo.Employee.name, dbo.Manufacturer.id AS manuId, 
                      dbo.Manufacturer.manufacturersName, dbo.RecheckReport.recheckTime, dbo.Position.positionName, dbo.BrandCode.brandCodeName
FROM         dbo.InCome INNER JOIN
                      dbo.MaterialCode ON dbo.InCome.codeId = dbo.MaterialCode.id INNER JOIN
                      dbo.Category ON dbo.InCome.categoryId = dbo.Category.id INNER JOIN
                      dbo.Model ON dbo.MaterialCode.materialModelId = dbo.Model.id INNER JOIN
                      dbo.Name ON dbo.MaterialCode.materialNameId = dbo.Name.id INNER JOIN
                      dbo.Employee ON dbo.InCome.operatorId = dbo.Employee.id INNER JOIN
                      dbo.Manufacturer ON dbo.InCome.menufactureId = dbo.Manufacturer.id INNER JOIN
                      dbo.RecheckReport ON dbo.InCome.id = dbo.RecheckReport.incomeId INNER JOIN
                      dbo.Inventory ON dbo.InCome.id = dbo.Inventory.incomeId INNER JOIN
                      dbo.Position ON dbo.Inventory.positionId = dbo.Position.id INNER JOIN
                      dbo.BrandCode ON dbo.InCome.brandCodeId = dbo.BrandCode.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[43] 4[18] 2[37] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "InCome"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 254
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "MaterialCode"
            Begin Extent = 
               Top = 20
               Left = 260
               Bottom = 159
               Right = 446
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Category"
            Begin Extent = 
               Top = 12
               Left = 485
               Bottom = 113
               Right = 660
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Model"
            Begin Extent = 
               Top = 89
               Left = 489
               Bottom = 190
               Right = 650
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Name"
            Begin Extent = 
               Top = 12
               Left = 679
               Bottom = 113
               Right = 850
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employee"
            Begin Extent = 
               Top = 6
               Left = 883
               Bottom = 145
               Right = 1052
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Manufacturer"
            Begin Extent = 
               Top = 6
               Left = 1090
               Bottom = 145
               Right = 1297
            End
            DisplayFlags =' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InComeView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' 280
            TopColumn = 0
         End
         Begin Table = "RecheckReport"
            Begin Extent = 
               Top = 85
               Left = 768
               Bottom = 205
               Right = 930
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Inventory"
            Begin Extent = 
               Top = 197
               Left = 561
               Bottom = 336
               Right = 770
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Position"
            Begin Extent = 
               Top = 163
               Left = 219
               Bottom = 283
               Right = 390
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BrandCode"
            Begin Extent = 
               Top = 150
               Left = 968
               Bottom = 240
               Right = 1137
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 20
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 2880
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InComeView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InComeView'
GO
/****** Object:  View [dbo].[OutcomeQueryView]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[OutcomeQueryView]
AS
SELECT     dbo.OutCome.number, dbo.OutCome.unit, dbo.OutCome.recipientsTime, dbo.OutCome.price, dbo.OutCome.instructions, dbo.InCome.unitPrice, dbo.MaterialCode.code, dbo.Name.materialName, 
                      dbo.Model.modelName, dbo.Category.categoryName, dbo.Employee.name, dbo.OutCome.id AS OutId, dbo.Manufacturer.id AS mfId, dbo.Manufacturer.manufacturersName, 
                      dbo.MaterialCode.id AS codeId, dbo.Department.id AS deparId, dbo.Department.departmentName, dbo.Employee.id AS emploId, dbo.Project.projectName, dbo.Position.positionName, 
                      dbo.OutCome.state, dbo.InCome.priceMeasure
FROM         dbo.Inventory INNER JOIN
                      dbo.OutCome ON dbo.Inventory.id = dbo.OutCome.inventoryId INNER JOIN
                      dbo.InCome ON dbo.Inventory.incomeId = dbo.InCome.id INNER JOIN
                      dbo.MaterialCode ON dbo.InCome.codeId = dbo.MaterialCode.id INNER JOIN
                      dbo.Name ON dbo.MaterialCode.materialNameId = dbo.Name.id INNER JOIN
                      dbo.Model ON dbo.MaterialCode.materialModelId = dbo.Model.id INNER JOIN
                      dbo.Category ON dbo.InCome.categoryId = dbo.Category.id INNER JOIN
                      dbo.Employee ON dbo.OutCome.borrowerId = dbo.Employee.id INNER JOIN
                      dbo.Manufacturer ON dbo.InCome.menufactureId = dbo.Manufacturer.id INNER JOIN
                      dbo.Department ON dbo.Employee.departmentId = dbo.Department.id INNER JOIN
                      dbo.Project ON dbo.OutCome.projectId = dbo.Project.id INNER JOIN
                      dbo.Position ON dbo.Inventory.positionId = dbo.Position.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[25] 4[25] 3[25] 2) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = -15
      End
      Begin Tables = 
         Begin Table = "Inventory"
            Begin Extent = 
               Top = 25
               Left = 424
               Bottom = 226
               Right = 636
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "OutCome"
            Begin Extent = 
               Top = 19
               Left = 662
               Bottom = 267
               Right = 835
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "InCome"
            Begin Extent = 
               Top = 23
               Left = 224
               Bottom = 246
               Right = 397
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "MaterialCode"
            Begin Extent = 
               Top = 42
               Left = 7
               Bottom = 244
               Right = 193
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Name"
            Begin Extent = 
               Top = 6
               Left = 873
               Bottom = 107
               Right = 1044
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Model"
            Begin Extent = 
               Top = 6
               Left = 1082
               Bottom = 107
               Right = 1243
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Category"
            Begin Extent = 
               Top = 6
               Left = 1281
               Bottom = 107
               Right = 1456
            End
            DisplayFlags = ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OutcomeQueryView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'280
            TopColumn = 0
         End
         Begin Table = "Employee"
            Begin Extent = 
               Top = 108
               Left = 873
               Bottom = 247
               Right = 1042
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Manufacturer"
            Begin Extent = 
               Top = 108
               Left = 1080
               Bottom = 247
               Right = 1287
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Department"
            Begin Extent = 
               Top = 228
               Left = 435
               Bottom = 367
               Right = 627
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Project"
            Begin Extent = 
               Top = 98
               Left = 1300
               Bottom = 237
               Right = 1472
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Position"
            Begin Extent = 
               Top = 335
               Left = 1118
               Bottom = 455
               Right = 1289
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 21
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2865
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OutcomeQueryView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OutcomeQueryView'
GO
/****** Object:  View [dbo].[InventoryView]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[InventoryView]
AS
SELECT     dbo.InCome.storageTime, dbo.InCome.batch, dbo.Inventory.unit, dbo.Inventory.amount, dbo.Inventory.id AS InvenId, dbo.Inventory.consumptionAmount, dbo.Category.categoryName, 
                      dbo.MaterialCode.code, dbo.Manufacturer.manufacturersName, dbo.Manufacturer.id AS manuId, dbo.Name.materialName, dbo.Model.modelName, dbo.Position.positionName, 
                      dbo.Position.id AS posiId, dbo.Employee.name
FROM         dbo.InCome INNER JOIN
                      dbo.Inventory ON dbo.InCome.id = dbo.Inventory.incomeId INNER JOIN
                      dbo.Category ON dbo.InCome.categoryId = dbo.Category.id INNER JOIN
                      dbo.MaterialCode ON dbo.InCome.codeId = dbo.MaterialCode.id INNER JOIN
                      dbo.Manufacturer ON dbo.InCome.menufactureId = dbo.Manufacturer.id INNER JOIN
                      dbo.Model ON dbo.MaterialCode.materialModelId = dbo.Model.id INNER JOIN
                      dbo.Name ON dbo.MaterialCode.materialNameId = dbo.Name.id INNER JOIN
                      dbo.Position ON dbo.Inventory.positionId = dbo.Position.id INNER JOIN
                      dbo.Employee ON dbo.InCome.operatorId = dbo.Employee.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[11] 2[32] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "InCome"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 254
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "Inventory"
            Begin Extent = 
               Top = 6
               Left = 249
               Bottom = 229
               Right = 458
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Category"
            Begin Extent = 
               Top = 6
               Left = 496
               Bottom = 107
               Right = 671
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MaterialCode"
            Begin Extent = 
               Top = 6
               Left = 709
               Bottom = 145
               Right = 895
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Manufacturer"
            Begin Extent = 
               Top = 6
               Left = 933
               Bottom = 145
               Right = 1140
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Model"
            Begin Extent = 
               Top = 6
               Left = 1178
               Bottom = 107
               Right = 1339
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Name"
            Begin Extent = 
               Top = 108
               Left = 496
               Bottom = 209
               Right = 667
            End
            DisplayFlags = ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InventoryView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'280
            TopColumn = 0
         End
         Begin Table = "Position"
            Begin Extent = 
               Top = 108
               Left = 1178
               Bottom = 228
               Right = 1349
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employee"
            Begin Extent = 
               Top = 112
               Left = 709
               Bottom = 251
               Right = 878
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InventoryView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InventoryView'
GO
/****** Object:  View [dbo].[EmployeeDepartView]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[EmployeeDepartView]
AS
SELECT   dbo.Department.departmentName, dbo.Employee.id AS emploId, dbo.Employee.name, dbo.Employee.number, 
                dbo.Employee.departmentId
FROM      dbo.Department INNER JOIN
                dbo.Employee ON dbo.Department.id = dbo.Employee.departmentId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Department"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 252
               Right = 230
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employee"
            Begin Extent = 
               Top = 6
               Left = 268
               Bottom = 212
               Right = 437
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EmployeeDepartView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EmployeeDepartView'
GO
/****** Object:  View [dbo].[CodeAmountView]    Script Date: 09/06/2019 09:12:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CodeAmountView]
AS
SELECT   dbo.Inventory.amount, dbo.MaterialCode.code, dbo.Inventory.unit
FROM      dbo.InCome INNER JOIN
                dbo.Inventory ON dbo.InCome.id = dbo.Inventory.incomeId INNER JOIN
                dbo.MaterialCode ON dbo.InCome.codeId = dbo.MaterialCode.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -288
         Left = 0
      End
      Begin Tables = 
         Begin Table = "InCome"
            Begin Extent = 
               Top = 0
               Left = 38
               Bottom = 248
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Inventory"
            Begin Extent = 
               Top = 6
               Left = 249
               Bottom = 218
               Right = 458
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MaterialCode"
            Begin Extent = 
               Top = 6
               Left = 496
               Bottom = 145
               Right = 682
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CodeAmountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CodeAmountView'
GO

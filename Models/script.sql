USE [master]
GO
/****** Object:  Database [SteelRepositoryDB]    Script Date: 2019/7/28 18:20:36 ******/
CREATE DATABASE [SteelRepositoryDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SteelRepositoryDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SteelRepositoryDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SteelRepositoryDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SteelRepositoryDB_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SteelRepositoryDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SteelRepositoryDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SteelRepositoryDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SteelRepositoryDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SteelRepositoryDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SteelRepositoryDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SteelRepositoryDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET RECOVERY FULL 
GO
ALTER DATABASE [SteelRepositoryDB] SET  MULTI_USER 
GO
ALTER DATABASE [SteelRepositoryDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SteelRepositoryDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SteelRepositoryDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SteelRepositoryDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SteelRepositoryDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [SteelRepositoryDB]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2019/7/28 18:20:36 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Department]    Script Date: 2019/7/28 18:20:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[departmentName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Departments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 2019/7/28 18:20:36 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InCome]    Script Date: 2019/7/28 18:20:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InCome](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[categoryId] [int] NOT NULL,
	[codeId] [int] NOT NULL,
	[batch] [nvarchar](max) NOT NULL,
	[menufactureId] [int] NULL,
	[positionId] [int] NOT NULL,
	[unit] [nvarchar](max) NOT NULL,
	[amount] [float] NOT NULL,
	[unitPrice] [float] NULL,
	[priceMeasure] [nchar](10) NULL,
	[storageTime] [datetime] NOT NULL,
	[qualityCertificate] [varbinary](max) NULL,
	[operatorId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.InComes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 2019/7/28 18:20:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[incomeId] [int] NOT NULL,
	[amount] [float] NOT NULL,
	[unit] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Inventories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Manufacturer]    Script Date: 2019/7/28 18:20:36 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MaterialCode]    Script Date: 2019/7/28 18:20:36 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Model]    Script Date: 2019/7/28 18:20:36 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Name]    Script Date: 2019/7/28 18:20:36 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OutCome]    Script Date: 2019/7/28 18:20:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OutCome](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[incomeId] [int] NOT NULL,
	[number] [float] NOT NULL,
	[unit] [nvarchar](max) NOT NULL,
	[recipientsTime] [datetime] NOT NULL,
	[borrowerId] [int] NOT NULL,
	[price] [float] NULL,
	[instructions] [nvarchar](max) NULL,
	[projectId] [int] NULL,
 CONSTRAINT [PK_dbo.OutComes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Position]    Script Date: 2019/7/28 18:20:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[positionName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Positions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Project]    Script Date: 2019/7/28 18:20:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project](
	[id] [int] NOT NULL,
	[projectCode] [varchar](max) NOT NULL,
	[projectName] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecheckReport]    Script Date: 2019/7/28 18:20:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecheckReport](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[recheckTime] [datetime] NOT NULL,
	[incomeId] [int] NOT NULL,
	[inspectionReport] [varbinary](max) NULL,
 CONSTRAINT [PK_dbo.RecheckReports] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [SteelRepositoryDB] SET  READ_WRITE 
GO

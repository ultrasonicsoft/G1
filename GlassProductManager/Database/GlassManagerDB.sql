USE [master]
GO
/****** Object:  Database [GlassManagerDB]    Script Date: 11/14/2013 11:40:01 ******/
CREATE DATABASE [GlassManagerDB] ON  PRIMARY 
( NAME = N'GlassManagerDB', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\GlassManagerDB.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GlassManagerDB_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\GlassManagerDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [GlassManagerDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GlassManagerDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GlassManagerDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [GlassManagerDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [GlassManagerDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [GlassManagerDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [GlassManagerDB] SET ARITHABORT OFF
GO
ALTER DATABASE [GlassManagerDB] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [GlassManagerDB] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [GlassManagerDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [GlassManagerDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [GlassManagerDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [GlassManagerDB] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [GlassManagerDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [GlassManagerDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [GlassManagerDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [GlassManagerDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [GlassManagerDB] SET  DISABLE_BROKER
GO
ALTER DATABASE [GlassManagerDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [GlassManagerDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [GlassManagerDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [GlassManagerDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [GlassManagerDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [GlassManagerDB] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [GlassManagerDB] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [GlassManagerDB] SET  READ_WRITE
GO
ALTER DATABASE [GlassManagerDB] SET RECOVERY SIMPLE
GO
ALTER DATABASE [GlassManagerDB] SET  MULTI_USER
GO
ALTER DATABASE [GlassManagerDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [GlassManagerDB] SET DB_CHAINING OFF
GO
USE [GlassManagerDB]
GO
/****** Object:  Table [dbo].[QuoteMaster]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteMaster](
	[ID] [int] NULL,
	[ClientID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[RequestedShipDate] [datetime] NULL,
	[ShippingID] [int] NULL,
	[SoldToID] [int] NULL,
	[LeadTimeTypeID] [int] NULL,
	[LeadTimeID] [int] NULL,
	[SubTotal] [float] NULL,
	[IsDollar] [bit] NULL,
	[EnergySurcharge] [float] NULL,
	[Discount] [float] NULL,
	[DeliveryCharge] [float] NULL,
	[IsRushOrder] [bit] NULL,
	[RushOrder] [float] NULL,
	[Tax] [float] NULL,
	[GrandTotal] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuoteItems]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteItems](
	[QuoteID] [int] NOT NULL,
	[LineID] [int] NULL,
	[Quantity] [int] NULL,
	[Description] [nvarchar](400) NULL,
	[Dimension] [nvarchar](100) NULL,
	[SqFt] [nvarchar](50) NULL,
	[PricePerUnit] [float] NULL,
	[Total] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuThickness]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuThickness](
	[ID] [int] NOT NULL,
	[Thickness] [nvarchar](50) NULL,
 CONSTRAINT [PK_LuThickness] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (1, N'3/16')
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (2, N'1/4')
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (3, N'3/8')
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (4, N'1/2')
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (5, N'5/8')
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (6, N'3/4')
/****** Object:  Table [dbo].[LuShippingMethods]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuShippingMethods](
	[ID] [int] NOT NULL,
	[Shipping] [nvarchar](50) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[LuShippingMethods] ([ID], [Shipping]) VALUES (1, N'Delivery')
INSERT [dbo].[LuShippingMethods] ([ID], [Shipping]) VALUES (2, N'Pickup')
INSERT [dbo].[LuShippingMethods] ([ID], [Shipping]) VALUES (3, N'Courier')
/****** Object:  Table [dbo].[LuShapes]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuShapes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Shape] [nvarchar](50) NULL,
 CONSTRAINT [PK_LuShapes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[LuShapes] ON
INSERT [dbo].[LuShapes] ([ID], [Shape]) VALUES (1, N'Square')
INSERT [dbo].[LuShapes] ([ID], [Shape]) VALUES (2, N'Triangle')
INSERT [dbo].[LuShapes] ([ID], [Shape]) VALUES (3, N'Quadrilateral')
INSERT [dbo].[LuShapes] ([ID], [Shape]) VALUES (4, N'Parallelogram')
INSERT [dbo].[LuShapes] ([ID], [Shape]) VALUES (5, N'Trapezoid')
INSERT [dbo].[LuShapes] ([ID], [Shape]) VALUES (6, N'Circle')
SET IDENTITY_INSERT [dbo].[LuShapes] OFF
/****** Object:  Table [dbo].[LuMiter]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuMiter](
	[ThicknessID] [int] NOT NULL,
	[Rate] [float] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[LuMiter] ([ThicknessID], [Rate]) VALUES (1, 0.3)
INSERT [dbo].[LuMiter] ([ThicknessID], [Rate]) VALUES (2, 0.35)
INSERT [dbo].[LuMiter] ([ThicknessID], [Rate]) VALUES (3, 0.45)
INSERT [dbo].[LuMiter] ([ThicknessID], [Rate]) VALUES (4, 0.55)
/****** Object:  Table [dbo].[LuLeadTimeType]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuLeadTimeType](
	[ID] [int] NULL,
	[LeadTimeType] [nvarchar](50) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[LuLeadTimeType] ([ID], [LeadTimeType]) VALUES (1, N'Days')
INSERT [dbo].[LuLeadTimeType] ([ID], [LeadTimeType]) VALUES (2, N'Weeks')
INSERT [dbo].[LuLeadTimeType] ([ID], [LeadTimeType]) VALUES (3, N'Months')
/****** Object:  Table [dbo].[LuLeadTime]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuLeadTime](
	[ID] [int] NULL,
	[LeadTime] [nvarchar](50) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (1, N'1')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (2, N'2')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (3, N'3')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (4, N'4')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (5, N'5')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (6, N'6')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (7, N'7')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (8, N'8')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (9, N'9')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (10, N'10')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (11, N'11')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (12, N'12')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (13, N'13')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (14, N'14')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (15, N'15')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (16, N'16')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (17, N'17')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (18, N'18')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (19, N'19')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (20, N'20')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (21, N'21')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (22, N'22')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (23, N'23')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (24, N'24')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (25, N'25')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (26, N'26')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (27, N'27')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (28, N'28')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (29, N'29')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (30, N'30')
INSERT [dbo].[LuLeadTime] ([ID], [LeadTime]) VALUES (31, N'31')
/****** Object:  Table [dbo].[LuLeadSetting]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuLeadSetting](
	[ID] [int] NULL,
	[LeadTimeID] [int] NULL,
	[LeadTypeID] [int] NULL,
	[IsDefaultLeadTime] [bit] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[LuLeadSetting] ([ID], [LeadTimeID], [LeadTypeID], [IsDefaultLeadTime]) VALUES (1, 3, 1, 1)
/****** Object:  Table [dbo].[LuInsulationCost]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuInsulationCost](
	[ID] [int] NULL,
	[Ceiling1] [int] NULL,
	[Cost1] [float] NULL,
	[Ceiling2] [int] NULL,
	[Cost2] [float] NULL,
	[Cost3] [float] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[LuInsulationCost] ([ID], [Ceiling1], [Cost1], [Ceiling2], [Cost2], [Cost3]) VALUES (1, 35, 5, 55, 7, 10)
/****** Object:  Table [dbo].[LuHole]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuHole](
	[ThicknessID] [int] NOT NULL,
	[HoleRate] [float] NOT NULL,
 CONSTRAINT [PK_LuHole] PRIMARY KEY CLUSTERED 
(
	[ThicknessID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[LuHole] ([ThicknessID], [HoleRate]) VALUES (1, 0)
INSERT [dbo].[LuHole] ([ThicknessID], [HoleRate]) VALUES (2, 9.5)
INSERT [dbo].[LuHole] ([ThicknessID], [HoleRate]) VALUES (3, 11.5)
INSERT [dbo].[LuHole] ([ThicknessID], [HoleRate]) VALUES (4, 12.5)
INSERT [dbo].[LuHole] ([ThicknessID], [HoleRate]) VALUES (5, 22.5)
INSERT [dbo].[LuHole] ([ThicknessID], [HoleRate]) VALUES (6, 25)
/****** Object:  Table [dbo].[LuGlass]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuGlass](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GlassType] [nvarchar](100) NULL,
 CONSTRAINT [PK_LuGlass] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[LuGlass] ON
INSERT [dbo].[LuGlass] ([ID], [GlassType]) VALUES (1, N'Clear Glass')
INSERT [dbo].[LuGlass] ([ID], [GlassType]) VALUES (2, N'Bronze/Gray')
INSERT [dbo].[LuGlass] ([ID], [GlassType]) VALUES (3, N'Starphire Glass')
INSERT [dbo].[LuGlass] ([ID], [GlassType]) VALUES (4, N'Single Acid')
INSERT [dbo].[LuGlass] ([ID], [GlassType]) VALUES (5, N'Frosted Wire')
INSERT [dbo].[LuGlass] ([ID], [GlassType]) VALUES (6, N'Low-E')
INSERT [dbo].[LuGlass] ([ID], [GlassType]) VALUES (7, N'Mirror')
INSERT [dbo].[LuGlass] ([ID], [GlassType]) VALUES (8, N'Customer Glass')
SET IDENTITY_INSERT [dbo].[LuGlass] OFF
/****** Object:  Table [dbo].[GlassRates]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlassRates](
	[ID] [int] NULL,
	[ThicknessID] [int] NULL,
	[CutSQFT] [float] NULL,
	[TemperedSQFT] [float] NULL,
	[PolishStraight] [float] NULL,
	[PolishShape] [float] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (1, 1, 1.65, 2.15, 0.08, 0.3)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (1, 2, 1.65, 2.25, 0.08, 0.3)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (1, 3, 3.25, 3.95, 0.13, 0.5)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (1, 4, 3.95, 5.15, 0.15, 0.6)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (2, 2, 3.15, 4.35, 0.08, 0.3)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (2, 3, 8, 10, 0.13, 0.5)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (2, 4, 10, 12, 0.15, 0.6)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (3, 2, 5, 6, 0, 0)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (3, 3, 8, 10, 0, 0)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (3, 4, 10, 12, 0, 0)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (4, 2, 6, 7.5, 0, 0)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (4, 3, 8, 10.5, 0, 0)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (5, 2, 3.7, 4.6, 0, 0)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (6, 2, 3.75, 4.5, 0, 0)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (7, 2, 3.5, 0, 0, 0)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (8, 1, 1.5, 0, 0, 0)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (8, 2, 1.55, 0, 0, 0)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (8, 3, 1.95, 0, 0, 0)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (8, 4, 2.75, 0, 0, 0)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (8, 5, 10.5, 0, 0, 0)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (8, 6, 10.5, 0, 0, 0)
/****** Object:  Table [dbo].[Customer]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [int] NOT NULL,
	[Address] [nvarchar](200) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/14/2013 11:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NULL,
	[Password] [nvarchar](50) NULL,
	[IsAdmin] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([ID], [UserName], [Password], [IsAdmin]) VALUES (1, N'admin', N'admin', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  StoredProcedure [dbo].[SetDefaultLeadTime]    Script Date: 11/14/2013 11:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SetDefaultLeadTime]
	@leadTimeID int,
	@leadTimeTypeID int
AS
BEGIN
	
	SET NOCOUNT ON;
	
	IF NOT EXISTS (SELECT IsDefaultLeadTime FROM LuLeadSetting)
	BEGIN
		INSERT INTO LuLeadSetting(ID,LeadTimeID,LeadTypeID,IsDefaultLeadTime)
		VALUES(1, @leadTimeID, @leadTimeTypeID, 'true');
	END
	ELSE
	BEGIN
		Update LuLeadSetting
		SET LeadTimeID = @leadTimeID, LeadTypeID = @leadTimeTypeID, IsDefaultLeadTime = 'true'
		WHERE ID = 1
	END
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[ResetDefaultLeadTime]    Script Date: 11/14/2013 11:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ResetDefaultLeadTime]
AS
BEGIN
	
	SET NOCOUNT ON;
	
	IF EXISTS (SELECT COUNT(1) FROM LuLeadSetting)
	BEGIN
		Update LuLeadSetting
		SET LeadTimeID = null, LeadTypeID = null, IsDefaultLeadTime = 'false'
		WHERE ID = 1
	END
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetThicknessByGlassID]    Script Date: 11/14/2013 11:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetThicknessByGlassID]
	@GlassID int
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT g.ThicknessID, t.Thickness
	FROM GlassRates g
	INNER JOIN LuThickness t ON t.ID = g.ThicknessID
	WHERE g.ID = @GlassID
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetRates]    Script Date: 11/14/2013 11:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRates]  
 @GlassID int,  
 @ThicknessID int  
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
 SELECT g.CutSQFT, g.TemperedSQFT,g.PolishStraight, g.PolishShape, ISNULL(m.Rate,0) 'MiterRate', 15 'NotchRate', 25 'HingeRate', 35 'PatchRate', h.HoleRate  
 FROM GlassRates g  
 LEFT JOIN LuMiter m ON m.ThicknessID = g.ThicknessID  AND m.ThicknessID = @ThicknessID
 LEFT JOIN LuHole h ON h.ThicknessID = g.ThicknessID
 WHERE g.ID = @GlassID AND g.ThicknessID = @ThicknessID 
   
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetNewQuoteID]    Script Date: 11/14/2013 11:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetNewQuoteID]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @QuoteID INT
	
	SELECT @QuoteID = MAX(ID)FROM QuoteMaster
	
	select ISNULL(@QuoteID,0) +1 'QuoteID'
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetInsulationCost]    Script Date: 11/14/2013 11:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetInsulationCost] 
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT [ID]
      ,[Ceiling1]
      ,[Cost1]
      ,[Ceiling2]
      ,[Cost2]
      ,[Cost3]
  FROM [LuInsulationCost]
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetDefaultLeadTimeSet]    Script Date: 11/14/2013 11:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDefaultLeadTimeSet]
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT lt.ID 'LeadTimeID', lt.LeadTime, ltt.ID 'LeadTimeTypeID', ltt.LeadTimeType, IsDefaultLeadTime
	FROM LuLeadSetting ls
	INNER JOIN LuLeadTime lt ON lt.ID = ls.LeadTimeID
	INNER JOIN LuLeadTimeType ltt ON ltt.ID = ls.LeadTypeID
	WHERE ls.IsDefaultLeadTime = 'TRUE'
		
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllShippingMethods]    Script Date: 11/14/2013 11:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetAllShippingMethods]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT ID, Shipping FROM LuShippingMethods
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllShapes]    Script Date: 11/14/2013 11:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetAllShapes] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT [ID],[Shape] FROM [LuShapes]
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllLeadTimeTypes]    Script Date: 11/14/2013 11:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetAllLeadTimeTypes]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT ID,LeadTimeType  FROM LuLeadTimeType
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllLeadTime]    Script Date: 11/14/2013 11:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetAllLeadTime]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT ID,LeadTime  FROM LuLeadTime
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllGlassTypes]    Script Date: 11/14/2013 11:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllGlassTypes] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT [ID]
      ,[GlassType]
  FROM [GlassManagerDB].[dbo].[LuGlass]
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[IsDefaultLeadTimeSet]    Script Date: 11/14/2013 11:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[IsDefaultLeadTimeSet]
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT IsDefaultLeadTime FROM LuLeadSetting
		
	SET NOCOUNT OFF;

END
GO

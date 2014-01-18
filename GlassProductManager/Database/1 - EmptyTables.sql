USE [master]
GO
/****** Object:  Database [GlassManagerDB]    Script Date: 12/21/2013 19:07:10 ******/
CREATE DATABASE [GlassManagerDB] 
GO

 -- CREATE DATABASE USER
CREATE LOGIN [admin] WITH PASSWORD=N'admin', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF 
GO

EXEC sys.sp_addsrvrolemember @loginame = N'admin', @rolename = N'sysadmin'
GO

USE GlassManagerDB
exec sp_changedbowner 'admin'
GO

USE [GlassManagerDB]
GO
/****** Object:  User [IIS APPPOOL\ASP.NET v4.0]    Script Date: 01/03/2014 15:17:35 ******/
CREATE USER [IIS APPPOOL\ASP.NET v4.0] FOR LOGIN [IIS APPPOOL\ASP.NET v4.0]
GO
USE [GlassManagerDB]
GO
USE [GlassManagerDB]
GO
/****** Object:  Table [dbo].[WorksheetLineItem]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorksheetLineItem](
	[LineID] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[WSNumber] [nvarchar](50) NULL,
	[ModifiedByOperatorID] [nvarchar](50) NULL,
	[ModifiedOn] [datetime] NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NULL,
 CONSTRAINT [PK_WorkshopItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Worksheet]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Worksheet](
	[ID] [int] NULL,
	[WSNumber] [nvarchar](50) NULL,
	[QuoteNumber] [nvarchar](50) NULL,
	[ConfirmedDate] [datetime] NULL,
	[Progress] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01/19/2014 01:31:22 ******/
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
/****** Object:  Table [dbo].[SaleOrder]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleOrder](
	[ID] [int] NULL,
	[SONumber] [nvarchar](50) NULL,
	[QuoteNumber] [nvarchar](50) NULL,
	[ConfirmedDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReprintQueue]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReprintQueue](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[WSNumber] [nvarchar](50) NULL,
	[LineID] [int] NULL,
	[ItemID] [int] NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_ReprintQueue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuoteLineItems]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteLineItems](
	[QuoteNumber] [nvarchar](50) NULL,
	[LineID] [int] NULL,
	[Quantity] [int] NULL,
	[Description] [nvarchar](1000) NULL,
	[Dimension] [nvarchar](50) NULL,
	[SqFt] [int] NULL,
	[PricePerUnit] [float] NULL,
	[Total] [float] NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ActialSqFt] [int] NULL,
	[IsLogo] [bit] NULL,
	[Shape] [nvarchar](50) NULL,
	[IsPolish] [bit] NULL,
	[IsDrills] [bit] NULL,
	[IsWaterjet] [bit] NULL,
	[IsTemper] [bit] NULL,
	[IsInsulate] [bit] NULL,
 CONSTRAINT [PK_QuoteLineItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuoteHeader]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteHeader](
	[CreatedOn] [nvarchar](50) NULL,
	[RequestedShipDate] [nvarchar](50) NULL,
	[CustomerPO] [nvarchar](50) NULL,
	[LeadTimeID] [int] NULL,
	[LeadTimeTypeID] [int] NULL,
	[CustomerID] [int] NULL,
	[ShipToOtherAddress] [bit] NULL,
	[QuoteNumber] [nvarchar](50) NOT NULL,
	[ShippingMethodID] [int] NULL,
	[OperatorName] [nvarchar](50) NULL,
	[PaymentModeID] [int] NULL,
	[QuoteStatusID] [int] NULL,
	[QuoteID] [int] NULL,
 CONSTRAINT [PK_QuoteHeader] PRIMARY KEY CLUSTERED 
(
	[QuoteNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuoteFooter]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteFooter](
	[QuoteNumber] [nvarchar](50) NULL,
	[SubTotal] [float] NULL,
	[IsDollar] [bit] NULL,
	[EnergySurcharge] [float] NULL,
	[Discount] [float] NULL,
	[Delivery] [float] NULL,
	[IsRush] [bit] NULL,
	[RushOrder] [float] NULL,
	[Tax] [float] NULL,
	[GrandTotal] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OtherShippingAddress]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OtherShippingAddress](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QuoteNumber] [nvarchar](50) NULL,
	[CustomerID] [int] NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Address] [nvarchar](300) NULL,
	[Phone] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Misc] [nvarchar](1000) NULL,
 CONSTRAINT [PK_OtherShippingAddress] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MiscRates]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MiscRates](
	[NotchRate] [float] NULL,
	[HingeRate] [float] NULL,
	[PatchRate] [float] NULL,
	[MinimumTotalSqft] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuThickness]    Script Date: 01/19/2014 01:31:22 ******/
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
/****** Object:  Table [dbo].[LuShippingMethods]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuShippingMethods](
	[ID] [int] NOT NULL,
	[Shipping] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuShapes]    Script Date: 01/19/2014 01:31:22 ******/
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
/****** Object:  Table [dbo].[LuQuoteStatus]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuQuoteStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_LuQuoteStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuPaymentTypes]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuPaymentTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_LuPaymentTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuMiter]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuMiter](
	[ThicknessID] [int] NOT NULL,
	[Rate] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuLeadTimeType]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuLeadTimeType](
	[ID] [int] NULL,
	[LeadTimeType] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuLeadTime]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuLeadTime](
	[ID] [int] NULL,
	[LeadTime] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuLeadSetting]    Script Date: 01/19/2014 01:31:22 ******/
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
/****** Object:  Table [dbo].[LuInvoiceTypes]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuInvoiceTypes](
	[ID] [int] NULL,
	[Type] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuInsulationCost]    Script Date: 01/19/2014 01:31:22 ******/
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
/****** Object:  Table [dbo].[LuHole]    Script Date: 01/19/2014 01:31:22 ******/
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
/****** Object:  Table [dbo].[LuGlass]    Script Date: 01/19/2014 01:31:22 ******/
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
/****** Object:  Table [dbo].[LineItemInsulationDetails]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LineItemInsulationDetails](
	[QuoteNumber] [nvarchar](50) NOT NULL,
	[LineID] [int] NOT NULL,
	[GlassType1Index] [int] NULL,
	[Thickness1Index] [int] NULL,
	[IsTemp1] [bit] NULL,
	[Sqft] [int] NULL,
	[Total1] [float] NULL,
	[GlassType2Index] [int] NULL,
	[Thickness2Index] [int] NULL,
	[IsTemp2] [bit] NULL,
	[MaterialCost] [float] NULL,
	[InsulationTier] [float] NULL,
	[InsulationTierTotal] [float] NULL,
	[InsulationTotal] [float] NULL,
	[Total2] [float] NULL,
 CONSTRAINT [PK_LineItemInsulationDetails] PRIMARY KEY CLUSTERED 
(
	[QuoteNumber] ASC,
	[LineID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LineItemDetails]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LineItemDetails](
	[QuoteNumber] [nvarchar](50) NOT NULL,
	[LineID] [int] NOT NULL,
	[SelectedGlassIndex] [int] NULL,
	[SelectedThicknessIndex] [int] NULL,
	[IsLogo] [bit] NULL,
	[IsTempered] [bit] NULL,
	[SelectedShapeIndex] [int] NULL,
	[ActualWidth] [int] NULL,
	[ActualWidthFraction] [nvarchar](50) NULL,
	[ActualHeight] [int] NULL,
	[ActualHeightFraction] [nvarchar](50) NULL,
	[Quantity] [int] NULL,
	[ActualTotalSqft] [int] NULL,
	[ChargedWidth] [int] NULL,
	[ChargedHeight] [int] NULL,
	[ChargedTotal] [int] NULL,
	[StraightTotalPolish] [int] NULL,
	[StraightLongSide] [int] NULL,
	[StraightShortSide] [int] NULL,
	[CustomTotalPolish] [int] NULL,
	[MiterTotalPolish] [int] NULL,
	[MiterLongSide] [int] NULL,
	[MiterShortSide] [int] NULL,
	[Notches] [int] NULL,
	[Patches] [int] NULL,
	[Holes] [int] NULL,
	[Hinges] [int] NULL,
	[CutoutTotal] [float] NULL,
 CONSTRAINT [PK_LineItemDetails] PRIMARY KEY CLUSTERED 
(
	[QuoteNumber] ASC,
	[LineID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LineCutoutDetails]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LineCutoutDetails](
	[QuoteNumber] [nvarchar](50) NOT NULL,
	[LineID] [int] NOT NULL,
	[Quantity] [int] NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[Price] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoicePaymentDetails]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoicePaymentDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QuoteNumber] [nvarchar](50) NULL,
	[PaymentDate] [datetime] NULL,
	[Amount] [float] NULL,
	[Description] [nvarchar](500) NULL,
	[PaymentModeID] [int] NULL,
 CONSTRAINT [PK_InvoicePaymentDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[ID] [int] NULL,
	[QuoteNumber] [nvarchar](50) NULL,
	[InvoiceNumber] [nvarchar](50) NULL,
	[CompletedDate] [datetime] NULL,
	[PaymentStatusID] [int] NULL,
	[PaymentMade] [float] NULL,
	[IsActive] [bit] NULL,
	[BalanceDue] [float] NULL,
	[InvoiceStatusID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GlassRates]    Script Date: 01/19/2014 01:31:22 ******/
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
/****** Object:  Table [dbo].[DBBackupStatus]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DBBackupStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LastBackupTakenOn] [nvarchar](50) NULL,
 CONSTRAINT [PK_DBBackupStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerShippingAddress]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerShippingAddress](
	[ID] [int] NOT NULL,
	[CustomerID] [int] NOT NULL,
	[Address] [nvarchar](200) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Misc] [nvarchar](1000) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 01/19/2014 01:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](200) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Misc] [nvarchar](1000) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

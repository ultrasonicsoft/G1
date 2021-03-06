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
/****** DELETE ALL TABLES FROM DATABASE ******/

EXEC sp_MSforeachtable @command1 = "DROP TABLE ?"
GO
/****** DROP ALL USER DEFINED STORED PROCEDURES ******/
Declare @procName varchar(500) 
Declare cur Cursor For Select [name] From sys.objects where type = 'p' 
Open cur 
Fetch Next From cur Into @procName 
While @@fetch_status = 0 
Begin 
 Exec('drop procedure ' + @procName) 
 Fetch Next From cur Into @procName 
End
Close cur 
Deallocate cur
GO
/****** Object:  Table [dbo].[WorksheetLineItem]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[Worksheet]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[SaleOrder]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[ReprintQueue]    Script Date: 01/26/2014 18:30:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReprintQueue](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[WSNumber] [nvarchar](50) NULL,
	[LineID] [int] NULL,
	[ItemID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_ReprintQueue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuoteLineItems]    Script Date: 01/26/2014 18:30:41 ******/
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
	[ActialSqFt] [float] NULL,
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
/****** Object:  Table [dbo].[QuoteHeader]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[QuoteFooter]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[OtherShippingAddress]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[MiscRates]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[DBBackupStatus]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[CustomerShippingAddress]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[Customer]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[InvoicePaymentDetails]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[Invoice]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[GlassRates]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[LuThickness]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[LuTaxRates]    Script Date: 01/26/2014 18:30:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuTaxRates](
	[TaxRate] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuShippingMethods]    Script Date: 01/26/2014 18:30:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuShippingMethods](
	[ID] [int] NOT NULL,
	[Shipping] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuShapes]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[LuQuoteStatus]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[LuPaymentTypes]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[LuMiter]    Script Date: 01/26/2014 18:30:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuMiter](
	[ThicknessID] [int] NOT NULL,
	[Rate] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuLeadTimeType]    Script Date: 01/26/2014 18:30:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuLeadTimeType](
	[ID] [int] NULL,
	[LeadTimeType] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuLeadTime]    Script Date: 01/26/2014 18:30:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuLeadTime](
	[ID] [int] NULL,
	[LeadTime] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuLeadSetting]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[LuInvoiceTypes]    Script Date: 01/26/2014 18:30:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuInvoiceTypes](
	[ID] [int] NULL,
	[Type] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuInsulationCost]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[LuHole]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[LuGlass]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[LineItemInsulationDetails]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  Table [dbo].[LineItemDetails]    Script Date: 01/26/2014 18:30:41 ******/
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
	[ChargedTotal] [float] NULL,
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
/****** Object:  Table [dbo].[LineCutoutDetails]    Script Date: 01/26/2014 18:30:41 ******/
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
/****** Object:  StoredProcedure [dbo].[IsWorksheetPresent]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[IsWorksheetPresent]
@quoteNumber nvarchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT Count(1) FROM Worksheet where QuoteNumber = @quoteNumber
		
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[IsValidWorksheetLineItem]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IsValidWorksheetLineItem]      
@wsNumber nvarchar(50),
@lineID int,
@itemID int
AS      
BEGIN      
       
 SET NOCOUNT ON;      
      
	IF EXISTS(SELECT WSNumber FROM WorksheetLineItem WHERE WSNumber = @wsNumber AND LineID = @lineID AND ItemID=@itemID)
	BEGIN
		SELECT 'true' 'Status'
	END
	ELSE
	BEGIN
		SELECT 'false' 'Status'
	END
      
      
 SET NOCOUNT OFF;      
      
END
GO
/****** Object:  StoredProcedure [dbo].[IsValidUser]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IsValidUser]
@userName nvarchar(100)
,@password nvarchar(100)
AS
BEGIN
	
	SET NOCOUNT ON;

	IF EXISTS (SELECT * FROM Users WHERE UserName=@userName AND Password = @password)
	BEGIN 
		SELECT 1
	END
	ELSE
	BEGIN
		SELECT 0
	END
		
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[IsSalesOrderPresent]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[IsSalesOrderPresent]
@quoteNumber nvarchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT Count(1) FROM SaleOrder where QuoteNumber = @quoteNumber
		
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[IsQuoteNumberPresent]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[IsQuoteNumberPresent]
@quoteNumber nvarchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT Count(1) FROM QuoteHeader where QuoteNumber = @quoteNumber
		
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[IsInvoicePresent]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[IsInvoicePresent]  
@quoteNumber nvarchar(50)  
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
 SELECT Count(1) FROM Invoice where QuoteNumber = @quoteNumber  
    
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[IsDefaultLeadTimeSet]    Script Date: 01/26/2014 18:30:47 ******/
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
/****** Object:  StoredProcedure [dbo].[GetWorksheetProgress]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetWorksheetProgress]
@wsNumber nvarchar(50)  
AS    
BEGIN    
     
 SET NOCOUNT ON;    
    
	DECLARE @totalLineItems float
	DECLARE @completedLineItems float
	DECLARE @percentage float

	SELECT @totalLineItems = COUNT(ItemID)
	FROM WorksheetLineItem
	WHERE WSNumber = @wsNumber

	SELECT @completedLineItems = COUNT(ItemID)
	FROM WorksheetLineItem
	WHERE Status = 'Completed'

	SET @percentage = (@completedLineItems/@totalLineItems) * 100.0
	
	SELECT @percentage 'Progress'
      
 SET NOCOUNT OFF;    
    
END
GO
/****** Object:  StoredProcedure [dbo].[GetWorksheetNumber]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetWorksheetNumber]  
@QuoteNumber nvarchar(50)
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
 SELECT w.WSNumber FROM Worksheet w
 WHERE w.QuoteNumber = @QuoteNumber
    
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetWorksheetMasterData]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetWorksheetMasterData]
AS
BEGIN
	
	SET NOCOUNT ON;
	
	--DECLARE @TotalQty INT
	--SELECT @TotalQty = SUM(Quantity) FROM QuoteLineItems
	--LEFT JOIN 
	--GROUP BY QuoteNumber
	

	SELECT w.WSNumber, w.ConfirmedDate, w.QuoteNumber,c.LastName + ', ' + c.FirstName 'FullName', qh.RequestedShipDate, w.Progress,SUM(ql.Quantity) 'TotalQuantity'
	FROM Worksheet w
	LEFT JOIN QuoteHeader qh ON qh.QuoteNumber = w.QuoteNumber
	LEFT JOIN Customer c ON c.ID = qh.CustomerID
	LEFT JOIN QuoteLineItems ql ON ql.QuoteNumber = qh.QuoteNumber
	GROUP BY w.WSNumber,w.ConfirmedDate, w.QuoteNumber,w.QuoteNumber,c.LastName, c.FirstName,qh.RequestedShipDate, w.Progress
	
	SET NOCOUNT OFF;

END

--select * from worksheet
--select * from QuoteHeader
--select * from QuoteLineItems
GO
/****** Object:  StoredProcedure [dbo].[GetWorksheetLineItemDetails]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetWorksheetLineItemDetails]    
@wsNumber nvarchar(50)  
,@lineID int
AS    
BEGIN    
     
 SET NOCOUNT ON;    
    
	 SELECT ID, LineID, Status, ModifiedByOperatorID 'OperatorName', ModifiedOn,ItemID
	 FROM WorksheetLineItem
	 WHERE WSNumber = @wsNumber AND LineID = @lineID
      
 SET NOCOUNT OFF;    
    
END
GO
/****** Object:  StoredProcedure [dbo].[GetWorksheetItemDetails]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetWorksheetItemDetails]  
@itemID int
,@lineID int
,@wsNumber nvarchar(50)
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
  SELECT wi.ID, wi.Status, qi.Description, qi.Quantity, qi.IsPolish, qi.IsDrills,qi.IsWaterjet, qi.IsTemper, qi.IsInsulate
  FROM WorksheetLineItem wi
  LEFT JOIN Worksheet w on W.WSNumber = WI.WSNumber
  LEFT JOIN QuoteLineItems qi ON qi.QuoteNumber = w.QuoteNumber AND qi.LineID =  wi.LineID
  WHERE wi.ItemID = @itemID AND wi.LineID = @lineID AND wi.WSNumber = @wsNumber
   
  
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetWorkItemDetails]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetWorkItemDetails]
@wsNumber nvarchar(100)
,@lineID int
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @quoteNumber nvarchar(50)
	
	SELECT @quoteNumber=w.QuoteNumber FROM Worksheet w
	WHERE w.WSNumber = @wsNumber
	
	SELECT * FROM QuoteLineItems qi
	WHERE qi.QuoteNumber= @quoteNumber AND qi.LineID = @lineID
		
	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[GetThicknesses]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetThicknesses]
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
   SELECT [ID]
      ,[Thickness]
  FROM [GlassManagerDB].[dbo].[LuThickness]
  
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetThicknessByGlassID]    Script Date: 01/26/2014 18:30:47 ******/
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
/****** Object:  StoredProcedure [dbo].[GetTaxRates]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetTaxRates]
AS
BEGIN
	SELECT TaxRate FROM LuTaxRates
END
GO
/****** Object:  StoredProcedure [dbo].[GetSpecificBarcodeDetail]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSpecificBarcodeDetail]      
@wsNumber nvarchar(50),
@lineID int
AS      
BEGIN      
       
 SET NOCOUNT ON;      
      
	DECLARE @QuoteNumber nvarchar(50)
	
	SELECT @QuoteNumber = QuoteNumber FROM Worksheet where WSNumber = @wsNumber
	
	SELECT ISNULL(c.LastName,'') LastName, ISNULL(c.FirstName,'')FirstName,    
	so.SONumber,    
	w.WSNumber,    
	qh.CreatedOn, qh.CustomerPO, qh.RequestedShipDate,  
	qi.LineID 'LineID', qi.Description, qi.ActialSqFt 'SqFt', qi.Dimension , qi.IsLogo 'IsLogo', qi.Quantity, qi.Shape  
	FROM QuoteHeader qh    
	LEFT JOIN Customer c ON c.ID = qh.CustomerID    
	LEFT JOIN SaleOrder so ON so.QuoteNumber = qh.QuoteNumber    
	LEFT JOIN Worksheet w ON w.QuoteNumber = qh.QuoteNumber    
	LEFT JOIN QuoteLineItems qi ON qi.QuoteNumber = qh.QuoteNumber          
	WHERE qh.QuoteNumber = @QuoteNumber AND qi.LineID = @lineID
      
      
 SET NOCOUNT OFF;      
      
END
GO
/****** Object:  StoredProcedure [dbo].[GetSmartSearchData]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSmartSearchData]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT FirstName + ' ' + LastName +  ' - ' + q.QuoteNumber +
		(Case When so.SONumber IS NULL THEN '' ELSE ' - ' + so.SONumber END ) + 
		(Case When ws.WSNumber IS NULL THEN '' ELSE ' - ' + ws.WSNumber END ) + 
		(Case When inv.InvoiceNumber IS NULL THEN '' ELSE ' - ' + inv.InvoiceNumber END ) +
		(Case When Phone IS NULL THEN '' ELSE ' - ' + Phone END )  'Item'
	FROM QuoteHeader q
	LEFT JOIN Customer c ON q.CustomerID = c.ID
	LEFT JOIN SaleOrder so ON so.QuoteNumber = q.QuoteNumber
	LEFT JOIN Worksheet ws ON ws.QuoteNumber = q.QuoteNumber
	LEFT JOIN Invoice inv ON inv.QuoteNumber = q.QuoteNumber AND inv.IsActive ='true'
	
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetSaleOrderNumber]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSaleOrderNumber]
@QuoteNumber nvarchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT SONumber FROM SaleOrder WHERE QuoteNumber = @QuoteNumber

	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetSaleOrderMasterData]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSaleOrderMasterData]
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT so.SONumber, qh.QuoteNumber, c.LastName + ', ' + c.FirstName 'FullName', so.ConfirmedDate,
		qf.GrandTotal, p.Type 'PaymentType', ws.WSNumber, qh.CustomerPO
	FROM SaleOrder so
	LEFT JOIN QuoteHeader qh ON qh.QuoteNumber = so.QuoteNumber
	LEFT JOIN QuoteFooter qf ON qf.QuoteNumber = so.QuoteNumber
	LEFT JOIN Customer c ON c.ID = qh.CustomerID
	LEFT JOIN LuPaymentTypes p ON p.ID = qh.PaymentModeID
	LEFT JOIN Worksheet ws ON ws.QuoteNumber = qh.QuoteNumber
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetRates]    Script Date: 01/26/2014 18:30:47 ******/
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
    
 SELECT g.CutSQFT, g.TemperedSQFT,g.PolishStraight, g.PolishShape, ISNULL(m.Rate,0) 'MiterRate',
	 mr.NotchRate 'NotchRate', mr.HingeRate 'HingeRate', mr.PatchRate 'PatchRate', h.HoleRate,
	 mr.MinimumTotalSqft    
 FROM GlassRates g    
 LEFT JOIN LuMiter m ON m.ThicknessID = g.ThicknessID  AND m.ThicknessID = @ThicknessID  
 LEFT JOIN LuHole h ON h.ThicknessID = g.ThicknessID  
 LEFT JOIN MiscRates mr ON g.ID = g.ID  
 WHERE g.ID = @GlassID AND g.ThicknessID = @ThicknessID   
     
 SET NOCOUNT OFF;    
    
END
GO
/****** Object:  StoredProcedure [dbo].[GetQuoteMasterData]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetQuoteMasterData]
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT qs.Type 'Status', qh.QuoteNumber, c.LastName + ', ' +c.FirstName 'FullName', qh.CreatedOn, qf.GrandTotal 'Total',
		qh.RequestedShipDate 'EstimatedShipDate', p.Type 'PaymentType', qh.CustomerPO 'CustomerPONumber'
	FROM QuoteHeader qh
	LEFT JOIN LuQuoteStatus qs ON qs.ID = qh.QuoteStatusID
	LEFT JOIN Customer c ON c.ID = qh.CustomerID
	LEFT JOIN QuoteFooter qf ON qf.QuoteNumber = qh.QuoteNumber
	LEFT JOIN LuPaymentTypes p ON p.ID = qh.PaymentModeID
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetQuoteDetails]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetQuoteDetails]  
@QuoteNumber nvarchar(50)  
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
 -- Quote Header  
 SELECT qh.QuoteNumber, qh.CreatedOn, qh.CustomerPO,qh.LeadTimeID, qh.LeadTimeTypeID, qh.RequestedShipDate, qh.ShipToOtherAddress,  
   qh.ShippingMethodID,qh.OperatorName,qh.PaymentModeID,qh.QuoteStatusID,  
   c.FirstName 'SoldTo_FirstName', c.LastName 'SoldTo_LastName', c.Address 'SoldTo_Address', c.Email 'SoldTo_Email', c.Fax 'SoldTo_Fax', c.Phone 'SoldTo_Phone', c.Misc 'SoldTo_Misc',  
   osa.FirstName 'ShipTo_FirstName', osa.LastName 'ShipTo_LastName', osa.Address 'ShipTo_Address', osa.Email 'ShipTo_Email', osa.Fax 'ShipTo_Fax', osa.Phone 'ShipTo_Phone', osa.Misc 'ShipTo_Misc',  
   so.SONumber, so.ConfirmedDate,  
   ws.WSNumber, ws.ConfirmedDate,
   ISNULL(inv.InvoiceNumber,'') 'InvoiceNumber', ISNULL(inv.BalanceDue,0) 'BalanceDue', ISNULL(inv.CompletedDate,'') 'CompletedDate'
 FROM QuoteHeader qh  
 LEFT JOIN Customer c ON c.ID = qh.CustomerID  
 LEFT JOIN OtherShippingAddress osa ON osa.CustomerID = c.ID  
 LEFT JOIN SaleOrder so ON so.QuoteNumber = qh.QuoteNumber  
 LEFT JOIN Worksheet ws ON ws.QuoteNumber = qh.QuoteNumber  
 LEFT JOIN Invoice inv ON inv.QuoteNumber = qh.QuoteNumber AND inv.IsActive = 'true'
 WHERE qh.QuoteNumber = @QuoteNumber  
   
 -- Line Items  
 SELECT QuoteNumber,LineID,Quantity,Description,Dimension,SqFt,PricePerUnit,Total  
 FROM QuoteLineItems  
 WHERE QuoteNumber = @QuoteNumber  
   
 -- Quote Footer  
 SELECT QuoteNumber, SubTotal, IsDollar, EnergySurcharge,Discount,Delivery,IsRush, RushOrder,Tax,GrandTotal  
 FROM QuoteFooter  
 WHERE QuoteNumber = @QuoteNumber  
 SET NOCOUNT OFF;  
  
END  
  
--[GetQuoteDetails] 'Q00002'
GO
/****** Object:  StoredProcedure [dbo].[GetPrintQueueNotificationCount]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetPrintQueueNotificationCount]
AS
BEGIN
	SELECT COUNT(*) 'TotalCount' FROM ReprintQueue
END
GO
/****** Object:  StoredProcedure [dbo].[GetPrintJobQueue]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetPrintJobQueue]  
AS  
BEGIN  
 SELECT [ID]  
      ,[WSNumber]  
      ,[LineID]  
      ,[ItemID]  
      ,UserName
  FROM[dbo].[ReprintQueue]  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetNewSaleOrderID]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetNewSaleOrderID]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @SaleOrderID INT
	DECLARE @Padding INT = 5
	DECLARE @NewSaleOrderID NVARCHAR(50)
	
	SELECT @SaleOrderID = MAX(ID)  FROM SaleOrder
	SET @SaleOrderID = ISNULL(@SaleOrderID,0) +1
	
	IF @Padding < LEN(@SaleOrderID)
	BEGIN
		SET @Padding = LEN(@SaleOrderID)
	END
	
	SET @NewSaleOrderID = 'S' + Replace(Str(@SaleOrderID, @Padding), ' ' , '0')
	
	SELECT @NewSaleOrderID 
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetNewQuoteID]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetNewQuoteID]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @QuoteID INT
	DECLARE @Padding INT = 5
	DECLARE @NewQuoteID NVARCHAR(50)
	
	SELECT @QuoteID = MAX(QuoteID)  FROM QuoteHeader
	SET @QuoteID = ISNULL(@QuoteID,0) +1
	
	IF @Padding < LEN(@QuoteID)
	BEGIN
		SET @Padding = LEN(@QuoteID)
	END
	
	SET @NewQuoteID = 'Q' + Replace(Str(@QuoteID, @Padding), ' ' , '0')
	
	SELECT @NewQuoteID 
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetMiscRates]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMiscRates]  
AS    
BEGIN    
     
 SET NOCOUNT ON;    
    
   SELECT [NotchRate]  
      ,[HingeRate]  
      ,[PatchRate]
      ,[MinimumTotalSqft]  
  FROM [GlassManagerDB].[dbo].[MiscRates]  
    
 SET NOCOUNT OFF;    
    
END
GO
/****** Object:  StoredProcedure [dbo].[GetLineItemInformation]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetLineItemInformation]
@quoteNumber nvarchar(50)
,@lineID int
AS
BEGIN
	SELECT li.QuoteNumber
      ,li.LineID
      ,SelectedGlassIndex
      ,SelectedThicknessIndex
      ,IsLogo
      ,IsTempered
      ,SelectedShapeIndex
      ,ActualWidth
      ,ActualWidthFraction
      ,ActualHeight
      ,ActualHeightFraction
      ,li.Quantity
      ,ActualTotalSqft
      ,ChargedWidth
      ,ChargedHeight
      ,ChargedTotal
      ,StraightTotalPolish
      ,StraightLongSide
      ,StraightShortSide
      ,CustomTotalPolish
      ,MiterTotalPolish
      ,MiterLongSide
      ,MiterShortSide
      ,Notches
      ,Patches
      ,Holes
      ,Hinges
      ,CutoutTotal
  FROM dbo.LineItemDetails li
  WHERE li.QuoteNumber = @quoteNumber AND li.LineID = @lineID

	SELECT QuoteNumber
		  ,LineID
		  ,Quantity
		  ,Width
		  ,Height
		  ,Price
	  FROM GlassManagerDB.dbo.LineCutoutDetails
	  WHERE QuoteNumber = @quoteNumber AND LineID = @lineID

	SELECT lid.QuoteNumber
		  ,lid.LineID
		  ,GlassType1Index
		  ,Thickness1Index
		  ,IsTemp1
		  ,lid.Sqft
		  ,lid.Total1
		  ,lid.Total2
		  ,GlassType2Index
		  ,Thickness2Index
		  ,IsTemp2
		  ,MaterialCost
		  ,InsulationTier
		  ,InsulationTierTotal
		  ,InsulationTotal
		  ,qi.IsInsulate
	  FROM GlassManagerDB.dbo.LineItemInsulationDetails lid
	  LEFT JOIN QuoteLineItems qi ON qi.QuoteNumber = lid.QuoteNumber AND qi.LineID = lid.LineID
	  WHERE lid.QuoteNumber = @quoteNumber AND lid.LineID = @lineID
END
GO
/****** Object:  StoredProcedure [dbo].[GetInvoicePaymentDetails]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetInvoicePaymentDetails] 
@QuoteNumber nvarchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT ipd.[ID]
		  ,ipd.[QuoteNumber]
		  ,[PaymentDate]
		  ,[Amount]
		  ,[Description]
	  FROM [InvoicePaymentDetails] ipd
	  LEFT JOIN Invoice inv ON inv.QuoteNumber = ipd.QuoteNumber AND inv.IsActive = 'true'
	  WHERE ipd.QuoteNumber = @QuoteNumber

	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceMasterData]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetInvoiceMasterData]    
AS    
BEGIN    
     
 SET NOCOUNT ON;    
    
 SELECT i.InvoiceNumber, qh.QuoteNumber, c.LastName + ', ' + c.FirstName 'FullName', i.CompletedDate,    
  qf.GrandTotal 'Total', p.Type 'PaymentType', qh.CustomerPO, i.BalanceDue, it.Type 'InvoiceStatus',  
  so.SONumber
 FROM Invoice i    
 LEFT JOIN QuoteHeader qh ON qh.QuoteNumber = i.QuoteNumber    
 LEFT JOIN QuoteFooter qf ON qf.QuoteNumber = i.QuoteNumber    
 LEFT JOIN Customer c ON c.ID = qh.CustomerID    
 LEFT JOIN LuPaymentTypes p ON p.ID = qh.PaymentModeID    
 LEFT JOIN LuInvoiceTypes it ON it.ID = i.InvoiceStatusID  
 LEFT JOIN SaleOrder so ON so.QuoteNumber = i.QuoteNumber
   
 SET NOCOUNT OFF;    
    
END
GO
/****** Object:  StoredProcedure [dbo].[GetInsulationCost]    Script Date: 01/26/2014 18:30:47 ******/
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
/****** Object:  StoredProcedure [dbo].[GetHoleRateByThicknessID]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetHoleRateByThicknessID]
@ThicknessID int
AS  
BEGIN  
   
 SET NOCOUNT ON;  
 
  SELECT  [HoleRate]
  FROM [GlassManagerDB].[dbo].[LuHole]
  WHERE ThicknessID = @ThicknessID
  
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetDefaultLeadTimeSet]    Script Date: 01/26/2014 18:30:47 ******/
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
/****** Object:  StoredProcedure [dbo].[GetCustomerID]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCustomerID]  
@QuoteNumber nvarchar(50)  
AS  
BEGIN  
   
 SET NOCOUNT ON;  
 
 SELECT CustomerID 'ID' FROM QuoteHeader
 WHERE QuoteNumber = @QuoteNumber 
   
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerDetails]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCustomerDetails]
@CustomerID int
AS
BEGIN
	
	SET NOCOUNT ON;

	-- Quote Header
	SELECT	 c.FirstName 'SoldTo_FirstName', c.LastName 'SoldTo_LastName', c.Address 'SoldTo_Address', c.Email 'SoldTo_Email', c.Fax 'SoldTo_Fax', c.Phone 'SoldTo_Phone', c.Misc 'SoldTo_Misc',
			ISNULL(osa.FirstName ,'')'ShipTo_FirstName', ISNULL(osa.LastName ,'')'ShipTo_LastName', ISNULL(osa.Address ,'')'ShipTo_Address', ISNULL(osa.Email ,'')'ShipTo_Email',
			ISNULL(osa.Fax ,'')'ShipTo_Fax' ,ISNULL(osa.Phone ,'')'ShipTo_Phone',ISNULL(osa.Misc ,'')'ShipTo_Misc'
			--osa.FirstName, osa.LastName, osa.Address, osa.Email, c.Fax, osa.Phone, osa.Misc
	FROM Customer c 
	LEFT JOIN OtherShippingAddress osa ON osa.CustomerID = c.ID
	WHERE c.ID= @CustomerID
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetBarcodeDetails]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBarcodeDetails]    
@QuoteNumber nvarchar(50)    
AS    
BEGIN    
     
 SET NOCOUNT ON;    
    
  SELECT ISNULL(c.LastName,'') LastName, ISNULL(c.FirstName,'')FirstName,  
  so.SONumber,  
  w.WSNumber,  
  qh.CreatedOn, qh.CustomerPO, qh.RequestedShipDate,
  qi.LineID 'LineID', qi.Description, qi.ActialSqFt 'SqFt', qi.Dimension , qi.IsLogo 'IsLogo', qi.Quantity, qi.Shape
  FROM QuoteHeader qh  
  LEFT JOIN Customer c ON c.ID = qh.CustomerID  
  LEFT JOIN SaleOrder so ON so.QuoteNumber = qh.QuoteNumber  
  LEFT JOIN Worksheet w ON w.QuoteNumber = qh.QuoteNumber  
  LEFT JOIN QuoteLineItems qi ON qi.QuoteNumber = qh.QuoteNumber  
    
  WHERE qh.QuoteNumber = @QuoteNumber  
    
    
 SET NOCOUNT OFF;    
    
END    
    
    
--GetBarcodeDetails   'Q00001'
GO
/****** Object:  StoredProcedure [dbo].[GetAllWorksheetNumbers]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetAllWorksheetNumbers]   
   
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
 SELECT QuoteNumber 'ID', WSNumber 'Type' FROM Worksheet
 
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllShippingMethods]    Script Date: 01/26/2014 18:30:47 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllShapes]    Script Date: 01/26/2014 18:30:47 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllSalesOrderNumbers]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetAllSalesOrderNumbers]   
   
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
 SELECT QuoteNumber 'ID', SONumber 'Type' FROM SaleOrder
 
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllQuoteStatus]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetAllQuoteStatus]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT ID,Type  FROM LuQuoteStatus
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllQuoteNumbers]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllQuoteNumbers]   
   
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
 SELECT QuoteID 'ID', QuoteNumber 'Type' FROM QuoteHeader
 
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllPriceData]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllPriceData]
AS
BEGIN

	SELECT lg.GlassType, t.Thickness, ISNULL(g.CutSQFT,0) 'CutSQFT',
	 ISNULL(g.TemperedSQFT,0) 'TemperedSQFT',
	 ISNULL(g.PolishStraight,0) 'PolishStraight',
	 ISNULL(g.PolishShape,0) 'PolishShape',
	 ISNULL(m.Rate,0) 'MiterRate'
	FROM GlassRates g
	LEFT JOIN LuGlass lg ON lg.ID = g.ID
	LEFT JOIN LuThickness t ON t.ID = g.ThicknessID
	LEFT JOIN LuMiter m ON m.ThicknessID = g.ThicknessID

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllPaymentTypes]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetAllPaymentTypes]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT ID,Type  FROM LuPaymentTypes
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllOperatorNames]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetAllOperatorNames] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT [ID]
      ,UserName
  FROM Users
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllLeadTimeTypes]    Script Date: 01/26/2014 18:30:47 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllLeadTime]    Script Date: 01/26/2014 18:30:47 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllInvoiceTypes]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetAllInvoiceTypes]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT ID, Type FROM LuInvoiceTypes
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllInvoiceNumbers]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetAllInvoiceNumbers]   
   
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
 SELECT QuoteNumber 'ID', InvoiceNumber 'Type' FROM Invoice
 
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllInsulationCost]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetAllInsulationCost]  
 
AS  
BEGIN  
   
 SET NOCOUNT ON;  
	
	SELECT [ID]
      ,[Ceiling1]
      ,[Cost1]
      ,[Ceiling2]
      ,[Cost2]
      ,[Cost3]
  FROM [GlassManagerDB].[dbo].[LuInsulationCost]
	
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllGlassTypes]    Script Date: 01/26/2014 18:30:47 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllCustomers]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetAllCustomers]  
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
 SELECT  c.ID, c.Address,c.FirstName,c.LastName,c.Phone,c.Fax,c.Email,c.Misc
 FROM Customer c
 
 WHERE c.IsActive = 'true'  
   
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllCustomerNames]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllCustomerNames]  
   
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
 SELECT c.ID, FirstName + ' ' + LastName +  +  (Case When len(Phone) = 0 THEN '' ELSE ' - ' + Phone END )  'Item'  
 FROM Customer c  
 WHERE c.IsActive = 'true'  
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllCustomerDetails]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllCustomerDetails]  
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
 SELECT  c.ID, c.Address,c.FirstName,c.LastName,c.Phone,c.Fax,c.Email,c.Misc,ISNULL(qh.QuoteNumber,'') 'QuoteNumber',  
 ISNULL(so.SONumber,'') 'SONumber',ISNULL(ws.WSNumber,'') 'WorksheetNumber', ISNULL(qh.CustomerPO,'') 'PONumber', 
 ISNULL(i.InvoiceNumber,'') 'InvoiceNumber'  
 FROM QuoteHeader qh 
 LEFT JOIN Customer c ON qh.CustomerID = c.ID  
 LEFT JOIN SaleOrder so ON so.QuoteNumber = qh.QuoteNumber
 LEFT JOIN Worksheet ws ON ws.QuoteNumber = qh.QuoteNumber
 LEFT JOIN Invoice i ON i.QuoteNumber = qh.QuoteNumber
 WHERE c.IsActive = 'true'  
   
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GenerateWorksheetItems]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenerateWorksheetItems]  
@itemID int
,@lineID int
,@status nvarchar(50)
,@wsNumber nvarchar(50)
,@modifiedByOperatorID nvarchar(50)
,@modifiedOn datetime
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
 INSERT INTO [dbo].[WorksheetLineItem]
           ([ItemID]
           ,[LineID]
           ,[Status]
           ,[WSNumber]
           ,[ModifiedByOperatorID]
           ,[ModifiedOn]
           )
     VALUES
           (@itemID
           ,@lineID
           ,@status
           ,@wsNumber
           ,@modifiedByOperatorID
           ,@modifiedOn)
  
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[GenerateWorksheet]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenerateWorksheet]
@QuoteNumber nvarchar(50),
@ConfirmedDate datetime
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @WorksheetID INT
	DECLARE @Padding INT = 5
	DECLARE @NewWorksheetID NVARCHAR(50)
	
	SELECT @WorksheetID = MAX(ID)  FROM Worksheet
	SET @WorksheetID = ISNULL(@WorksheetID,0) +1
	
	IF @Padding < LEN(@WorksheetID)
	BEGIN
		SET @Padding = LEN(@WorksheetID)
	END
	
	SET @NewWorksheetID = 'W' + Replace(Str(@WorksheetID, @Padding), ' ' , '0')
	
	INSERT INTO [GlassManagerDB].[dbo].Worksheet
           ([ID]
           ,WSNumber
           ,[QuoteNumber]
           ,[ConfirmedDate])
     VALUES
           (@WorksheetID
           ,@NewWorksheetID
           ,@QuoteNumber
           ,@ConfirmedDate)


	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GenerateSaleOrder]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenerateSaleOrder]
@QuoteNumber nvarchar(50),
@ConfirmedDate datetime
AS
BEGIN
	
	SET NOCOUNT ON;

	-- Update Quote status to Confirmed 
	UPDATE QuoteHeader
	SET QuoteStatusID = 2
	WHERE QuoteNumber = @QuoteNumber
	
	DECLARE @SaleOrderID INT
	DECLARE @Padding INT = 5
	DECLARE @NewSaleOrderID NVARCHAR(50)
	
	-- Generate New Sale Order ID
	SELECT @SaleOrderID = MAX(ID)  FROM SaleOrder
	SET @SaleOrderID = ISNULL(@SaleOrderID,0) +1
	
	IF @Padding < LEN(@SaleOrderID)
	BEGIN
		SET @Padding = LEN(@SaleOrderID)
	END
	
	SET @NewSaleOrderID = 'S' + Replace(Str(@SaleOrderID, @Padding), ' ' , '0')
	
	
	-- Insert new record in Sale ORder
	INSERT INTO [GlassManagerDB].[dbo].[SaleOrder]
           ([ID]
           ,[SONumber]
           ,[QuoteNumber]
           ,[ConfirmedDate])
     VALUES
           (@SaleOrderID
           ,@NewSaleOrderID
           ,@QuoteNumber
           ,@ConfirmedDate)


	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GenerateInvoice]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenerateInvoice]
@QuoteNumber nvarchar(50),
@CompletedDate datetime
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @InvoiceID INT
	DECLARE @Padding INT = 5
	DECLARE @NewInvoiceID NVARCHAR(50)
	
	-- Generate New Invoice ID
	SELECT @InvoiceID = MAX(ID)  FROM Invoice
	SET @InvoiceID = ISNULL(@InvoiceID,0) +1
	
	IF @Padding < LEN(@InvoiceID)
	BEGIN
		SET @Padding = LEN(@InvoiceID)
	END
	
	SET @NewInvoiceID = 'Inv' + Replace(Str(@InvoiceID, @Padding), ' ' , '0')
	
	DECLARE @balanceDue float
	
	SELECT @balanceDue = GrandTotal
	From QuoteFooter
	WHERE QuoteNumber = @QuoteNumber
	
	-- Insert new record in Sale ORder
	INSERT INTO [GlassManagerDB].[dbo].Invoice
           ([ID]
           ,InvoiceNumber
           ,[QuoteNumber]
           ,CompletedDate
           ,PaymentStatusID
           ,BalanceDue
           ,IsActive)
     VALUES
           (@InvoiceID
           ,@NewInvoiceID
           ,@QuoteNumber
           ,@CompletedDate
           ,1
           ,@balanceDue
           ,'true')


	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteWorksheet]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteWorksheet]  
@quoteNumber nvarchar(50)  
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
	DECLARE @wsNumber nvarchar(50)
	SELECT @wsNumber = WSNumber FROM Worksheet WHERE QuoteNumber = @quoteNumber
	
	DELETE FROM Worksheet where QuoteNumber = @quoteNumber  
	DELETE FROM SaleOrder where QuoteNumber = @quoteNumber  
	DELETE FROM WorksheetLineItem WHERE WSNumber = @wsNumber  
	DELETE FROM Invoice where QuoteNumber = @quoteNumber
   
 --UPDATE Invoice  
 --SET IsActive = 'false'  
 --WHERE QuoteNumber = @quoteNumber  
   
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteSalesOrder]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSalesOrder]  
@quoteNumber nvarchar(50)  
AS  
BEGIN  
   
 SET NOCOUNT ON;  
 
	DECLARE @wsNumber nvarchar(50)
	
	SELECT @wsNumber = WSNumber FROM Worksheet WHERE QuoteNumber = @quoteNumber
	
	DELETE FROM SaleOrder where QuoteNumber = @quoteNumber  
	DELETE FROM Worksheet where QuoteNumber = @quoteNumber  
	DELETE FROM WorksheetLineItem WHERE WSNumber = @wsNumber
	DELETE FROM Invoice where QuoteNumber = @quoteNumber  
   
 --UPDATE Invoice  
 --SET IsActive = 'false'  
 --WHERE QuoteNumber = @quoteNumber  
    
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteQuoteItems]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[DeleteQuoteItems]
@QuoteNumber nvarchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;
	
	DELETE QuoteLineItems
	WHERE [QuoteNumber] =@QuoteNumber
	
	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteQuote]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteQuote]    
 @QuoteNumber nvarchar(50)  
AS    
BEGIN    
     
 SET NOCOUNT ON;    
    
 DELETE FROM QuoteHeader   
 WHERE QuoteNumber = @QuoteNumber  
  
 DELETE FROM QuoteLineItems   
 WHERE QuoteNumber = @QuoteNumber  
  
 DELETE FROM QuoteFooter  
 WHERE QuoteNumber = @QuoteNumber  
   
 DELETE FROM SaleOrder where QuoteNumber = @quoteNumber  
 DELETE FROM Worksheet where QuoteNumber = @quoteNumber  
 DELETE FROM Invoice where QuoteNumber = @quoteNumber  
 DELETE FROM LineCutoutDetails WHERE QuoteNumber = @QuoteNumber
 DELETE FROM LineItemDetails WHERE QuoteNumber = @QuoteNumber
 DELETE FROM LineItemInsulationDetails WHERE QuoteNumber = @QuoteNumber
   
 SET NOCOUNT OFF;    
    
END
GO
/****** Object:  StoredProcedure [dbo].[DeletePrintReuqest]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeletePrintReuqest]
@id int
AS
BEGIN
	DELETE FROM ReprintQueue
	WHERE ID= @id
END
GO
/****** Object:  StoredProcedure [dbo].[InsertShippingDetails]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertShippingDetails] 
@QuoteNumber nvarchar(50)
,@CustomerID int
,@Address nvarchar(200)
,@FirstName nvarchar(50)
,@LastName nvarchar(50)
,@Phone nvarchar(50)
,@Fax nvarchar(50)
,@Email nvarchar(50)
,@Misc nvarchar(1000)
AS
BEGIN
	
	SET NOCOUNT ON;

	INSERT INTO [dbo].[OtherShippingAddress]
           ([QuoteNumber]
           ,[CustomerID]
           ,[Address]
           ,[FirstName]
           ,[LastName]
           ,[Phone]
           ,[Fax]
           ,[Email]
           ,[Misc])
     VALUES
           (@QuoteNumber
           ,@CustomerID
           ,@Address
           ,@FirstName
           ,@LastName
           ,@Phone
           ,@Fax
           ,@Email
           ,@Misc)
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[InsertQuoteLineItem]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertQuoteLineItem]       

@QuoteNumber nvarchar(50)      
,@LineID int      
,@Quantity int      
,@Description nvarchar(1000)      
,@Dimension nvarchar(50)      
,@SqFt int      
,@PricePerUnit float      
,@Total float      
,@ActualTotalSQFT float    
,@IsLogo bit    
,@Shape nvarchar(50)   
,@IsPolish bit  
,@IsDrills bit  
,@IsWaterjet bit  
,@IsTemper bit  
,@IsInsulate bit  
AS      
BEGIN      
       
 SET NOCOUNT ON;      
      
 INSERT INTO [GlassManagerDB].[dbo].[QuoteLineItems]      
           ([QuoteNumber]      
           ,[LineID]      
           ,[Quantity]      
           ,[Description]      
           ,[Dimension]      
           ,[SqFt]      
           ,[PricePerUnit]      
           ,[Total]    
           ,ActialSqFt    
           ,IsLogo    
           ,Shape  
           ,IsPolish  
           ,IsDrills  
           ,IsWaterjet   
           ,IsTemper  
           ,IsInsulate  
           )      
     VALUES      
           (@QuoteNumber      
           ,@LineID      
           ,@Quantity      
           ,@Description      
           ,@Dimension      
           ,@SqFt      
           ,@PricePerUnit      
           ,@Total    
           ,@ActualTotalSQFT    
           ,@IsLogo    
           ,@Shape   
           ,@IsPolish  
           ,@IsDrills  
           ,@IsWaterjet   
           ,@IsTemper  
           ,@IsInsulate   
           )      
      
       
 SET NOCOUNT OFF;      
      
END
GO
/****** Object:  StoredProcedure [dbo].[InsertQuoteFooter]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertQuoteFooter] 
@QuoteNumber nvarchar(50)
,@SubTotal float
,@IsDollar bit
,@EnergySurcharge float
,@Discount float
,@Delivery float
,@IsRush bit
,@RushOrder float
,@Tax float
,@GrandTotal float
AS
BEGIN
	
	SET NOCOUNT ON;

	INSERT INTO [GlassManagerDB].[dbo].QuoteFooter
           (QuoteNumber
           ,[SubTotal]
           ,[IsDollar]
           ,[EnergySurcharge]
           ,[Discount]
           ,[Delivery]
           ,[IsRush]
           ,[RushOrder]
           ,[Tax]
           ,[GrandTotal])
     VALUES
           (@QuoteNumber
           ,@SubTotal
           ,@IsDollar
           ,@EnergySurcharge
           ,@Discount
           ,@Delivery
           ,@IsRush
           ,@RushOrder
           ,@Tax
           ,@GrandTotal)
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[InsertLineItemInsulationDetails]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertLineItemInsulationDetails]  
@QuoteNumber nvarchar(50)  
,@LineID int  
,@GlassType1Index int  
,@Thickness1Index int  
,@IsTemp1 bit  
,@Sqft int  
,@Total1 float  
,@Total2 float  
,@GlassType2Index int  
,@Thickness2Index int  
,@IsTemp2 bit  
,@MaterialCost float  
,@InsulationTier float  
,@InsulationTierTotal float  
,@InsulationTotal float  
AS  
BEGIN  
INSERT INTO [GlassManagerDB].[dbo].[LineItemInsulationDetails]  
           ([QuoteNumber]  
           ,[LineID]  
           ,[GlassType1Index]  
           ,[Thickness1Index]  
           ,[IsTemp1]  
           ,[Sqft]  
           ,[Total1]  
           ,Total2
           ,[GlassType2Index]  
           ,[Thickness2Index]  
           ,[IsTemp2]  
           ,[MaterialCost]  
           ,[InsulationTier]  
           ,[InsulationTierTotal]  
           ,[InsulationTotal])  
     VALUES  
           (@QuoteNumber  
           ,@LineID  
           ,@GlassType1Index  
           ,@Thickness1Index  
           ,@IsTemp1  
           ,@Sqft  
           ,@Total1
           ,@Total2  
           ,@GlassType2Index  
           ,@Thickness2Index  
           ,@IsTemp2  
           ,@MaterialCost  
           ,@InsulationTier  
           ,@InsulationTierTotal  
           ,@InsulationTotal)  
END
GO
/****** Object:  StoredProcedure [dbo].[InsertLineItemDetails]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertLineItemDetails]    
@QuoteNumber nvarchar(50)    
,@LineID int    
,@SelectedGlassIndex int    
,@SelectedThicknessIndex int    
,@IsLogo bit    
,@IsTempered bit    
,@SelectedShapeIndex int    
,@ActualWidth int    
,@ActualWidthFraction nvarchar(50)    
,@ActualHeight int    
,@ActualHeightFraction nvarchar(50)    
,@Quantity int    
,@ActualTotalSqft int    
,@ChargedWidth int    
,@ChargedHeight int    
,@ChargedTotal float    
,@StraightTotalPolish int    
,@StraightLongSide int    
,@StraightShortSide int    
,@CustomTotalPolish int    
,@MiterTotalPolish int    
,@MiterLongSide int    
,@MiterShortSide int    
,@Notches int    
,@Patches int    
,@Holes int    
,@Hinges int    
,@CutoutTotal float  
AS    
BEGIN    
 INSERT INTO [LineItemDetails]    
      ([QuoteNumber]    
      ,[LineID]    
      ,[SelectedGlassIndex]    
      ,[SelectedThicknessIndex]    
      ,[IsLogo]    
      ,[IsTempered]    
      ,[SelectedShapeIndex]    
      ,[ActualWidth]    
      ,[ActualWidthFraction]    
      ,[ActualHeight]    
      ,[ActualHeightFraction]    
      ,[Quantity]    
      ,[ActualTotalSqft]    
      ,[ChargedWidth]    
      ,[ChargedHeight]    
      ,[ChargedTotal]    
      ,[StraightTotalPolish]    
      ,[StraightLongSide]    
      ,[StraightShortSide]    
      ,[CustomTotalPolish]    
      ,[MiterTotalPolish]    
      ,[MiterLongSide]    
      ,[MiterShortSide]    
      ,[Notches]    
      ,[Patches]    
      ,[Holes]    
      ,[Hinges]  
      ,CutoutTotal)    
   VALUES    
      (    
    @QuoteNumber    
    ,@LineID    
    ,@SelectedGlassIndex    
    ,@SelectedThicknessIndex    
    ,@IsLogo    
    ,@IsTempered    
    ,@SelectedShapeIndex    
    ,@ActualWidth    
    ,@ActualWidthFraction    
    ,@ActualHeight    
    ,@ActualHeightFraction    
    ,@Quantity    
    ,@ActualTotalSqft    
    ,@ChargedWidth    
    ,@ChargedHeight    
    ,@ChargedTotal    
    ,@StraightTotalPolish    
    ,@StraightLongSide    
    ,@StraightShortSide    
    ,@CustomTotalPolish    
    ,@MiterTotalPolish    
    ,@MiterLongSide    
    ,@MiterShortSide    
    ,@Notches    
    ,@Patches    
    ,@Holes    
    ,@Hinges  
    ,@CutoutTotal)    
END
GO
/****** Object:  StoredProcedure [dbo].[InsertLineCutoutDetails]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertLineCutoutDetails]
@QuoteNumber nvarchar(50) 
,@LineID int 
,@Quantity int 
,@Width int 
,@Height int 
,@Price float 
AS
BEGIN
	INSERT INTO [GlassManagerDB].[dbo].[LineCutoutDetails]
           ([QuoteNumber]
           ,[LineID]
           ,[Quantity]
           ,[Width]
           ,[Height]
           ,[Price])
     VALUES
           (@QuoteNumber
           ,@LineID
           ,@Quantity
           ,@Width
           ,@Height
           ,@Price)
END
GO
/****** Object:  StoredProcedure [dbo].[CreateNewThickness]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[CreateNewThickness]  
 @glassID nvarchar(50),
 @thickness nvarchar(50)
AS  
BEGIN  
   
 SET NOCOUNT ON;  
	
	DECLARE @thicknessID int
	
	SELECT @thicknessID = MAX(ID) FROM LuThickness
	
	IF @thicknessID = NULL
		SET @thicknessID = 1
	ELSE
		SET @thicknessID = @thicknessID + 1 
	
	INSERT INTO [dbo].[LuThickness]
           ([ID]
           ,[Thickness])
     VALUES
           (@thicknessID
           ,@thickness)
           
     INSERT INTO GlassRates(ID, ThicknessID)
     VALUES(@glassID,@thicknessID)
	
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[CreateNewCustomer]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateNewCustomer] 
 @Address nvarchar(200)
,@FirstName nvarchar(50)
,@LastName nvarchar(50)
,@Phone nvarchar(50)
,@Fax nvarchar(50)
,@Email nvarchar(50)
,@Misc nvarchar(1000)
AS
BEGIN
	
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Customer]
           ([Address]
           ,[FirstName]
           ,[LastName]
           ,[Phone]
           ,[Fax]
           ,[Email]
           ,[Misc]
           ,IsActive)
     VALUES
           (@Address
           ,@FirstName
           ,@LastName
           ,@Phone
           ,@Fax
           ,@Email
           ,@Misc
           ,'true')
	
	SELECT MAX(ID) FROM Customer
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[BackupDatabase]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[BackupDatabase]
@databaseName nvarchar(50)
,@backupPath nvarchar(200)
,@backupDate nvarchar(50)
AS
BEGIN
	declare @sql as nvarchar(1000)
	set @sql='BACKUP DATABASE ' + @databaseName + ' TO DISK = ' +@backupPath
	exec sp_executesql @sql
	
	INSERT INTO DBBackupStatus Values(@backupDate)
	
END
GO
/****** Object:  StoredProcedure [dbo].[AddQuoteHeader]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddQuoteHeader] 
 @CreatedOn  nvarchar(50)
,@RequestedShipDate nvarchar(50)
,@CustomerPO nvarchar(50)
,@LeadTimeID int
,@LeadTimeTypeID int
,@CustomerID int
,@ShippingMethodID int
,@ShipToOtherAddress bit
,@QuoteNumber nvarchar(50)
,@OperatorName nvarchar(50)
,@PaymentModeID int
,@QuoteStatusID int
AS
BEGIN
	
	SET NOCOUNT ON;
	
	DECLARE @QuoteID int
	
	SELECT @QuoteID = MAX(QuoteID)  FROM QuoteHeader
	SET @QuoteID = ISNULL(@QuoteID,0) +1

	INSERT INTO [GlassManagerDB].[dbo].[QuoteHeader]
           ([CreatedOn]
           ,[RequestedShipDate]
           ,[CustomerPO]
           ,[LeadTimeID]
           ,[LeadTimeTypeID]
           ,[CustomerID]
           ,[ShipToOtherAddress]
           ,[QuoteNumber]
           ,ShippingMethodID
           ,OperatorName
           ,PaymentModeID
           ,QuoteStatusID
           ,QuoteID)
     VALUES
           (@CreatedOn
           ,@RequestedShipDate
           ,@CustomerPO
           ,@LeadTimeID
           ,@LeadTimeTypeID
           ,@CustomerID
           ,@ShipToOtherAddress
           ,@QuoteNumber
           ,@ShippingMethodID
           ,@OperatorName
           ,@PaymentModeID
           ,@QuoteStatusID
           ,@QuoteID)
		
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[AddNewGlassType]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddNewGlassType]  
 @glassType nvarchar(50)
AS  
BEGIN  
   
 SET NOCOUNT ON;  
 
	INSERT INTO [dbo].[LuGlass]
           ([GlassType])
     VALUES
           (@glassType)
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[AddJobToPrintQueue]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[AddJobToPrintQueue]  
@WSNumber nvarchar(50),
@userName nvarchar(50),  
@lineID int,  
@itemID int  
AS  
BEGIN  
 INSERT INTO [dbo].[ReprintQueue]  
           ([WSNumber]  
           ,[LineID]  
           ,[ItemID]
           ,UserName)  
     VALUES  
           (@WSNumber  
           ,@lineID  
           ,@itemID
           ,@userName)  
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteInvoice]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteInvoice]  
 @QuoteNumber nvarchar(50)
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
	DELETE FROM Invoice
	WHERE QuoteNumber = @quoteNumber
	
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteGlassType]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteGlassType]  
 @glassID nvarchar(50)
AS  
BEGIN  
   
 SET NOCOUNT ON;  
 
	DELETE FROM LuGlass
	WHERE ID = @glassID

	DELETE FROM GlassRates
	WHERE ID = @glassID
	
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteCustomer]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCustomer]  
 @CustomerID nvarchar(50)
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
	UPDATE Customer
	SET IsActive = 'false'
	WHERE ID= @CustomerID
	--Declare @QuoteNumber nvarchar(50)
	
	--DELETE FROM Customer 
	--WHERE ID= @CustomerID
	
	--SELECT @QuoteNumber =QuoteNumber
	--FROM QuoteHeader
	--WHERE CustomerID = @CustomerID
	
	--EXEC DeleteQuote @QuoteNumber
	
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateInsulationCost]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[UpdateInsulationCost]  
 @Ceiling1 int
,@Cost1 float
,@Ceiling2 int 
,@Cost2 float
,@Cost3 float
AS  
BEGIN  
   
	UPDATE [LuInsulationCost]
	SET  [Ceiling1] = @Ceiling1
		  ,[Cost1] = @Cost1
		  ,[Ceiling2] = @Ceiling2
		  ,[Cost2] = @Cost2
		  ,[Cost3] = @Cost3
	
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateHoleRate]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[UpdateHoleRate]
@thicknessID int,
@holeRate float
AS  
BEGIN  
   
 SET NOCOUNT ON;  
 
 UPDATE [GlassManagerDB].[dbo].[LuHole]
   SET [HoleRate] = @holeRate
 WHERE ThicknessID =  @thicknessID
  
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateGlassType]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[UpdateGlassType]  
 @updatedGlassType nvarchar(50),
 @glasstypeID int
AS  
BEGIN  
   
 SET NOCOUNT ON;  
 
 UPDATE [dbo].[LuGlass]
   SET [GlassType] = @updatedGlassType
 WHERE ID = @glasstypeID

 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateGlassRates]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[UpdateGlassRates]  
 @GlassID int,  
 @ThicknessID int,  
 @cutSqft float,
 @temperedSqft float,
 @polishStraight float,
 @polishShape float,
 @miterRate float
AS  
BEGIN  
   
 SET NOCOUNT ON;  
 
 UPDATE GlassRates
 SET CutSQFT =@cutSqft, TemperedSQFT=@temperedSqft, PolishStraight=@polishStraight, PolishShape=@polishShape
 WHERE ID = @GlassID AND ThicknessID = @ThicknessID 
  
 UPDATE LuMiter
 SET Rate = @miterRate
 WHERE ThicknessID = @ThicknessID
   
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateGlassItemStatus]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateGlassItemStatus]
@glassItemID int
,@status nvarchar(50)
AS 
BEGIN    
     
 SET NOCOUNT ON;    
 
	UPDATE WorksheetLineItem
	SET Status = @status
	WHERE ID = @glassItemID    
    
 SET NOCOUNT OFF;    
    
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCustomer]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCustomer]   
 @Address nvarchar(200)  
,@FirstName nvarchar(50)  
,@LastName nvarchar(50)  
,@Phone nvarchar(50)  
,@Fax nvarchar(50)  
,@Email nvarchar(50)  
,@Misc nvarchar(1000)  
,@customerID nvarchar(50)  
AS  
BEGIN  
   
 SET NOCOUNT ON;  

UPDATE [Customer]
   SET [Address] = @Address
      ,[FirstName] = @FirstName
      ,[LastName] = @LastName
      ,[Phone] = @Phone
      ,[Fax] = @Fax
      ,[Email] = @Email
      ,[Misc] = @Misc
      
 WHERE ID = @customerID

 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[SetDefaultLeadTime]    Script Date: 01/26/2014 18:30:47 ******/
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
/****** Object:  StoredProcedure [dbo].[RemoveLabelFromPrintQueue]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[RemoveLabelFromPrintQueue]
@id int
AS
BEGIN
	DELETE FROM ReprintQueue WHERE ID = @id
END
GO
/****** Object:  StoredProcedure [dbo].[ResetDefaultLeadTime]    Script Date: 01/26/2014 18:30:47 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateThickness]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[UpdateThickness]  
 @glassID nvarchar(50),
 @thickness nvarchar(50)
AS  
BEGIN  
   
 SET NOCOUNT ON;  
	
	DECLARE @thicknessID int
	
	UPDATE LuThickness
	SET Thickness = @thickness
	WHERE ID=@glassID
	
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTaxRate]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UpdateTaxRate]
@taxRate float
AS
BEGIN
	IF EXISTS(SELECT 1 FROM LuTaxRates)
	BEGIN
		UPDATE LuTaxRates
		SET TaxRate = @taxRate
	END
	ELSE
	BEGIN
		INSERT INTO LuTaxRates(TaxRate) VALUES(@taxRate)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateQuoteHeader]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[UpdateQuoteHeader] 
 @CreatedOn  nvarchar(50)
,@RequestedShipDate nvarchar(50)
,@CustomerPO nvarchar(50)
,@LeadTimeID int
,@LeadTimeTypeID int
,@CustomerID int
,@ShippingMethodID int
,@ShipToOtherAddress bit
,@QuoteNumber nvarchar(50)
,@OperatorName nvarchar(50)
,@PaymentModeID int
,@QuoteStatusID int
AS
BEGIN
	
	SET NOCOUNT ON;
	
	UPDATE [GlassManagerDB].[dbo].[QuoteHeader]
	   SET [CreatedOn] = @CreatedOn
		  ,[RequestedShipDate] = @RequestedShipDate
		  ,[CustomerPO] =@CustomerPO
		  ,[LeadTimeID] =@LeadTimeID
		  ,[LeadTimeTypeID] = @LeadTimeTypeID
		  ,[CustomerID] = @CustomerID
		  ,[ShipToOtherAddress] = @ShipToOtherAddress
		  ,[ShippingMethodID] = @ShippingMethodID
		  ,[OperatorName] = @OperatorName
		  ,[PaymentModeID] = @PaymentModeID
		  ,[QuoteStatusID] = @QuoteStatusID
	 WHERE [QuoteNumber] = @QuoteNumber
		
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateQuoteFooter]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[UpdateQuoteFooter] 
@QuoteNumber nvarchar(50)
,@SubTotal float
,@IsDollar bit
,@EnergySurcharge float
,@Discount float
,@Delivery float
,@IsRush bit
,@RushOrder float
,@Tax float
,@GrandTotal float
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE [GlassManagerDB].[dbo].[QuoteFooter]
	   SET 
		  [SubTotal] = @SubTotal
		  ,[IsDollar] = @IsDollar
		  ,[EnergySurcharge] = @EnergySurcharge
		  ,[Discount] = @Discount
		  ,[Delivery] = @Delivery
		  ,[IsRush] =@IsRush
		  ,[RushOrder] =@RushOrder
		  ,[Tax] = @Tax
		  ,[GrandTotal] = @GrandTotal
	 WHERE [QuoteNumber] =@QuoteNumber
	
	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateMiscRate]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateMiscRate]  
@NotchRate float,  
@HingeRate float,  
@PatchRate float,  
@MinimumTotalSqft float  
AS    
BEGIN    
     
 SET NOCOUNT ON;    
    
   UPDATE [MiscRates]  
   SET [NotchRate] = @NotchRate  
      ,[HingeRate] = @HingeRate  
      ,[PatchRate] = @PatchRate  
      ,[MinimumTotalSqft] = @MinimumTotalSqft
    
 SET NOCOUNT OFF;    
    
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateInvoiceStatusID]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[UpdateInvoiceStatusID]  
@QuoteNumber nvarchar(50),
@InvoiceStatusID int
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
  UPDATE Invoice
  SET InvoiceStatusID = @InvoiceStatusID
  WHERE QuoteNumber = @QuoteNumber
    
 SET NOCOUNT OFF;  
  
END  
  
--[GetQuoteDetails] 'Q00002'
GO
/****** Object:  StoredProcedure [dbo].[UpdateInvoicePaymentTotal]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateInvoicePaymentTotal]
@QuoteNumber nvarchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @balanceDue float
	
	DECLARE @PaymentMade float
	
	SELECT @PaymentMade = SUM(Amount) FROM InvoicePaymentDetails
	WHERE QuoteNumber = @QuoteNumber
	
	SELECT @balanceDue = ISNULL(GrandTotal - @PaymentMade,GrandTotal)
	FROM QuoteFooter
	WHERE QuoteNumber = @QuoteNumber
	
	UPDATE Invoice
	SET BalanceDue = @balanceDue
	WHERE QuoteNumber = @QuoteNumber
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateInvoicePayment]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateInvoicePayment]
@ID int,
@QuoteNumber nvarchar(50),
@PaymentDate datetime,
@Amount float,
@Description nvarchar(500)
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE [GlassManagerDB].[dbo].[InvoicePaymentDetails]
	   SET [PaymentDate] = @PaymentDate
		  ,[Amount] = @Amount
		  ,[Description] = @Description
		  
	 WHERE ID= @ID

	exec UpdateInvoicePaymentTotal @QuoteNumber
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[MakePayment]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MakePayment]
@QuoteNumber nvarchar(50),
@PaymentDate datetime,
@Amount float,
@Description nvarchar(500)
AS
BEGIN
	
	SET NOCOUNT ON;

	INSERT INTO [GlassManagerDB].[dbo].[InvoicePaymentDetails]
           ([QuoteNumber]
           ,[PaymentDate]
           ,[Amount]
           ,[Description])
     VALUES
           (
			@QuoteNumber
			,@PaymentDate
			,@Amount
			,@Description
			
           )
	
	exec UpdateInvoicePaymentTotal @QuoteNumber
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteInvoicePayment]    Script Date: 01/26/2014 18:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteInvoicePayment]
@ID int,
@QuoteNumber nvarchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;

	DELETE FROM InvoicePaymentDetails
	WHERE ID = @ID
	
	exec UpdateInvoicePaymentTotal @QuoteNumber
	
	SET NOCOUNT OFF;

END
GO

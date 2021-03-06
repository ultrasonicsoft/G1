USE [master]
GO
/****** Object:  Database [GlassManagerDB]    Script Date: 12/21/2013 19:07:10 ******/
CREATE DATABASE [GlassManagerDB] 
GO
USE [GlassManagerDB]
GO
/****** Object:  Table [dbo].[WorkshopItem]    Script Date: 12/21/2013 19:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkshopItem](
	[QuoteNumber] [nvarchar](50) NULL,
	[LineID] [int] NULL,
	[ItemID] [int] NULL,
	[Status] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Worksheet]    Script Date: 12/21/2013 19:07:11 ******/
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
INSERT [dbo].[Worksheet] ([ID], [WSNumber], [QuoteNumber], [ConfirmedDate], [Progress], [Status]) VALUES (1, N'W00001', N'Q00001', CAST(0x0000A29B012DDAAC AS DateTime), NULL, NULL)
/****** Object:  Table [dbo].[Users]    Script Date: 12/21/2013 19:07:11 ******/
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
INSERT [dbo].[Users] ([ID], [UserName], [Password], [IsAdmin]) VALUES (2, N'balram', N'balram', 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Table [dbo].[SaleOrder]    Script Date: 12/21/2013 19:07:11 ******/
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
INSERT [dbo].[SaleOrder] ([ID], [SONumber], [QuoteNumber], [ConfirmedDate]) VALUES (1, N'S00001', N'Q00001', CAST(0x0000A29B012DDAA2 AS DateTime))
/****** Object:  Table [dbo].[QuoteLineItems]    Script Date: 12/21/2013 19:07:11 ******/
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
	[ActualDescription] [nvarchar](1000) NULL,
	[ActualDimension] [nvarchar](50) NULL,
	[ActialSqFt] [int] NULL,
	[IsLogo] [bit] NULL,
 CONSTRAINT [PK_QuoteLineItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[QuoteLineItems] ON
INSERT [dbo].[QuoteLineItems] ([QuoteNumber], [LineID], [Quantity], [Description], [Dimension], [SqFt], [PricePerUnit], [Total], [ID], [ActualDescription], [ActualDimension], [ActialSqFt], [IsLogo]) VALUES (N'Q00001', 1, 2, N'Clear Glass [Square] [3/16] 
', N'20 2/4" x  30 2/3"', 5, 8.25, 16.5, 85, N'Clear Glass [Square] [3/16] ', N'20 2/4" x  30 2/3"', 5, 1)
SET IDENTITY_INSERT [dbo].[QuoteLineItems] OFF
/****** Object:  Table [dbo].[QuoteHeader]    Script Date: 12/21/2013 19:07:11 ******/
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
INSERT [dbo].[QuoteHeader] ([CreatedOn], [RequestedShipDate], [CustomerPO], [LeadTimeID], [LeadTimeTypeID], [CustomerID], [ShipToOtherAddress], [QuoteNumber], [ShippingMethodID], [OperatorName], [PaymentModeID], [QuoteStatusID], [QuoteID]) VALUES (N'12/21/2013', N'12/21/2013', N'C11', -1, -1, 59, 0, N'Q00001', 0, N'admin', 1, 2, 1)
/****** Object:  Table [dbo].[QuoteFooter]    Script Date: 12/21/2013 19:07:11 ******/
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
INSERT [dbo].[QuoteFooter] ([QuoteNumber], [SubTotal], [IsDollar], [EnergySurcharge], [Discount], [Delivery], [IsRush], [RushOrder], [Tax], [GrandTotal]) VALUES (N'Q00001', 16.5, 0, 0, 0, 0, 0, 0, 0, 16.5)
/****** Object:  Table [dbo].[OtherShippingAddress]    Script Date: 12/21/2013 19:07:11 ******/
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
/****** Object:  Table [dbo].[MiscRates]    Script Date: 12/21/2013 19:07:11 ******/
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
INSERT [dbo].[MiscRates] ([NotchRate], [HingeRate], [PatchRate], [MinimumTotalSqft]) VALUES (15, 25, 35, 4)
/****** Object:  Table [dbo].[LuThickness]    Script Date: 12/21/2013 19:07:11 ******/
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
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (7, N'11/11')
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (8, N'12/22')
/****** Object:  Table [dbo].[LuShippingMethods]    Script Date: 12/21/2013 19:07:11 ******/
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
/****** Object:  Table [dbo].[LuShapes]    Script Date: 12/21/2013 19:07:11 ******/
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
/****** Object:  Table [dbo].[LuQuoteStatus]    Script Date: 12/21/2013 19:07:11 ******/
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
SET IDENTITY_INSERT [dbo].[LuQuoteStatus] ON
INSERT [dbo].[LuQuoteStatus] ([ID], [Type]) VALUES (1, N'Pending')
INSERT [dbo].[LuQuoteStatus] ([ID], [Type]) VALUES (2, N'Confirmed')
INSERT [dbo].[LuQuoteStatus] ([ID], [Type]) VALUES (3, N'Rejected')
SET IDENTITY_INSERT [dbo].[LuQuoteStatus] OFF
/****** Object:  Table [dbo].[LuPaymentTypes]    Script Date: 12/21/2013 19:07:11 ******/
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
SET IDENTITY_INSERT [dbo].[LuPaymentTypes] ON
INSERT [dbo].[LuPaymentTypes] ([ID], [Type]) VALUES (1, N'COD')
INSERT [dbo].[LuPaymentTypes] ([ID], [Type]) VALUES (2, N'Net 15')
INSERT [dbo].[LuPaymentTypes] ([ID], [Type]) VALUES (3, N'Net 30')
SET IDENTITY_INSERT [dbo].[LuPaymentTypes] OFF
/****** Object:  Table [dbo].[LuMiter]    Script Date: 12/21/2013 19:07:11 ******/
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
INSERT [dbo].[LuMiter] ([ThicknessID], [Rate]) VALUES (3, 10.45)
INSERT [dbo].[LuMiter] ([ThicknessID], [Rate]) VALUES (4, 0.55)
/****** Object:  Table [dbo].[LuLeadTimeType]    Script Date: 12/21/2013 19:07:11 ******/
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
/****** Object:  Table [dbo].[LuLeadTime]    Script Date: 12/21/2013 19:07:11 ******/
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
/****** Object:  Table [dbo].[LuLeadSetting]    Script Date: 12/21/2013 19:07:11 ******/
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
INSERT [dbo].[LuLeadSetting] ([ID], [LeadTimeID], [LeadTypeID], [IsDefaultLeadTime]) VALUES (1, NULL, NULL, 0)
/****** Object:  Table [dbo].[LuInvoiceTypes]    Script Date: 12/21/2013 19:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuInvoiceTypes](
	[ID] [int] NULL,
	[Type] [nvarchar](50) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[LuInvoiceTypes] ([ID], [Type]) VALUES (1, N'Paid in full')
INSERT [dbo].[LuInvoiceTypes] ([ID], [Type]) VALUES (2, N'Payment in progress')
INSERT [dbo].[LuInvoiceTypes] ([ID], [Type]) VALUES (3, N'Outstanding')
/****** Object:  Table [dbo].[LuInsulationCost]    Script Date: 12/21/2013 19:07:11 ******/
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
/****** Object:  Table [dbo].[LuHole]    Script Date: 12/21/2013 19:07:11 ******/
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
/****** Object:  Table [dbo].[LuGlass]    Script Date: 12/21/2013 19:07:11 ******/
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
INSERT [dbo].[LuGlass] ([ID], [GlassType]) VALUES (9, N'test')
SET IDENTITY_INSERT [dbo].[LuGlass] OFF
/****** Object:  Table [dbo].[GlassRates]    Script Date: 12/21/2013 19:07:11 ******/
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
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (2, 3, 18, 110, 10.13, 10.5)
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
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (3, 7, NULL, NULL, NULL, NULL)
INSERT [dbo].[GlassRates] ([ID], [ThicknessID], [CutSQFT], [TemperedSQFT], [PolishStraight], [PolishShape]) VALUES (9, 8, NULL, NULL, NULL, NULL)
/****** Object:  Table [dbo].[CustomerShippingAddress]    Script Date: 12/21/2013 19:07:11 ******/
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
/****** Object:  Table [dbo].[Customer]    Script Date: 12/21/2013 19:07:11 ******/
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
SET IDENTITY_INSERT [dbo].[Customer] ON
INSERT [dbo].[Customer] ([ID], [Address], [FirstName], [LastName], [Phone], [Fax], [Email], [Misc], [IsActive]) VALUES (59, N'NY', N'Tim', N'Owner', N'222-222-2222', N'', N'tim@gmail.com', N'note1', 1)
SET IDENTITY_INSERT [dbo].[Customer] OFF
/****** Object:  Table [dbo].[InvoicePaymentDetails]    Script Date: 12/21/2013 19:07:11 ******/
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
/****** Object:  Table [dbo].[Invoice]    Script Date: 12/21/2013 19:07:11 ******/
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
/****** Object:  StoredProcedure [dbo].[InsertShippingDetails]    Script Date: 12/21/2013 19:07:12 ******/
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
/****** Object:  StoredProcedure [dbo].[InsertQuoteLineItem]    Script Date: 12/21/2013 19:07:12 ******/
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
           ,[Total])
     VALUES
           (@QuoteNumber
           ,@LineID
           ,@Quantity
           ,@Description
           ,@Dimension
           ,@SqFt
           ,@PricePerUnit
           ,@Total)

	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[InsertQuoteFooter]    Script Date: 12/21/2013 19:07:12 ******/
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
/****** Object:  StoredProcedure [dbo].[CreateNewThickness]    Script Date: 12/21/2013 19:07:12 ******/
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
/****** Object:  StoredProcedure [dbo].[CreateNewCustomer]    Script Date: 12/21/2013 19:07:12 ******/
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
/****** Object:  StoredProcedure [dbo].[AddQuoteHeader]    Script Date: 12/21/2013 19:07:12 ******/
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
/****** Object:  StoredProcedure [dbo].[AddNewGlassType]    Script Date: 12/21/2013 19:07:12 ******/
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
/****** Object:  StoredProcedure [dbo].[GetWorksheetMasterData]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetThicknesses]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetThicknessByGlassID]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetSmartSearchData]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetSaleOrderNumber]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetSaleOrderMasterData]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetRates]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetQuoteMasterData]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetQuoteDetails]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetNewSaleOrderID]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetNewQuoteID]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetMiscRates]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetInvoicePaymentDetails]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetInvoiceMasterData]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetInsulationCost]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetHoleRateByThicknessID]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetDefaultLeadTimeSet]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetCustomerID]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetCustomerDetails]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetBarcodeDetails]    Script Date: 12/21/2013 19:07:13 ******/
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
  qh.CreatedOn, qh.CustomerPO,
  qi.LineID 'LineID', qi.ActualDescription 'Description', qi.ActialSqFt 'SqFt', qi.ActualDimension 'Dimension', qi.IsLogo 'IsLogo', qi.Quantity
  FROM QuoteHeader qh
  LEFT JOIN Customer c ON c.ID = qh.CustomerID
  LEFT JOIN SaleOrder so ON so.QuoteNumber = qh.QuoteNumber
  LEFT JOIN Worksheet w ON w.QuoteNumber = qh.QuoteNumber
  LEFT JOIN QuoteLineItems qi ON qi.QuoteNumber	= qh.QuoteNumber
  
  WHERE qh.QuoteNumber = @QuoteNumber
  
  
 SET NOCOUNT OFF;  
  
END  
  
  
--GetBarcodeDetails   'Q00001'
GO
/****** Object:  StoredProcedure [dbo].[GetAllWorksheetNumbers]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllShippingMethods]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllShapes]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllSalesOrderNumbers]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllQuoteStatus]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllQuoteNumbers]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllPaymentTypes]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllOperatorNames]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllLeadTimeTypes]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllLeadTime]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllInvoiceTypes]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllInvoiceNumbers]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllInsulationCost]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllGlassTypes]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllCustomers]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllCustomerNames]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllCustomerDetails]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GenerateWorksheet]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GenerateSaleOrder]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[GenerateInvoice]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteWorksheet]    Script Date: 12/21/2013 19:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteWorksheet]
@quoteNumber nvarchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;

	DELETE FROM Worksheet where QuoteNumber = @quoteNumber
	DELETE FROM SaleOrder where QuoteNumber = @quoteNumber
	DELETE FROM Invoice where QuoteNumber = @quoteNumber
	
	--UPDATE Invoice
	--SET IsActive = 'false'
	--WHERE QuoteNumber = @quoteNumber
	
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteSalesOrder]    Script Date: 12/21/2013 19:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSalesOrder]
@quoteNumber nvarchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;

	DELETE FROM SaleOrder where QuoteNumber = @quoteNumber
	DELETE FROM Worksheet where QuoteNumber = @quoteNumber
	DELETE FROM Invoice where QuoteNumber = @quoteNumber
	
	--UPDATE Invoice
	--SET IsActive = 'false'
	--WHERE QuoteNumber = @quoteNumber
		
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteQuoteItems]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteQuote]    Script Date: 12/21/2013 19:07:13 ******/
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
	
 SET NOCOUNT OFF;  
  
END
GO
/****** Object:  StoredProcedure [dbo].[IsWorksheetPresent]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[IsSalesOrderPresent]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[IsQuoteNumberPresent]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[IsInvoicePresent]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[IsDefaultLeadTimeSet]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteInvoice]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteGlassType]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteCustomer]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateInsulationCost]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateHoleRate]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateGlassType]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateGlassRates]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateCustomer]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[SetDefaultLeadTime]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[ResetDefaultLeadTime]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateThickness]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateQuoteHeader]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateQuoteFooter]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateMiscRate]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateInvoiceStatusID]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateInvoicePaymentTotal]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateInvoicePayment]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[MakePayment]    Script Date: 12/21/2013 19:07:13 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteInvoicePayment]    Script Date: 12/21/2013 19:07:13 ******/
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

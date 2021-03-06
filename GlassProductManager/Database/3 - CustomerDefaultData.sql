USE [GlassManagerDB]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01/26/2014 18:43:16 ******/
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([ID], [UserName], [Password], [IsAdmin]) VALUES (1, N'admin', N'admin', 1)
INSERT [dbo].[Users] ([ID], [UserName], [Password], [IsAdmin]) VALUES (2, N'balram', N'balram', 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Table [dbo].[MiscRates]    Script Date: 01/26/2014 18:43:16 ******/
INSERT [dbo].[MiscRates] ([NotchRate], [HingeRate], [PatchRate], [MinimumTotalSqft]) VALUES (15, 25, 35, 4)
/****** Object:  Table [dbo].[LuThickness]    Script Date: 01/26/2014 18:43:16 ******/
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (1, N'3/16')
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (2, N'1/4')
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (3, N'3/8')
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (4, N'1/2')
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (5, N'5/8')
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (6, N'3/4')
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (7, N'11/11')
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (8, N'12/22')
INSERT [dbo].[LuThickness] ([ID], [Thickness]) VALUES (9, N'23/45')
/****** Object:  Table [dbo].[LuTaxRates]    Script Date: 01/26/2014 18:43:16 ******/
INSERT [dbo].[LuTaxRates] ([TaxRate]) VALUES (10)
/****** Object:  Table [dbo].[LuShippingMethods]    Script Date: 01/26/2014 18:43:16 ******/
INSERT [dbo].[LuShippingMethods] ([ID], [Shipping]) VALUES (1, N'Delivery')
INSERT [dbo].[LuShippingMethods] ([ID], [Shipping]) VALUES (2, N'Pickup')
INSERT [dbo].[LuShippingMethods] ([ID], [Shipping]) VALUES (3, N'Courier')
/****** Object:  Table [dbo].[LuShapes]    Script Date: 01/26/2014 18:43:16 ******/
SET IDENTITY_INSERT [dbo].[LuShapes] ON
INSERT [dbo].[LuShapes] ([ID], [Shape]) VALUES (1, N'Square')
INSERT [dbo].[LuShapes] ([ID], [Shape]) VALUES (2, N'Triangle')
INSERT [dbo].[LuShapes] ([ID], [Shape]) VALUES (3, N'Quadrilateral')
INSERT [dbo].[LuShapes] ([ID], [Shape]) VALUES (4, N'Parallelogram')
INSERT [dbo].[LuShapes] ([ID], [Shape]) VALUES (5, N'Trapezoid')
INSERT [dbo].[LuShapes] ([ID], [Shape]) VALUES (6, N'Circle')
INSERT [dbo].[LuShapes] ([ID], [Shape]) VALUES (7, N'Custom')
SET IDENTITY_INSERT [dbo].[LuShapes] OFF
/****** Object:  Table [dbo].[LuQuoteStatus]    Script Date: 01/26/2014 18:43:16 ******/
SET IDENTITY_INSERT [dbo].[LuQuoteStatus] ON
INSERT [dbo].[LuQuoteStatus] ([ID], [Type]) VALUES (1, N'Pending')
INSERT [dbo].[LuQuoteStatus] ([ID], [Type]) VALUES (2, N'Confirmed')
INSERT [dbo].[LuQuoteStatus] ([ID], [Type]) VALUES (3, N'Rejected')
SET IDENTITY_INSERT [dbo].[LuQuoteStatus] OFF
/****** Object:  Table [dbo].[LuPaymentTypes]    Script Date: 01/26/2014 18:43:16 ******/
SET IDENTITY_INSERT [dbo].[LuPaymentTypes] ON
INSERT [dbo].[LuPaymentTypes] ([ID], [Type]) VALUES (1, N'COD')
INSERT [dbo].[LuPaymentTypes] ([ID], [Type]) VALUES (2, N'Net 15')
INSERT [dbo].[LuPaymentTypes] ([ID], [Type]) VALUES (3, N'Net 30')
SET IDENTITY_INSERT [dbo].[LuPaymentTypes] OFF
/****** Object:  Table [dbo].[LuMiter]    Script Date: 01/26/2014 18:43:16 ******/
INSERT [dbo].[LuMiter] ([ThicknessID], [Rate]) VALUES (1, 0.3)
INSERT [dbo].[LuMiter] ([ThicknessID], [Rate]) VALUES (2, 0.35)
INSERT [dbo].[LuMiter] ([ThicknessID], [Rate]) VALUES (3, 10.45)
INSERT [dbo].[LuMiter] ([ThicknessID], [Rate]) VALUES (4, 0.55)
/****** Object:  Table [dbo].[LuLeadTimeType]    Script Date: 01/26/2014 18:43:16 ******/
INSERT [dbo].[LuLeadTimeType] ([ID], [LeadTimeType]) VALUES (1, N'Days')
INSERT [dbo].[LuLeadTimeType] ([ID], [LeadTimeType]) VALUES (2, N'Weeks')
INSERT [dbo].[LuLeadTimeType] ([ID], [LeadTimeType]) VALUES (3, N'Months')
/****** Object:  Table [dbo].[LuLeadTime]    Script Date: 01/26/2014 18:43:16 ******/
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
/****** Object:  Table [dbo].[LuLeadSetting]    Script Date: 01/26/2014 18:43:16 ******/
INSERT [dbo].[LuLeadSetting] ([ID], [LeadTimeID], [LeadTypeID], [IsDefaultLeadTime]) VALUES (1, NULL, NULL, 0)
/****** Object:  Table [dbo].[LuInvoiceTypes]    Script Date: 01/26/2014 18:43:16 ******/
INSERT [dbo].[LuInvoiceTypes] ([ID], [Type]) VALUES (1, N'Paid in full')
INSERT [dbo].[LuInvoiceTypes] ([ID], [Type]) VALUES (2, N'Payment in progress')
INSERT [dbo].[LuInvoiceTypes] ([ID], [Type]) VALUES (3, N'Outstanding')
/****** Object:  Table [dbo].[LuInsulationCost]    Script Date: 01/26/2014 18:43:16 ******/
INSERT [dbo].[LuInsulationCost] ([ID], [Ceiling1], [Cost1], [Ceiling2], [Cost2], [Cost3]) VALUES (1, 35, 5, 55, 7, 10)
/****** Object:  Table [dbo].[LuHole]    Script Date: 01/26/2014 18:43:16 ******/
INSERT [dbo].[LuHole] ([ThicknessID], [HoleRate]) VALUES (1, 0)
INSERT [dbo].[LuHole] ([ThicknessID], [HoleRate]) VALUES (2, 9.5)
INSERT [dbo].[LuHole] ([ThicknessID], [HoleRate]) VALUES (3, 11.5)
INSERT [dbo].[LuHole] ([ThicknessID], [HoleRate]) VALUES (4, 12.5)
INSERT [dbo].[LuHole] ([ThicknessID], [HoleRate]) VALUES (5, 22.5)
INSERT [dbo].[LuHole] ([ThicknessID], [HoleRate]) VALUES (6, 25)
/****** Object:  Table [dbo].[LuGlass]    Script Date: 01/26/2014 18:43:16 ******/
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
/****** Object:  Table [dbo].[GlassRates]    Script Date: 01/26/2014 18:43:16 ******/
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

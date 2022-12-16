CREATE DATABASE GymDb
GO

USE GymDb
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 12/12/2022 2:14:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[StaffID] [int] NULL,
	[AccountName] [nchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[LastLogin] [datetime] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 12/12/2022 2:14:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[BookingID] [int] IDENTITY(1,1) NOT NULL,
	[PTContractID] [int] NULL,
	[CustomerID] [int] NULL,
	[PTID] [int] NULL,
	[CreateDate] [date] NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 12/12/2022 2:14:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contracts](
	[ContractID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[CourseID] [int] NULL,
	[CreateDate] [date] NULL,
	[FinishDate] [date] NULL,
	[Description] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED 
(
	[ContractID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 12/12/2022 2:14:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CourseID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [int] NULL,
	[Duration] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 12/12/2022 2:14:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IdentityNumber] [varchar](12) NULL,
	[Gender] [nvarchar](10) NULL,
	[Birthday] [date] NULL,
	[Phone] [varchar](10) NULL,
	[Email] [nchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Avatar] [nvarchar](255) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facilities]    Script Date: 12/12/2022 2:14:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facilities](
	[FacilityID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[TypeID] [int] NULL,
	[Quantity] [int] NULL,
	[PricePerUnit] [int] NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Facility] PRIMARY KEY CLUSTERED 
(
	[FacilityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PTContracts]    Script Date: 12/12/2022 2:14:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PTContracts](
	[PTContractID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[PTID] [int] NULL,
	[PTCourseID] [int] NULL,
	[CreateDate] [date] NULL,
	[FinishDate] [date] NULL,
	[Description] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_PTContracts] PRIMARY KEY CLUSTERED 
(
	[PTContractID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PTCourses]    Script Date: 12/12/2022 2:14:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PTCourses](
	[PTCourseID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[NumberOfSession] [int] NULL,
	[Price] [int] NULL,
	[Duration] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_PTCourses] PRIMARY KEY CLUSTERED 
(
	[PTCourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PTs]    Script Date: 12/12/2022 2:14:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PTs](
	[PTID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IdentityNumber] [varchar](12) NULL,
	[Gender] [nvarchar](10) NULL,
	[Birthday] [date] NULL,
	[Phone] [varchar](10) NULL,
	[Email] [nchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Avatar] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_PTs] PRIMARY KEY CLUSTERED 
(
	[PTID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/12/2022 2:14:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StaffID]    Script Date: 12/12/2022 2:14:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[StaffID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IdentityNumber] [varchar](12) NULL,
	[Gender] [nvarchar](10) NULL,
	[Birthday] [date] NULL,
	[Phone] [varchar](10) NULL,
	[Email] [nchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Avatar] [nvarchar](255) NULL,
	[RoleID] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_StaffID] PRIMARY KEY CLUSTERED 
(
	[StaffID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypesOfFacility]    Script Date: 12/12/2022 2:14:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypesOfFacility](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_TypesOfFacility] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([AccountID], [StaffID], [AccountName], [Password], [CreateDate], [LastLogin], [Active]) VALUES (1, 1, N'adminkh                                           ', N'admin', CAST(N'2022-01-01T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Accounts] ([AccountID], [StaffID], [AccountName], [Password], [CreateDate], [LastLogin], [Active]) VALUES (2, 2, N'adminth                                           ', N'admin', CAST(N'2022-05-05T00:00:00.000' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Booking] ON 

INSERT [dbo].[Booking] ([BookingID], [PTContractID], [CustomerID], [PTID], [CreateDate]) VALUES (1, 1, 1, 1, CAST(N'2022-05-10' AS Date))
INSERT [dbo].[Booking] ([BookingID], [PTContractID], [CustomerID], [PTID], [CreateDate]) VALUES (2, 1, 1, 1, CAST(N'2022-05-15' AS Date))
INSERT [dbo].[Booking] ([BookingID], [PTContractID], [CustomerID], [PTID], [CreateDate]) VALUES (3, 1, 1, 1, CAST(N'2022-05-20' AS Date))
INSERT [dbo].[Booking] ([BookingID], [PTContractID], [CustomerID], [PTID], [CreateDate]) VALUES (4, 5, 4, 3, CAST(N'2022-01-03' AS Date))
INSERT [dbo].[Booking] ([BookingID], [PTContractID], [CustomerID], [PTID], [CreateDate]) VALUES (5, 4, 3, 2, CAST(N'2022-10-02' AS Date))
SET IDENTITY_INSERT [dbo].[Booking] OFF
GO
SET IDENTITY_INSERT [dbo].[Contracts] ON 

INSERT [dbo].[Contracts] ([ContractID], [CustomerID], [CourseID], [CreateDate], [FinishDate], [Description], [Active]) VALUES (1, 1, 1, CAST(N'2022-09-05' AS Date), CAST(N'2022-09-06' AS Date), NULL, 0)
INSERT [dbo].[Contracts] ([ContractID], [CustomerID], [CourseID], [CreateDate], [FinishDate], [Description], [Active]) VALUES (2, 2, 2, CAST(N'2022-01-12' AS Date), CAST(N'2023-01-06' AS Date), NULL, 1)
INSERT [dbo].[Contracts] ([ContractID], [CustomerID], [CourseID], [CreateDate], [FinishDate], [Description], [Active]) VALUES (3, 3, 3, CAST(N'2022-01-02' AS Date), CAST(N'2023-01-02' AS Date), NULL, 1)
INSERT [dbo].[Contracts] ([ContractID], [CustomerID], [CourseID], [CreateDate], [FinishDate], [Description], [Active]) VALUES (4, 4, 3, CAST(N'2022-01-03' AS Date), CAST(N'2023-01-03' AS Date), NULL, 1)
INSERT [dbo].[Contracts] ([ContractID], [CustomerID], [CourseID], [CreateDate], [FinishDate], [Description], [Active]) VALUES (5, 5, 2, CAST(N'2022-04-01' AS Date), CAST(N'2022-11-01' AS Date), NULL, 1)
SET IDENTITY_INSERT [dbo].[Contracts] OFF
GO
SET IDENTITY_INSERT [dbo].[Courses] ON 

INSERT [dbo].[Courses] ([CourseID], [Name], [Description], [Price], [Duration], [Active]) VALUES (1, N'Bronze', N'1 tháng', 400000, 30, 1)
INSERT [dbo].[Courses] ([CourseID], [Name], [Description], [Price], [Duration], [Active]) VALUES (2, N'Silver', N'6 tháng', 2100000, 180, 1)
INSERT [dbo].[Courses] ([CourseID], [Name], [Description], [Price], [Duration], [Active]) VALUES (3, N'Gold', N'12 tháng', 3600000, 365, 1)
INSERT [dbo].[Courses] ([CourseID], [Name], [Description], [Price], [Duration], [Active]) VALUES (4, N'Platinum', N'36 tháng', 9000000, 1095, 1)
SET IDENTITY_INSERT [dbo].[Courses] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerID], [Name], [IdentityNumber], [Gender], [Birthday], [Phone], [Email], [Address], [Avatar], [Active]) VALUES (1, N'Nguyễn Quốc Vinh', N'079202010494', N'Nam', CAST(N'2002-05-09' AS Date), N'0899052002', N'quocvinh@gmail.com                                ', NULL, NULL, 0)
INSERT [dbo].[Customers] ([CustomerID], [Name], [IdentityNumber], [Gender], [Birthday], [Phone], [Email], [Address], [Avatar], [Active]) VALUES (2, N'Hồ Mạnh Tiến', N'079202010595', N'Nam', CAST(N'2002-01-28' AS Date), N'0899055222', N'manhtien@gmail.com                                ', NULL, NULL, 1)
INSERT [dbo].[Customers] ([CustomerID], [Name], [IdentityNumber], [Gender], [Birthday], [Phone], [Email], [Address], [Avatar], [Active]) VALUES (3, N'Trần Nguyễn Gia Kỳ', N'079202010555', N'Nữ', CAST(N'2002-10-21' AS Date), N'0890952254', N'giaky@gmail.com                                   ', NULL, NULL, 1)
INSERT [dbo].[Customers] ([CustomerID], [Name], [IdentityNumber], [Gender], [Birthday], [Phone], [Email], [Address], [Avatar], [Active]) VALUES (4, N'Trần Tuấn Khôi', N'079222211111', N'Nam', CAST(N'2002-08-14' AS Date), N'0909086755', N'tuankhoi@gmail.com                                ', NULL, NULL, 1)
INSERT [dbo].[Customers] ([CustomerID], [Name], [IdentityNumber], [Gender], [Birthday], [Phone], [Email], [Address], [Avatar], [Active]) VALUES (5, N'Trương Quốc Huân', N'079090855566', N'Nam', CAST(N'2002-09-07' AS Date), N'0809052215', N'quochuan@gmail.com                                ', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Facilities] ON 

INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (1, N'Ghế tập tạ GM4380', 1, 2, 6150000, NULL)
INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (2, N'Máy đẩy ngực Impluse IT9301', 1, 1, 30950000, NULL)
INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (3, N'Máy tập tay trước ImpulseIT8103', 2, 1, 22490000, NULL)
INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (4, N'Máy tập tay trước TigerSport TGP680', 2, 1, 72000000, NULL)
INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (5, N'Máy nhấn tay sau Impulse IE9517', 3, 1, 35000000, NULL)
INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (6, N'Máy tập cơ tay sau TigerSport Premium TGP690', 3, 1, 65990000, NULL)
INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (7, N'Máy tập ép đùi IMPULSE IF8116', 4, 1, 23500000, NULL)
INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (8, N'Máy gập bụng Toshiko TB01', 5, 2, 3830000, NULL)
INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (9, N'Máy gập bụng TB01', 5, 2, 1200000, NULL)
INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (10, N'Máy ngồi móc đùi sau Impulse IF8106', 6, 1, 24950000, NULL)
INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (11, N'Giàn tạ đa năng BP-806', 7, 2, 14950000, NULL)
INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (12, N'Kệ Ngang Để Tạ Dumbell 10 Cặp Alex', 8, 2, 12000000, NULL)
INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (13, N'Thanh Bar Thẳng Chromed Olympic Bar Alex', 9, 5, 2000000, NULL)
INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (14, N'Tạ đòn cong ALEX chất liệu TPU PS-TPUC', 9, 5, 2450000, NULL)
INSERT [dbo].[Facilities] ([FacilityID], [Name], [TypeID], [Quantity], [PricePerUnit], [Description]) VALUES (15, N'Bộ combo 50kg tạ', 10, 2, 1500000, NULL)
SET IDENTITY_INSERT [dbo].[Facilities] OFF
GO
SET IDENTITY_INSERT [dbo].[PTContracts] ON 

INSERT [dbo].[PTContracts] ([PTContractID], [CustomerID], [PTID], [PTCourseID], [CreateDate], [FinishDate], [Description], [Active]) VALUES (1, 1, 1, 1, CAST(N'2022-05-09' AS Date), CAST(N'2022-06-09' AS Date), NULL, 0)
INSERT [dbo].[PTContracts] ([PTContractID], [CustomerID], [PTID], [PTCourseID], [CreateDate], [FinishDate], [Description], [Active]) VALUES (2, 2, 1, 2, CAST(N'2022-12-01' AS Date), CAST(N'2023-01-15' AS Date), NULL, 1)
INSERT [dbo].[PTContracts] ([PTContractID], [CustomerID], [PTID], [PTCourseID], [CreateDate], [FinishDate], [Description], [Active]) VALUES (3, 3, 2, 4, CAST(N'2022-10-01' AS Date), CAST(N'2023-02-01' AS Date), NULL, 1)
INSERT [dbo].[PTContracts] ([PTContractID], [CustomerID], [PTID], [PTCourseID], [CreateDate], [FinishDate], [Description], [Active]) VALUES (4, 4, 3, 3, CAST(N'2022-01-03' AS Date), CAST(N'2022-03-03' AS Date), NULL, 0)
INSERT [dbo].[PTContracts] ([PTContractID], [CustomerID], [PTID], [PTCourseID], [CreateDate], [FinishDate], [Description], [Active]) VALUES (5, 5, 1, 2, CAST(N'2022-04-01' AS Date), CAST(N'2022-05-15' AS Date), NULL, 0)
SET IDENTITY_INSERT [dbo].[PTContracts] OFF
GO
SET IDENTITY_INSERT [dbo].[PTCourses] ON 

INSERT [dbo].[PTCourses] ([PTCourseID], [Name], [NumberOfSession], [Price], [Duration], [Active]) VALUES (1, N'PTBronze', 3, 900000, 30, 1)
INSERT [dbo].[PTCourses] ([PTCourseID], [Name], [NumberOfSession], [Price], [Duration], [Active]) VALUES (2, N'PTSilver', 6, 1500000, 45, 1)
INSERT [dbo].[PTCourses] ([PTCourseID], [Name], [NumberOfSession], [Price], [Duration], [Active]) VALUES (3, N'PTGold', 12, 3000000, 60, 1)
INSERT [dbo].[PTCourses] ([PTCourseID], [Name], [NumberOfSession], [Price], [Duration], [Active]) VALUES (4, N'PTPlatinum', 24, 5000000, 90, 1)
INSERT [dbo].[PTCourses] ([PTCourseID], [Name], [NumberOfSession], [Price], [Duration], [Active]) VALUES (5, N'PTDiamond', 48, 8000000, 180, 1)
SET IDENTITY_INSERT [dbo].[PTCourses] OFF
GO
SET IDENTITY_INSERT [dbo].[PTs] ON 

INSERT [dbo].[PTs] ([PTID], [Name], [IdentityNumber], [Gender], [Birthday], [Phone], [Email], [Address], [Avatar], [Active]) VALUES (1, N'Trần Thành Nhân', N'079222215554', N'Nam', CAST(N'2002-08-23' AS Date), N'0902344835', N'thanhnhan@gmail.com                               ', N'Quận 7', NULL, 1)
INSERT [dbo].[PTs] ([PTID], [Name], [IdentityNumber], [Gender], [Birthday], [Phone], [Email], [Address], [Avatar], [Active]) VALUES (2, N'Phạm Văn Mách', N'09995664225', N'Nam', CAST(N'1991-05-05' AS Date), N'0902221555', N'vanmach@gmail.com                                 ', N'Quận 3', NULL, 1)
INSERT [dbo].[PTs] ([PTID], [Name], [IdentityNumber], [Gender], [Birthday], [Phone], [Email], [Address], [Avatar], [Active]) VALUES (3, N'Võ Anh Kiệt', N'552205913569', N'Nam', CAST(N'1989-01-01' AS Date), N'0987654321', N'anhkiet@gmail.com                                 ', N'Quận 6', NULL, 1)
INSERT [dbo].[PTs] ([PTID], [Name], [IdentityNumber], [Gender], [Birthday], [Phone], [Email], [Address], [Avatar], [Active]) VALUES (4, N'Phạm Nguyễn Phương Vy', N'079202016659', N'Nữ', CAST(N'2002-06-06' AS Date), N'0909056612', N'phuongvy@gmail.com                                ', N'Quận 8', NULL, 0)
SET IDENTITY_INSERT [dbo].[PTs] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [Name], [Description]) VALUES (1, N'Admin     ', N'Full chức năng')
INSERT [dbo].[Roles] ([RoleID], [Name], [Description]) VALUES (2, N'Staff     ', N'Xem')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[StaffID] ON 

INSERT [dbo].[StaffID] ([StaffID], [Name], [IdentityNumber], [Gender], [Birthday], [Phone], [Email], [Address], [Avatar], [RoleID], [Active]) VALUES (1, N'Lê Thị Khánh Huyền', N'079222222222', N'Nữ', CAST(N'2022-06-26' AS Date), N'0342471767', N'khanhhuyen@gmail.com                              ', N'Quận 7', NULL, 1, 1)
INSERT [dbo].[StaffID] ([StaffID], [Name], [IdentityNumber], [Gender], [Birthday], [Phone], [Email], [Address], [Avatar], [RoleID], [Active]) VALUES (2, N'Phạm Thanh Hằng', N'079555512555', N'Nữ', CAST(N'2022-06-05' AS Date), N'0123354466', N'thanhhang@gmail.com                               ', N'Quận 11', NULL, 1, 1)
INSERT [dbo].[StaffID] ([StaffID], [Name], [IdentityNumber], [Gender], [Birthday], [Phone], [Email], [Address], [Avatar], [RoleID], [Active]) VALUES (3, N'Hà Quang Tín', N'055998815969', N'Nam', CAST(N'2002-02-22' AS Date), N'0559966315', N'quangtin@gmail.com                                ', N'Quận 8', NULL, 2, 0)
INSERT [dbo].[StaffID] ([StaffID], [Name], [IdentityNumber], [Gender], [Birthday], [Phone], [Email], [Address], [Avatar], [RoleID], [Active]) VALUES (4, N'Du Gia Tuấn', N'012336699494', N'Nam', CAST(N'2002-06-05' AS Date), N'0123456777', N'giatuan@gmail.com                                 ', N'Quận 1', NULL, 2, 1)
INSERT [dbo].[StaffID] ([StaffID], [Name], [IdentityNumber], [Gender], [Birthday], [Phone], [Email], [Address], [Avatar], [RoleID], [Active]) VALUES (5, N'Trần Bảo Tín', N'059202010496', N'Nam', CAST(N'2002-06-08' AS Date), N'0125579522', N'baotin@gmail.com                                  ', N'Quận Phú Nhuận', NULL, 2, 1)
SET IDENTITY_INSERT [dbo].[StaffID] OFF
GO
SET IDENTITY_INSERT [dbo].[TypesOfFacility] ON 

INSERT [dbo].[TypesOfFacility] ([TypeID], [Name], [Description]) VALUES (1, N'Máy tập cơ ngực', NULL)
INSERT [dbo].[TypesOfFacility] ([TypeID], [Name], [Description]) VALUES (2, N'Máy tập tay trước', NULL)
INSERT [dbo].[TypesOfFacility] ([TypeID], [Name], [Description]) VALUES (3, N'Máy tập tay sau', NULL)
INSERT [dbo].[TypesOfFacility] ([TypeID], [Name], [Description]) VALUES (4, N'Máy tập cơ đùi', NULL)
INSERT [dbo].[TypesOfFacility] ([TypeID], [Name], [Description]) VALUES (5, N'Máy tập cơ bụng', NULL)
INSERT [dbo].[TypesOfFacility] ([TypeID], [Name], [Description]) VALUES (6, N'Máy tập chân sau', NULL)
INSERT [dbo].[TypesOfFacility] ([TypeID], [Name], [Description]) VALUES (7, N'Máy tập đa năng', NULL)
INSERT [dbo].[TypesOfFacility] ([TypeID], [Name], [Description]) VALUES (8, N'Tạ đơn', NULL)
INSERT [dbo].[TypesOfFacility] ([TypeID], [Name], [Description]) VALUES (9, N'Tạ đòn', NULL)
INSERT [dbo].[TypesOfFacility] ([TypeID], [Name], [Description]) VALUES (10, N'Tạ đĩa', NULL)
SET IDENTITY_INSERT [dbo].[TypesOfFacility] OFF
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_StaffID] FOREIGN KEY([StaffID])
REFERENCES [dbo].[StaffID] ([StaffID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_StaffID]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Customers] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Customers]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_PTContracts] FOREIGN KEY([PTContractID])
REFERENCES [dbo].[PTContracts] ([PTContractID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_PTContracts]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_PTCourses] FOREIGN KEY([PTContractID])
REFERENCES [dbo].[PTCourses] ([PTCourseID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_PTCourses]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_PTs] FOREIGN KEY([PTID])
REFERENCES [dbo].[PTs] ([PTID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_PTs]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_Courses] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Courses] ([CourseID])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_Courses]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_Customers] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_Customers]
GO
ALTER TABLE [dbo].[Facilities]  WITH CHECK ADD  CONSTRAINT [FK_Facilities_TypesOfFacility] FOREIGN KEY([TypeID])
REFERENCES [dbo].[TypesOfFacility] ([TypeID])
GO
ALTER TABLE [dbo].[Facilities] CHECK CONSTRAINT [FK_Facilities_TypesOfFacility]
GO
ALTER TABLE [dbo].[PTContracts]  WITH CHECK ADD  CONSTRAINT [FK_PTContracts_Customers] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[PTContracts] CHECK CONSTRAINT [FK_PTContracts_Customers]
GO
ALTER TABLE [dbo].[PTContracts]  WITH CHECK ADD  CONSTRAINT [FK_PTContracts_PTCourses] FOREIGN KEY([PTCourseID])
REFERENCES [dbo].[PTCourses] ([PTCourseID])
GO
ALTER TABLE [dbo].[PTContracts] CHECK CONSTRAINT [FK_PTContracts_PTCourses]
GO
ALTER TABLE [dbo].[PTContracts]  WITH CHECK ADD  CONSTRAINT [FK_PTContracts_PTs] FOREIGN KEY([PTID])
REFERENCES [dbo].[PTs] ([PTID])
GO
ALTER TABLE [dbo].[PTContracts] CHECK CONSTRAINT [FK_PTContracts_PTs]
GO
ALTER TABLE [dbo].[StaffID]  WITH CHECK ADD  CONSTRAINT [FK_StaffID_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[StaffID] CHECK CONSTRAINT [FK_StaffID_Roles]
GO

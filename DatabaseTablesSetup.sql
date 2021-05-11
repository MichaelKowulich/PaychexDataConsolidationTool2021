SET ANSI_NULLS ON
GO
 
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Status]    Script Date: 5/6/2021 10:45:31 PM ******/ 
CREATE TABLE [dbo].[Status](
[StatusId] [int] IDENTITY(1,1) NOT NULL,
[StatusName] [varchar](50) NOT NULL,
CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
[StatusId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ClientsPerStatus]    Script Date: 5/6/2021 10:45:10 PM ******/
CREATE TABLE [dbo].[ClientsPerStatus](
[ClientsPerStatusId] [int] IDENTITY(1,1) NOT NULL,
[DateOfReport] [date] NOT NULL,
[StatusId] [int] NOT NULL,
[StatusCountAsOfDate] [int] NOT NULL,
CONSTRAINT [PK_ClientsPerStatus] PRIMARY KEY CLUSTERED 
(
[ClientsPerStatusId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
 
ALTER TABLE [dbo].[ClientsPerStatus]  WITH CHECK ADD  CONSTRAINT [FK_ClientsPerStatus_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO

ALTER TABLE [dbo].[ClientsPerStatus] CHECK CONSTRAINT [FK_ClientsPerStatus_Status]
GO

/****** Object:  Table [dbo].[ClientType]    Script Date: 5/6/2021 10:47:08 PM ******/
CREATE TABLE [dbo].[ClientType](
[ClientTypeId] [int] IDENTITY(1,1) NOT NULL,
[ClientTypeName] [varchar](100) NOT NULL,
CONSTRAINT [PK_ClientType] PRIMARY KEY CLUSTERED 
(
[ClientTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ClientsPerType]    Script Date: 5/6/2021 10:47:59 PM ******/
CREATE TABLE [dbo].[ClientsPerType](
[ClientsPerTypeId] [int] IDENTITY(1,1) NOT NULL,
[DateOfReport] [date] NOT NULL,
[ClientTypeId] [int] NOT NULL,
[TypeCountAsOfDate] [int] NOT NULL,
CONSTRAINT [PK_ClientsPerType] PRIMARY KEY CLUSTERED 
(
[ClientsPerTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
 
ALTER TABLE [dbo].[ClientsPerType]  WITH CHECK ADD  CONSTRAINT [FK_ClientsPerType_ClientType] FOREIGN KEY([ClientTypeId])
REFERENCES [dbo].[ClientType] ([ClientTypeId])
GO

ALTER TABLE [dbo].[ClientsPerType] CHECK CONSTRAINT [FK_ClientsPerType_ClientType]
GO

/****** Object:  Table [dbo].[UserType]    Script Date: 5/6/2021 10:49:03 PM ******/
CREATE TABLE [dbo].[UserType](
[UserTypeId] [int] IDENTITY(1,1) NOT NULL,
[UserTypeName] [varchar](100) NOT NULL,
CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
[UserTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[UsersPerType]    Script Date: 5/6/2021 10:49:11 PM ******/
CREATE TABLE [dbo].[UsersPerType](
[UsersPerTypeId] [int] IDENTITY(1,1) NOT NULL,
[DateOfReport] [date] NOT NULL,
[UserTypeId] [int] NOT NULL,
[UserTypeCountAsOfDate] [int] NOT NULL,
CONSTRAINT [PK_UsersPerType] PRIMARY KEY CLUSTERED 
(
[UsersPerTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
 
ALTER TABLE [dbo].[UsersPerType]  WITH CHECK ADD  CONSTRAINT [FK_UsersPerType_UserType] FOREIGN KEY([UserTypeId])
REFERENCES [dbo].[UserType] ([UserTypeId])
GO

ALTER TABLE [dbo].[UsersPerType] CHECK CONSTRAINT [FK_UsersPerType_UserType]
GO

/****** Object:  Table [dbo].[ClientBrand]    Script Date: 5/6/2021 10:50:27 PM ******/
CREATE TABLE [dbo].[ClientBrand](
[ClientBrandId] [int] IDENTITY(1,1) NOT NULL,
[ClientBrandName] [varchar](50) NOT NULL,
CONSTRAINT [PK_ClientBrand] PRIMARY KEY CLUSTERED 
(
[ClientBrandId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ClientsPerBrandCountType]    Script Date: 5/6/2021 10:50:45 PM ******/
CREATE TABLE [dbo].[ClientsPerBrandCountType](
[ClientsPerBrandCountTypeId] [int] IDENTITY(1,1) NOT NULL,
[ClientsPerBrandCountTypeName] [varchar](50) NOT NULL,
CONSTRAINT [PK_ClientsPerBrandCountType] PRIMARY KEY CLUSTERED 
(
[ClientsPerBrandCountTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ClientsPerBrand]    Script Date: 5/6/2021 10:51:06 PM ******/
CREATE TABLE [dbo].[ClientsPerBrand](
[ClientsPerBrandId] [int] IDENTITY(1,1) NOT NULL,
[DateOfReport] [date] NOT NULL,
[ClientBrandId] [int] NOT NULL,
[ClientsPerBrandCountTypeId] [int] NOT NULL,
[CountAsOfDate] [int] NOT NULL,
CONSTRAINT [PK_ClientsPerBrand] PRIMARY KEY CLUSTERED 
(
[ClientsPerBrandId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
 
ALTER TABLE [dbo].[ClientsPerBrand]  WITH CHECK ADD  CONSTRAINT [FK_ClientsPerBrand_ClientBrand] FOREIGN KEY([ClientBrandId])
REFERENCES [dbo].[ClientBrand] ([ClientBrandId])
GO
 
ALTER TABLE [dbo].[ClientsPerBrand] CHECK CONSTRAINT [FK_ClientsPerBrand_ClientBrand]
GO
 
ALTER TABLE [dbo].[ClientsPerBrand]  WITH CHECK ADD  CONSTRAINT [FK_ClientsPerBrand_ClientsPerBrandCountType] FOREIGN KEY([ClientsPerBrandCountTypeId])
REFERENCES [dbo].[ClientsPerBrandCountType] ([ClientsPerBrandCountTypeId])
GO
 
ALTER TABLE [dbo].[ClientsPerBrand] CHECK CONSTRAINT [FK_ClientsPerBrand_ClientsPerBrandCountType]
GO





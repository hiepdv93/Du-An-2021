USE [master]
GO
/****** Object:  Database [TpAutoLab_test]    Script Date: 13/06/2017 04:19:27 ******/
CREATE DATABASE [TpAutoLab_test]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TpAutoLab_test', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\TpAutoLab_test.mdf' , SIZE = 7168KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TpAutoLab_test_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\TpAutoLab_test_log.ldf' , SIZE = 5184KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TpAutoLab_test] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TpAutoLab_test].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TpAutoLab_test] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET ARITHABORT OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [TpAutoLab_test] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TpAutoLab_test] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TpAutoLab_test] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TpAutoLab_test] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TpAutoLab_test] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TpAutoLab_test] SET  MULTI_USER 
GO
ALTER DATABASE [TpAutoLab_test] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TpAutoLab_test] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TpAutoLab_test] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TpAutoLab_test] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [TpAutoLab_test]
GO
/****** Object:  Table [dbo].[AttachFile]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AttachFile](
	[AttachFileId] [varchar](128) NOT NULL,
	[AttachFileName] [nvarchar](250) NULL,
	[AttachFileLink] [nvarchar](500) NULL,
	[FileSize] [int] NULL,
	[AttachFileType] [nvarchar](20) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AttachFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [varchar](128) NOT NULL,
	[ParentId] [varchar](128) NULL,
	[OrderNo] [int] NULL,
	[CateFunctionName] [nvarchar](500) NULL,
	[LinkSite] [nvarchar](1000) NULL,
	[CateFunctionComment] [nvarchar](500) NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CategoryFunction]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CategoryFunction](
	[CategoryFunctionId] [varchar](128) NOT NULL,
	[CategoryId] [varchar](128) NULL,
	[ListFunctionId] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryFunctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Class]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Class](
	[ClassId] [varchar](128) NOT NULL,
	[DepartmentId] [varchar](128) NULL,
	[TrainningSystemId] [varchar](128) NULL,
	[CourseId] [varchar](128) NULL,
	[VocationalId] [varchar](128) NULL,
	[ClassCode] [varchar](100) NULL,
	[ClassName] [nvarchar](250) NULL,
	[AcademicYear] [int] NULL,
	[NumberStudent] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
	[TeacherId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ClassSubject]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClassSubject](
	[ClassSubjectId] [varchar](128) NOT NULL,
	[ClassId] [varchar](128) NULL,
	[SubjectId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[ClassSubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Course]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [varchar](128) NOT NULL,
	[TrainningSystemId] [varchar](128) NULL,
	[CourseCode] [varchar](100) NULL,
	[CourseName] [nvarchar](250) NULL,
	[FromYear] [datetime] NULL,
	[ToYear] [datetime] NULL,
	[Comment] [nvarchar](500) NULL,
	[CourseType] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DbError]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DbError](
	[IDX] [varchar](128) NOT NULL,
	[Err_Date] [datetime] NULL,
	[sp_name] [nvarchar](250) NULL,
	[Err_Number] [int] NULL,
	[Err_Message] [ntext] NULL,
	[Err_Line] [int] NULL,
	[Err_State] [int] NULL,
	[Err_Proc] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[IDX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Degree]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Degree](
	[DegreeId] [varchar](128) NOT NULL,
	[DegreeName] [nvarchar](250) NULL,
	[Comment] [nvarchar](500) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DegreeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DegreeStudent]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DegreeStudent](
	[DegreeStudentId] [varchar](128) NOT NULL,
	[DegreeId] [varchar](128) NULL,
	[StudentId] [varchar](128) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DegreeStudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Department]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [varchar](128) NOT NULL,
	[DepartmentCode] [varchar](100) NULL,
	[DepartmentName] [nvarchar](250) NULL,
	[DepartmentType] [int] NULL,
	[DepartmentComment] [nvarchar](500) NULL,
	[FoundingDate] [datetime] NULL,
	[Avatar] [nvarchar](200) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DepartmentCourse]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DepartmentCourse](
	[DepartmentCourseId] [varchar](128) NOT NULL,
	[CourseId] [varchar](128) NULL,
	[DepartmentId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeppartmentTrainningSystem]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DeppartmentTrainningSystem](
	[DeppartmentTrainningSystemId] [varchar](128) NOT NULL,
	[TrainningSystemId] [varchar](128) NULL,
	[DepartmentId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[DeppartmentTrainningSystemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Device]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Device](
	[DeviceId] [varchar](128) NOT NULL,
	[DeviceTypeId] [varchar](128) NULL,
	[Name] [nvarchar](500) NULL,
	[SerialNumber] [varchar](150) NULL,
	[Model] [varchar](100) NULL,
	[ManufactureName] [nvarchar](250) NULL,
	[ManufactureDate] [varchar](10) NULL,
	[BuyDate] [datetime] NULL,
	[PurposeUses] [nvarchar](500) NULL,
	[Image] [nvarchar](250) NULL,
	[Note] [nvarchar](500) NULL,
	[ActiveStatus] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
 CONSTRAINT [PK__Device__49E12311236332E4] PRIMARY KEY CLUSTERED 
(
	[DeviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeviceType]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DeviceType](
	[DeviceTypeId] [varchar](128) NOT NULL,
	[Name] [nvarchar](128) NULL,
	[Note] [nvarchar](500) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DeviceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[District]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[District](
	[DistrictId] [varchar](128) NOT NULL,
	[LocationId] [varchar](128) NULL,
	[DistrictName] [varchar](250) NULL,
	[Status] [int] NULL,
	[Code] [varchar](100) NULL,
	[Type] [nvarchar](250) NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DistrictId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DistrictWard]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DistrictWard](
	[DistrictWardId] [varchar](128) NOT NULL,
	[DistrictId] [varchar](128) NULL,
	[WardName] [nvarchar](250) NULL,
	[Status] [int] NULL,
	[Code] [varchar](100) NULL,
	[Type] [nvarchar](250) NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DistrictWardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Function]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Function](
	[FunctionId] [varchar](128) NOT NULL,
	[GroupFunctionId] [varchar](128) NULL,
	[Code] [nvarchar](100) NULL,
	[Name] [nvarchar](500) NULL,
	[Description] [ntext] NULL,
	[OrderNo] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[FunctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Group]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Group](
	[GroupId] [varchar](128) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Status] [int] NULL,
	[Description] [nvarchar](500) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GroupFunction]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GroupFunction](
	[GroupFunctionId] [varchar](128) NOT NULL,
	[Code] [nvarchar](100) NULL,
	[Name] [nvarchar](500) NULL,
	[Description] [ntext] NULL,
	[OrderNo] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupFunctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GroupPermission]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GroupPermission](
	[GroupPermissionId] [varchar](128) NOT NULL,
	[GroupId] [varchar](128) NULL,
	[FunctionId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupPermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Lecture]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lecture](
	[LectureId] [varchar](128) NOT NULL,
	[SubjectId] [varchar](128) NULL,
	[VocationalId] [varchar](128) NULL,
	[LectureCode] [varchar](100) NULL,
	[LectureName] [nvarchar](250) NULL,
	[LectureDescription] [ntext] NULL,
	[ApproveStatus] [int] NULL,
	[OrderNo] [int] NULL,
	[SourceAuthor] [nvarchar](2000) NULL,
	[DurationTime] [int] NULL,
	[SourceType] [int] NULL,
	[SourceOtherName] [nvarchar](500) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[ApproveBy] [varchar](128) NULL,
	[ApproveDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
	[Note] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[LectureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LectureAttach]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LectureAttach](
	[LectureAttachId] [varchar](128) NOT NULL,
	[AttachFileId] [varchar](128) NULL,
	[LectureId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[LectureAttachId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LectureFavorite]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LectureFavorite](
	[LectureFavoriteId] [varchar](128) NOT NULL,
	[DepartmentId] [varchar](128) NULL,
	[TrainningSystemId] [varchar](128) NULL,
	[VocationId] [varchar](128) NULL,
	[SubjectId] [varchar](128) NULL,
	[SkillId] [varchar](128) NULL,
	[LectureId] [varchar](128) NULL,
	[FavoriteUserId] [varchar](128) NULL,
	[Rating] [float] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[LectureFavoriteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LectureHistory]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LectureHistory](
	[HistoryId] [varchar](128) NOT NULL,
	[LectureId] [varchar](128) NULL,
	[UserId] [varchar](128) NULL,
	[StudentId] [varchar](128) NULL,
	[ClassId] [varchar](128) NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[HistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LectureModule]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LectureModule](
	[LectureModuleId] [varchar](128) NOT NULL,
	[ModuleId] [varchar](128) NULL,
	[LectureId] [varchar](128) NULL,
	[Description] [nvarchar](500) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[LectureModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LecturePage]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LecturePage](
	[LecturePageId] [varchar](128) NOT NULL,
	[LectureId] [varchar](128) NULL,
	[ParentId] [varchar](128) NULL,
	[LecturePageTitle] [nvarchar](500) NULL,
	[LectureContent] [ntext] NULL,
	[OrderNo] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[LecturePageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LectureTeach]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LectureTeach](
	[LectureTeachId] [varchar](128) NOT NULL,
	[LectureId] [varchar](128) NULL,
	[LectureUserId] [varchar](128) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[LectureTeachId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LectureTrainningSystem]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LectureTrainningSystem](
	[LectureTrainningSystem] [varchar](128) NOT NULL,
	[LectureId] [varchar](128) NULL,
	[TrainningSystemId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[LectureTrainningSystem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LectureUserNote]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LectureUserNote](
	[LectureUserNoteId] [varchar](128) NOT NULL,
	[LectureId] [varchar](128) NULL,
	[StudentId] [varchar](128) NULL,
	[NoteContent] [nvarchar](2000) NULL,
	[AttachFileId] [varchar](128) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[LectureUserNoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Location]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Location](
	[LocationId] [varchar](128) NOT NULL,
	[NationalId] [varchar](128) NULL,
	[LocationName] [nvarchar](250) NULL,
	[Status] [int] NULL,
	[Code] [varchar](100) NULL,
	[Type] [nvarchar](250) NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Module]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Module](
	[ModuleId] [varchar](128) NOT NULL,
	[ModuleCode] [varchar](100) NULL,
	[ModuleName] [nvarchar](250) NULL,
	[Quantity] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Notification](
	[NotificationId] [varchar](128) NOT NULL,
	[NotificationContent] [ntext] NULL,
	[NotificationType] [int] NULL,
	[OrderNo] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PanValue]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PanValue](
	[Id] [varchar](128) NOT NULL,
	[PanValueIndex] [int] NULL,
	[Name] [nvarchar](250) NULL,
	[Type] [nvarchar](500) NULL,
	[Value] [int] NULL,
	[StudentValue] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Permission](
	[PermissionId] [varchar](128) NOT NULL,
	[PermissionName] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Practice]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Practice](
	[PracticeId] [varchar](128) NOT NULL,
	[VocationalId] [varchar](128) NULL,
	[SubjectId] [varchar](128) NULL,
	[PracticeName] [nvarchar](250) NULL,
	[PracticeCode] [varchar](100) NULL,
	[PracticeFilePath] [nvarchar](250) NULL,
	[PracticePrivateKey] [varchar](250) NULL,
	[OrderNo] [int] NULL,
	[Author] [nvarchar](250) NULL,
	[ApproveStatus] [int] NULL,
	[Description] [nvarchar](500) NULL,
	[PracticeType] [int] NULL,
	[Model] [nvarchar](250) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
	[Note] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[PracticeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PracticeAttach]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PracticeAttach](
	[PracticeAttachId] [varchar](128) NOT NULL,
	[PracticeId] [varchar](128) NULL,
	[AttachId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[PracticeAttachId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PracticeBackup]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PracticeBackup](
	[PracticeId] [varchar](128) NOT NULL,
	[VocationalId] [varchar](128) NULL,
	[SubjectId] [varchar](128) NULL,
	[PracticeName] [nvarchar](250) NULL,
	[PracticeCode] [varchar](100) NULL,
	[PracticeFilePath] [nvarchar](250) NULL,
	[PracticePrivateKey] [varchar](250) NULL,
	[OrderNo] [int] NULL,
	[Author] [nvarchar](250) NULL,
	[ApproveStatus] [int] NULL,
	[Description] [nvarchar](500) NULL,
	[PracticeType] [int] NULL,
	[Model] [nvarchar](250) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PracticeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PracticeGroup]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PracticeGroup](
	[PracticeGroupId] [varchar](128) NOT NULL,
	[PracticeGroupName] [nvarchar](250) NULL,
	[Address] [nvarchar](500) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[PracticeGroupState] [int] NULL,
	[PracticeGroupStatus] [int] NULL,
	[TeacherId] [varchar](128) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PracticeGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PracticeGroupStudent]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PracticeGroupStudent](
	[PracticeStudentId] [varchar](128) NOT NULL,
	[PracticeId] [varchar](128) NULL,
	[PracticeGroupId] [varchar](128) NULL,
	[StudentId] [varchar](128) NULL,
	[PracticeStudentState] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PracticeStudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PracticeInput]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PracticeInput](
	[PracticeInputId] [varchar](128) NOT NULL,
	[PracticeOutputId] [varchar](128) NULL,
	[pName] [nvarchar](250) NULL,
	[pType] [nvarchar](250) NULL,
	[pValue] [nvarchar](250) NULL,
	[pUnit] [nvarchar](250) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PracticeInputId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PracticeModule]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PracticeModule](
	[PracticeModuleId] [varchar](128) NOT NULL,
	[PracticeId] [varchar](128) NULL,
	[ModuleId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[PracticeModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PracticeOutput]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PracticeOutput](
	[PracticeOutputId] [varchar](128) NOT NULL,
	[PracticeId] [varchar](128) NULL,
	[StudentId] [varchar](128) NULL,
	[Result] [nvarchar](500) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PracticeOutputId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PracticeSimResult]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PracticeSimResult](
	[PracticeSimResultId] [varchar](128) NOT NULL,
	[PracticeId] [varchar](128) NULL,
	[StudentId] [varchar](128) NULL,
	[JsonResult] [ntext] NULL,
	[Type] [int] NULL,
	[IndexCount] [int] NULL,
	[Comment] [nvarchar](500) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PracticeSimResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PracticeType]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PracticeType](
	[PracticeTypeId] [varchar](128) NOT NULL,
	[PracticeTypeName] [nvarchar](250) NULL,
	[PracticeTypeStatus] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PracticeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PracticeUserNote]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PracticeUserNote](
	[PracticeUserNoteId] [varchar](128) NOT NULL,
	[PracticeId] [varchar](128) NULL,
	[StudentId] [varchar](128) NULL,
	[NoteContent] [nvarchar](2000) NULL,
	[AttachFileId] [varchar](128) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PracticeUserNoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Question]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Question](
	[QuestionId] [varchar](128) NOT NULL,
	[VocationalId] [varchar](128) NULL,
	[SubjectId] [varchar](128) NULL,
	[QuestionContent] [ntext] NULL,
	[AnswerIdCorrect] [varchar](128) NULL,
	[QuestionType] [int] NULL,
	[ApproveStatus] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[ApproveBy] [varchar](128) NULL,
	[ApproveDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
	[Note] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuestionAnswer]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuestionAnswer](
	[QuestionAnswerId] [varchar](128) NOT NULL,
	[QuestionId] [varchar](128) NULL,
	[AnswerName] [nvarchar](500) NULL,
	[AnswerCorrect] [int] NULL,
	[AnswerOrder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionAnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuestionSkill]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuestionSkill](
	[QuestionSkillId] [varchar](128) NOT NULL,
	[QuestionId] [varchar](128) NULL,
	[SkillId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionSkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuestionTrainningSystem]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuestionTrainningSystem](
	[QuestionTrainningSystemId] [varchar](128) NOT NULL,
	[TrainningSystemId] [varchar](128) NULL,
	[QuestionId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionTrainningSystemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuickTest]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuickTest](
	[QuickTestId] [varchar](128) NOT NULL,
	[QuizzesId] [varchar](128) NULL,
	[StudentId] [varchar](128) NULL,
	[TotalTime] [int] NULL,
	[TotalPoint] [float] NULL,
	[CountIndex] [int] NULL,
	[LogQuizzes] [ntext] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[QuickTestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuickTestAnswer]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuickTestAnswer](
	[QuickTestAnswerId] [varchar](128) NOT NULL,
	[QuickTestId] [varchar](128) NULL,
	[QuestionId] [varchar](128) NULL,
	[QuestionAnswerId] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[QuickTestAnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Quizzes]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Quizzes](
	[QuizzesId] [varchar](128) NOT NULL,
	[TrainningSystemId] [varchar](128) NULL,
	[VocationId] [varchar](128) NULL,
	[SubjectId] [varchar](128) NULL,
	[LectureId] [varchar](128) NULL,
	[QuizzesName] [nvarchar](250) NULL,
	[QuizzesTime] [int] NULL,
	[QuizzesType] [int] NULL,
	[ApproveStatus] [int] NULL,
	[ApproveBy] [varchar](128) NULL,
	[ApproveDate] [datetime] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
	[Note] [nvarchar](500) NULL,
 CONSTRAINT [PK__Quizzes__8032D152DC026276] PRIMARY KEY CLUSTERED 
(
	[QuizzesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuizzesAnswer]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuizzesAnswer](
	[QuizzesAnswerId] [varchar](128) NOT NULL,
	[QuizzesId] [varchar](128) NULL,
	[QuestionId] [varchar](128) NULL,
	[Result] [int] NULL,
	[StudentId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[QuizzesAnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuizzesAttach]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuizzesAttach](
	[QuizzesAttachId] [varchar](128) NOT NULL,
	[QuizzesId] [varchar](128) NULL,
	[AttachId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[QuizzesAttachId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuizzesQuestion]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuizzesQuestion](
	[QuizzesQuestionId] [varchar](128) NOT NULL,
	[QuestionId] [varchar](128) NULL,
	[QuizzesId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[QuizzesQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuizzesRoom]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuizzesRoom](
	[QuizzesRoomId] [varchar](128) NOT NULL,
	[QuizzesId] [varchar](128) NULL,
	[QuizzesRoomName] [nvarchar](250) NULL,
	[Address] [nvarchar](250) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[QuizzesRoomState] [int] NULL,
	[QuizzesRoomStatus] [int] NULL,
	[TeacherId] [varchar](128) NULL,
	[ClassId] [varchar](128) NULL,
	[VocationId] [varchar](128) NULL,
	[SubjectId] [varchar](128) NULL,
	[QuizzesListId] [nvarchar](500) NULL,
	[DepartmentId] [varchar](128) NULL,
	[TestDate] [datetime] NULL,
	[TestTime] [int] NULL,
	[MixingQuestion] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
	[Note] [nvarchar](500) NULL,
 CONSTRAINT [PK__QuizzesR__84DBB589C319D4AF] PRIMARY KEY CLUSTERED 
(
	[QuizzesRoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuizzesRoomStudent]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuizzesRoomStudent](
	[QuizzesStudentId] [varchar](128) NOT NULL,
	[QuizzesRoomId] [varchar](128) NULL,
	[QuizzesId] [varchar](128) NULL,
	[StudentId] [varchar](128) NULL,
	[QuizzesStudentState] [int] NULL,
	[TotalTime] [int] NULL,
	[Reason] [nvarchar](500) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[QuestionContent] [ntext] NULL,
	[CorrectAnswer] [int] NULL,
	[Scores] [float] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[QuizzesStudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuizzesTrainningSystem]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuizzesTrainningSystem](
	[QuizzesTrainningSystem] [varchar](128) NOT NULL,
	[QuizzesId] [varchar](128) NULL,
	[TrainningSystemId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[QuizzesTrainningSystem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RePracticeType]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RePracticeType](
	[RePracticeTypeId] [varchar](128) NOT NULL,
	[PracticeTypeId] [varchar](128) NULL,
	[PracticeId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[RePracticeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReStudentClass]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReStudentClass](
	[StudentClassId] [varchar](128) NOT NULL,
	[ClassId] [varchar](128) NULL,
	[StudentId] [varchar](128) NULL,
	[StudentClassType] [int] NULL,
	[StudentClassStatus] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReSubjectSkill]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReSubjectSkill](
	[SubjectskillId] [char](128) NOT NULL,
	[SubjectId] [varchar](128) NULL,
	[SkillId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[SubjectskillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReTeacherClass]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReTeacherClass](
	[TeacherClassId] [varchar](128) NOT NULL,
	[UserId] [varchar](128) NULL,
	[ClassId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[TeacherClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReVocationalSubject]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReVocationalSubject](
	[VocationalSubjectId] [varchar](128) NOT NULL,
	[VocationalId] [varchar](128) NULL,
	[SubjectId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[VocationalSubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoomLab]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoomLab](
	[RoomLabId] [varchar](128) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Address] [nvarchar](500) NULL,
	[Note] [nvarchar](500) NULL,
	[ManageBy] [varchar](128) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomLabId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoomLabDevice]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoomLabDevice](
	[RoomLabDeviceId] [varchar](128) NOT NULL,
	[RoomLabId] [varchar](128) NULL,
	[DeviceId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomLabDeviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[School]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[School](
	[SchoolId] [varchar](128) NOT NULL,
	[SchoolName] [nvarchar](250) NULL,
	[LocationId] [varchar](128) NULL,
	[DistrictId] [varchar](128) NULL,
	[Schoolcode] [varchar](150) NULL,
	[SchoolImagePath] [nvarchar](2000) NULL,
	[SchollSlogan] [nvarchar](250) NULL,
	[SchoolAddress] [nvarchar](500) NULL,
	[SchoolPhone] [varchar](50) NULL,
	[SchoolFax] [varchar](50) NULL,
	[SchoolWebsitePath] [nvarchar](2000) NULL,
	[SchoolFoundationDay] [datetime] NULL,
	[SchoolCreateDate] [datetime] NULL,
	[Model] [nvarchar](150) NULL,
	[MadeIn] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[SchoolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Score]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Score](
	[SoreId] [varchar](128) NOT NULL,
	[SoreValues] [float] NULL,
	[SoreNote] [nvarchar](400) NULL,
	[StudentId] [varchar](128) NULL,
	[InfoId] [varchar](128) NULL,
	[Exten2] [nvarchar](200) NULL,
	[Sindex] [int] NULL,
 CONSTRAINT [PK_Score] PRIMARY KEY CLUSTERED 
(
	[SoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Skill]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Skill](
	[SkillId] [varchar](128) NOT NULL,
	[SkillName] [nvarchar](250) NULL,
	[SkillDescription] [nvarchar](500) NULL,
	[SkillCode] [varchar](100) NULL,
	[TotalLecture] [int] NULL,
	[TotalQuizzes] [int] NULL,
	[TotalPractice] [int] NULL,
	[SkillValidUserId] [varchar](128) NULL,
	[SkillComment] [nvarchar](500) NULL,
	[OrderNo] [int] NULL,
	[VocationalId] [varchar](128) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SkillLecture]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SkillLecture](
	[SkillLectureId] [varchar](128) NOT NULL,
	[SkillId] [varchar](128) NULL,
	[LectureId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[SkillLectureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SkillPractice]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SkillPractice](
	[SkillPracticeId] [varchar](128) NOT NULL,
	[SkillId] [varchar](128) NULL,
	[PracticeId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[SkillPracticeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SkillQuizzes]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SkillQuizzes](
	[SkillQuizzesId] [varchar](128) NOT NULL,
	[QuizzesId] [varchar](128) NULL,
	[SkillId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[SkillQuizzesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SkillScore]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SkillScore](
	[SkillScoreId] [varchar](128) NOT NULL,
	[SkillId] [varchar](128) NULL,
	[SubjectScoreId] [varchar](128) NULL,
	[StudentId] [varchar](128) NULL,
	[SkillType] [int] NULL,
	[SkillScoreType] [int] NULL,
	[SkillScorePoint] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[SkillScoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SoreInfo]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SoreInfo](
	[SoreInfoId] [varchar](128) NOT NULL,
	[SoreInfoName] [nvarchar](250) NULL,
	[SoreInfoDate] [datetime] NULL,
	[SubjectId] [varchar](128) NULL,
	[ClassId] [varchar](128) NULL,
	[SoreInfoType] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
	[Exten1] [nvarchar](200) NULL,
 CONSTRAINT [PK_SoreInfo] PRIMARY KEY CLUSTERED 
(
	[SoreInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Student]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Student](
	[StudentId] [varchar](128) NOT NULL,
	[ClassId] [varchar](128) NULL,
	[StudentName] [nvarchar](250) NULL,
	[StudentPass] [nvarchar](250) NULL,
	[StudentCode] [nvarchar](100) NULL,
	[FullName] [nvarchar](250) NULL,
	[CourseId] [varchar](128) NULL,
	[Avatar] [nvarchar](1000) NULL,
	[Sex] [int] NULL,
	[Birthday] [datetime] NULL,
	[Email] [nvarchar](150) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[IdentifyNumber] [varchar](20) NULL,
	[Religion] [nvarchar](150) NULL,
	[LocationId] [varchar](128) NULL,
	[DistrictId] [varchar](128) NULL,
	[DistrictWardId] [varchar](128) NULL,
	[Comment] [nvarchar](500) NULL,
	[StudentType] [int] NULL,
	[Status] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
	[StudentObj] [int] NULL,
 CONSTRAINT [PK__Student__32C52B992CA11A82] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectId] [varchar](128) NOT NULL,
	[SubjectName] [nvarchar](250) NULL,
	[SubjectCode] [varchar](100) NULL,
	[TotalSkill] [int] NULL,
	[ImagePath] [nvarchar](250) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SubjectScore]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SubjectScore](
	[SubjectScoreId] [varchar](128) NOT NULL,
	[SubjectId] [varchar](128) NULL,
	[StudentId] [varchar](128) NULL,
	[ScorePoint] [int] NULL,
	[SubjectScoreType] [int] NULL,
	[SubjectScoreComment] [nvarchar](500) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SubjectScoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TrainningSystem]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TrainningSystem](
	[TrainningSystemId] [varchar](128) NOT NULL,
	[Code] [varchar](100) NULL,
	[Name] [nvarchar](250) NULL,
	[Comment] [nvarchar](500) NULL,
	[Type] [int] NULL,
	[FoundationDay] [datetime] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TrainningSystemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TrainningSystemCourse]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TrainningSystemCourse](
	[TrainningSystemCourse] [varchar](128) NOT NULL,
	[TrainningSystemId] [varchar](128) NULL,
	[CourseId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[TrainningSystemCourse] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TrainningSystemVocational]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TrainningSystemVocational](
	[TrainningSystemVocational] [varchar](128) NOT NULL,
	[VocationalId] [varchar](128) NULL,
	[TrainningSystemId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[TrainningSystemVocational] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TypeValue]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TypeValue](
	[TypeValueId] [varchar](128) NOT NULL,
	[TypeName] [nvarchar](250) NULL,
	[GroupType] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeValueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [varchar](128) NOT NULL,
	[UserName] [nvarchar](250) NULL,
	[UserPass] [varchar](250) NULL,
	[FullName] [nvarchar](250) NULL,
	[UserCode] [varchar](100) NULL,
	[Sex] [int] NULL,
	[Birthday] [datetime] NULL,
	[Email] [nvarchar](150) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[IdentifyNumber] [varchar](20) NULL,
	[Religion] [nvarchar](150) NULL,
	[Avatar] [nvarchar](1000) NULL,
	[Comment] [nvarchar](500) NULL,
	[UserType] [int] NULL,
	[Level] [int] NULL,
	[PedagogicalCertificate] [nvarchar](150) NULL,
	[SkillCertificate] [nvarchar](150) NULL,
	[Title] [nvarchar](150) NULL,
	[Status] [int] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
 CONSTRAINT [PK__User__1788CC4C5DD66158] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserDepartment]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserDepartment](
	[UserDepartmentId] [varchar](128) NOT NULL,
	[UserId] [varchar](128) NULL,
	[DepartmentId] [varchar](128) NULL,
 CONSTRAINT [PK_UserDepartment] PRIMARY KEY CLUSTERED 
(
	[UserDepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserGroup](
	[UserGroupId] [varchar](128) NOT NULL,
	[GroupId] [varchar](128) NULL,
	[UserId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserLog]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserLog](
	[UserLogId] [varchar](128) NOT NULL,
	[UserId] [varchar](128) NULL,
	[Description] [ntext] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserPermission]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserPermission](
	[UserPermissionId] [varchar](128) NOT NULL,
	[FunctionId] [varchar](128) NULL,
	[UserId] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserPermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Vocational]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vocational](
	[VocationalId] [varchar](128) NOT NULL,
	[TrainningSystemId] [varchar](128) NULL,
	[DepartmentId] [varchar](128) NULL,
	[VocationalName] [nvarchar](250) NULL,
	[ImageLink] [nvarchar](250) NULL,
	[VocationalType] [int] NULL,
	[VocationalComment] [nvarchar](500) NULL,
	[FoundingDate] [datetime] NULL,
	[VocationCode] [varchar](100) NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[VocationalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WorkProgress]    Script Date: 13/06/2017 04:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WorkProgress](
	[WorkProgressId] [varchar](128) NOT NULL,
	[UserId] [varchar](128) NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[Description] [nvarchar](500) NULL,
	[Comment] [ntext] NULL,
	[CreateBy] [varchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [varchar](128) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[WorkProgressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [TpAutoLab_test] SET  READ_WRITE 
GO

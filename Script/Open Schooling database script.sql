USE [master]
GO
/****** Object:  Database [Open_Schooling_Final_2023]    Script Date: 23-02-2023 11:33:54 ******/
CREATE DATABASE [Open_Schooling_Final_2023]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Open_Schooling_Final_2023', FILENAME = N'D:\MSSQL13.MSSQLSERVER\MSSQL\DATA\Open_Schooling_Final_2023.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Open_Schooling_Final_2023_log', FILENAME = N'D:\MSSQL13.MSSQLSERVER\MSSQL\DATA\Open_Schooling_Final_2023_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Open_Schooling_Final_2023].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET ARITHABORT OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET RECOVERY FULL 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET  MULTI_USER 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET QUERY_STORE = OFF
GO
USE [Open_Schooling_Final_2023]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [Open_Schooling_Final_2023]
GO
/****** Object:  Table [dbo].[All_Taluka]    Script Date: 23-02-2023 11:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[All_Taluka](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Taluka] [nvarchar](255) NULL,
	[District] [nvarchar](255) NULL,
	[Division] [nvarchar](255) NULL,
 CONSTRAINT [PK_All_Taluka] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Center_Login_Information]    Script Date: 23-02-2023 11:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Center_Login_Information](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UDISE_No] [float] NULL,
	[Center_Password] [nvarchar](50) NULL,
	[Division] [nvarchar](50) NULL,
	[Div_Code] [nvarchar](50) NULL,
	[District] [nvarchar](50) NULL,
	[Taluka] [nvarchar](50) NULL,
	[Contact_Center_Code] [nvarchar](50) NULL,
	[Center_Name] [nvarchar](max) NULL,
	[Contact] [float] NULL,
	[Medium] [nvarchar](50) NULL,
	[Active] [nvarchar](50) NULL,
	[District_Code] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Application_Form]    Script Date: 23-02-2023 11:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Application_Form](
	[Form_No] [nvarchar](max) NULL,
	[UDISE_No] [nvarchar](max) NULL,
	[Contact_center] [nvarchar](max) NULL,
	[Last_name] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Middle_name] [nvarchar](max) NULL,
	[Mother_name] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Pincode] [nvarchar](max) NULL,
	[Mobile_no] [nvarchar](max) NULL,
	[Place_of_birth] [nvarchar](max) NULL,
	[Date_of_birth] [nvarchar](max) NULL,
	[Adhar_no] [nvarchar](max) NULL,
	[Gender] [nvarchar](max) NULL,
	[Minority_Religion] [nvarchar](max) NULL,
	[Category] [nvarchar](max) NULL,
	[Divyang] [nvarchar](max) NULL,
	[Medium] [nvarchar](max) NULL,
	[Type_Of_User] [nvarchar](max) NULL,
	[Subjects] [nvarchar](max) NULL,
	[Subject1] [nvarchar](max) NULL,
	[Subject2] [nvarchar](max) NULL,
	[Subject3] [nvarchar](max) NULL,
	[Subject4] [nvarchar](max) NULL,
	[Subject5] [nvarchar](max) NULL,
	[EC_Year] [nvarchar](max) NULL,
	[EC_Number] [nvarchar](max) NULL,
	[LastExamYear] [nvarchar](max) NULL,
	[LastExamSeatNo] [nvarchar](max) NULL,
	[Seat_No] [nvarchar](max) NULL,
	[Year_Id] [nvarchar](max) NULL,
	[Ip] [nvarchar](max) NULL,
	[DateNow] [nvarchar](max) NULL,
	[Photo] [nvarchar](max) NULL,
	[Signature] [nvarchar](max) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Tbl_Application_Form] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Uni_Id] UNIQUE NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_CenterList]    Script Date: 23-02-2023 11:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_CenterList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[division] [nvarchar](255) NULL,
	[div_code] [nvarchar](50) NULL,
	[district] [nvarchar](255) NULL,
	[taluka] [nvarchar](255) NULL,
	[center_code] [nvarchar](255) NULL,
	[center_name] [nvarchar](255) NULL,
	[Medium] [nvarchar](255) NULL,
	[active] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_CenterList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_District]    Script Date: 23-02-2023 11:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_District](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[District] [nvarchar](50) NULL,
	[DivCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tbl_District] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_OpenSch_Result]    Script Date: 23-02-2023 11:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_OpenSch_Result](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Form_No] [nvarchar](max) NULL,
	[seatnumber] [nvarchar](max) NULL,
	[STD] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Sub1] [nvarchar](max) NULL,
	[Marks1] [nvarchar](max) NULL,
	[Sub2] [nvarchar](max) NULL,
	[Marks2] [nvarchar](max) NULL,
	[Sub3] [nvarchar](max) NULL,
	[Marks3] [nvarchar](max) NULL,
	[Sub4] [nvarchar](max) NULL,
	[Marks4] [nvarchar](max) NULL,
	[Sub5] [nvarchar](max) NULL,
	[Marks5] [nvarchar](max) NULL,
	[REMARKS] [nvarchar](max) NULL,
	[Mother_Name] [nvarchar](max) NULL,
	[Division] [nvarchar](max) NULL,
 CONSTRAINT [PK_Five] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_payment]    Script Date: 23-02-2023 11:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_payment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [nvarchar](100) NOT NULL,
	[tracking_id] [nvarchar](max) NULL,
	[bank_ref_no] [nvarchar](max) NULL,
	[order_status] [nvarchar](max) NULL,
	[failure_message] [nvarchar](max) NULL,
	[payment_mode] [nvarchar](max) NULL,
	[card_name] [nvarchar](max) NULL,
	[status_code] [nvarchar](max) NULL,
	[status_message] [nvarchar](max) NULL,
	[currency] [nvarchar](max) NULL,
	[amount_money] [nvarchar](max) NULL,
	[billing_name] [nvarchar](max) NULL,
	[billing_address] [nvarchar](max) NULL,
	[billing_city] [nvarchar](max) NULL,
	[billing_state] [nvarchar](max) NULL,
	[billing_zip] [nvarchar](max) NULL,
	[billing_country] [nvarchar](max) NULL,
	[billing_tel] [nvarchar](max) NULL,
	[billing_email] [nvarchar](max) NULL,
	[delivery_name] [nvarchar](max) NULL,
	[delivery_address] [nvarchar](max) NULL,
	[delivery_city] [nvarchar](max) NULL,
	[delivery_state] [nvarchar](max) NULL,
	[delivery_zip] [nvarchar](max) NULL,
	[delivery_country] [nvarchar](max) NULL,
	[delivery_tel] [nvarchar](max) NULL,
	[merchant_param1] [nvarchar](max) NULL,
	[merchant_param2] [nvarchar](max) NULL,
	[merchant_param3] [nvarchar](max) NULL,
	[merchant_param4] [nvarchar](max) NULL,
	[merchant_param5] [nvarchar](max) NULL,
	[vault] [nvarchar](max) NULL,
	[offer_type] [nvarchar](max) NULL,
	[offer_code] [nvarchar](max) NULL,
	[discount_value] [nvarchar](max) NULL,
	[mer_account] [nvarchar](max) NULL,
	[eci_value] [nvarchar](max) NULL,
	[retry] [nvarchar](max) NULL,
	[response_code] [nvarchar](max) NULL,
	[billing_notes] [nvarchar](max) NULL,
	[trans_date] [nvarchar](max) NULL,
	[bin_country] [nvarchar](max) NULL,
	[active] [int] NULL,
 CONSTRAINT [PK_Tbl_payment] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Registration]    Script Date: 23-02-2023 11:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Registration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Id] [nvarchar](max) NULL,
	[UID] [nvarchar](max) NULL,
	[Enrollment_No] [nvarchar](max) NULL,
	[ApplicationId] [nvarchar](max) NULL,
	[First_Name] [nvarchar](max) NULL,
	[Middle_Name] [nvarchar](max) NULL,
	[Last_Name] [nvarchar](max) NULL,
	[Mother_Name] [nvarchar](max) NULL,
	[Adhar_no] [nvarchar](50) NULL,
	[Mobile_No] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Village] [nvarchar](max) NULL,
	[District] [nvarchar](max) NULL,
	[Taluka] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[Pin_Code] [nvarchar](50) NULL,
	[Date_of_Birth] [nvarchar](max) NULL,
	[DOB_Words] [nvarchar](max) NULL,
	[DOB_Village] [nvarchar](50) NULL,
	[DOB_Taluka] [nvarchar](50) NULL,
	[DOB_District] [nvarchar](50) NULL,
	[Email] [nvarchar](max) NULL,
	[Gender] [nvarchar](50) NULL,
	[Standard] [nvarchar](max) NULL,
	[Medium] [nvarchar](max) NULL,
	[Age_Certified_Proof] [nvarchar](max) NULL,
	[District_Center] [nvarchar](50) NULL,
	[Taluka_Center] [nvarchar](50) NULL,
	[Center] [nvarchar](max) NULL,
	[Center_Code] [nvarchar](max) NULL,
	[Previous_Attend_School_YN] [nvarchar](max) NULL,
	[Self_Declaration_Not_Gone_School] [nvarchar](max) NULL,
	[Previous_Attend_School] [nvarchar](max) NULL,
	[Date_Of_Leaving_Last_Attended_School] [nvarchar](max) NULL,
	[Handicap] [nvarchar](max) NULL,
	[Candidate_Handicaped_YN] [nvarchar](max) NULL,
	[Subject_List] [nvarchar](max) NULL,
	[Subject1] [nvarchar](max) NULL,
	[Subject2] [nvarchar](max) NULL,
	[Subject3] [nvarchar](max) NULL,
	[Subject4] [nvarchar](max) NULL,
	[Subject5] [nvarchar](max) NULL,
	[Minority_Religion] [nvarchar](max) NULL,
	[Exam_Form_Disable] [nvarchar](max) NULL,
	[Type_Of_User] [nvarchar](max) NULL,
	[Nsqf_Subject] [nvarchar](max) NULL,
	[Hall_Ticket] [nvarchar](max) NULL,
	[Photo] [nvarchar](max) NULL,
	[Password] [nvarchar](50) NULL,
	[Signature] [nvarchar](max) NULL,
	[Payment_Status] [nvarchar](50) NULL,
	[Ec_Status] [nvarchar](50) NULL,
	[ip] [nvarchar](50) NULL,
	[DateNow] [datetime] NULL,
	[Year_Id] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tbl_Registration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Registration_BK]    Script Date: 23-02-2023 11:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Registration_BK](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Id] [nvarchar](max) NULL,
	[UID] [nvarchar](max) NULL,
	[Enrollment_No] [nvarchar](max) NULL,
	[ApplicationId] [nvarchar](max) NULL,
	[First_Name] [nvarchar](max) NULL,
	[Middle_Name] [nvarchar](max) NULL,
	[Last_Name] [nvarchar](max) NULL,
	[Mother_Name] [nvarchar](max) NULL,
	[Adhar_no] [nvarchar](50) NULL,
	[Mobile_No] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Village] [nvarchar](max) NULL,
	[District] [nvarchar](max) NULL,
	[Taluka] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[Pin_Code] [nvarchar](50) NULL,
	[Date_of_Birth] [nvarchar](max) NULL,
	[DOB_Words] [nvarchar](max) NULL,
	[DOB_Village] [nvarchar](50) NULL,
	[DOB_Taluka] [nvarchar](50) NULL,
	[DOB_District] [nvarchar](50) NULL,
	[Email] [nvarchar](max) NULL,
	[Gender] [nvarchar](50) NULL,
	[Standard] [nvarchar](max) NULL,
	[Medium] [nvarchar](max) NULL,
	[Age_Certified_Proof] [nvarchar](max) NULL,
	[District_Center] [nvarchar](50) NULL,
	[Taluka_Center] [nvarchar](50) NULL,
	[Center] [nvarchar](max) NULL,
	[Center_Code] [nvarchar](max) NULL,
	[Previous_Attend_School_YN] [nvarchar](max) NULL,
	[Self_Declaration_Not_Gone_School] [nvarchar](max) NULL,
	[Previous_Attend_School] [nvarchar](max) NULL,
	[Date_Of_Leaving_Last_Attended_School] [nvarchar](max) NULL,
	[Handicap] [nvarchar](max) NULL,
	[Candidate_Handicaped_YN] [nvarchar](max) NULL,
	[Subject_List] [nvarchar](max) NULL,
	[Subject1] [nvarchar](max) NULL,
	[Subject2] [nvarchar](max) NULL,
	[Subject3] [nvarchar](max) NULL,
	[Subject4] [nvarchar](max) NULL,
	[Subject5] [nvarchar](max) NULL,
	[Minority_Religion] [nvarchar](max) NULL,
	[Exam_Form_Disable] [nvarchar](max) NULL,
	[Type_Of_User] [nvarchar](max) NULL,
	[Nsqf_Subject] [nvarchar](max) NULL,
	[Hall_Ticket] [nvarchar](max) NULL,
	[Photo] [nvarchar](max) NULL,
	[Password] [nvarchar](50) NULL,
	[Signature] [nvarchar](max) NULL,
	[Payment_Status] [nvarchar](50) NULL,
	[Ec_Status] [nvarchar](50) NULL,
	[ip] [nvarchar](50) NULL,
	[DateNow] [nvarchar](max) NULL,
	[Year_Id] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Site_Status]    Script Date: 23-02-2023 11:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Site_Status](
	[ID] [int] NOT NULL,
	[Active_Status] [int] NULL,
	[Start_Date] [date] NULL,
	[Late_Date] [date] NULL,
	[Type] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_Site_Status] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Subject]    Script Date: 23-02-2023 11:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Subject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Subject_Code] [nvarchar](50) NULL,
	[Subject_Name] [nvarchar](max) NULL,
	[Subject_Group] [nvarchar](50) NULL,
	[Handicaped] [nvarchar](50) NULL,
	[Class] [nvarchar](50) NULL,
	[sample2] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tbl_Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Open_Schooling_Final_2023] SET  READ_WRITE 
GO

CREATE DATABASE [sep4-DWH]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'sep4-DWH', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\sep4-DWH.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'sep4-DWH_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\sep4-DWH_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO


ALTER DATABASE [sep4-DWH] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [sep4-DWH] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [sep4-DWH] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [sep4-DWH] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [sep4-DWH] SET ARITHABORT OFF 
GO

ALTER DATABASE [sep4-DWH] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [sep4-DWH] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [sep4-DWH] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [sep4-DWH] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [sep4-DWH] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [sep4-DWH] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [sep4-DWH] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [sep4-DWH] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [sep4-DWH] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [sep4-DWH] SET  DISABLE_BROKER 
GO

ALTER DATABASE [sep4-DWH] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [sep4-DWH] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [sep4-DWH] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [sep4-DWH] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [sep4-DWH] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [sep4-DWH] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [sep4-DWH] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [sep4-DWH] SET RECOVERY FULL 
GO

ALTER DATABASE [sep4-DWH] SET  MULTI_USER 
GO

ALTER DATABASE [sep4-DWH] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [sep4-DWH] SET DB_CHAINING OFF 
GO

ALTER DATABASE [sep4-DWH] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [sep4-DWH] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [sep4-DWH] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [sep4-DWH] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [sep4-DWH] SET QUERY_STORE = OFF
GO

ALTER DATABASE [sep4-DWH] SET  READ_WRITE 
GO


USE [sep4-DWH]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY') ALTER DATABASE [sep4-DWH] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO
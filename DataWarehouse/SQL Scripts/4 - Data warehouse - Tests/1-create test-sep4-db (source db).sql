USE [master]
GO

/****** Object:  Database [test-sep4-db]   ******/
CREATE DATABASE [test-sep4-db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'test-sep4-db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\test-sep4-db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'test-sep4-db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\test-sep4-db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [test-sep4-db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [test-sep4-db] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [test-sep4-db] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [test-sep4-db] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [test-sep4-db] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [test-sep4-db] SET ARITHABORT OFF 
GO

ALTER DATABASE [test-sep4-db] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [test-sep4-db] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [test-sep4-db] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [test-sep4-db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [test-sep4-db] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [test-sep4-db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [test-sep4-db] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [test-sep4-db] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [test-sep4-db] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [test-sep4-db] SET  DISABLE_BROKER 
GO

ALTER DATABASE [test-sep4-db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [test-sep4-db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [test-sep4-db] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [test-sep4-db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [test-sep4-db] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [test-sep4-db] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [test-sep4-db] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [test-sep4-db] SET RECOVERY FULL 
GO

ALTER DATABASE [test-sep4-db] SET  MULTI_USER 
GO

ALTER DATABASE [test-sep4-db] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [test-sep4-db] SET DB_CHAINING OFF 
GO

ALTER DATABASE [test-sep4-db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [test-sep4-db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [test-sep4-db] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [test-sep4-db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [test-sep4-db] SET QUERY_STORE = OFF
GO

ALTER DATABASE [test-sep4-db] SET  READ_WRITE 
GO



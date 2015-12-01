USE [master]
GO

CREATE DATABASE [Quiztomizador_Server]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Quiztomizador_Server', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Quiztomizador_Server.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Quiztomizador_Server_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Quiztomizador_Server_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Quiztomizador_Server].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Quiztomizador_Server] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET ARITHABORT OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [Quiztomizador_Server] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Quiztomizador_Server] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Quiztomizador_Server] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Quiztomizador_Server] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Quiztomizador_Server] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [Quiztomizador_Server] SET  MULTI_USER 
GO

ALTER DATABASE [Quiztomizador_Server] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Quiztomizador_Server] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Quiztomizador_Server] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Quiztomizador_Server] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [Quiztomizador_Server] SET  READ_WRITE 
GO


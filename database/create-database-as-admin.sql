USE [master];
GO

IF SUSER_ID(N'$(PortfolioUser)') IS NULL
BEGIN
    CREATE LOGIN [$(PortfolioUser)] FROM WINDOWS;
END
GO

IF DB_ID(N'BalpreetPortfolioDb') IS NULL
BEGIN
    CREATE DATABASE [BalpreetPortfolioDb];
END
GO

USE [BalpreetPortfolioDb];
GO

IF USER_ID(N'$(PortfolioUser)') IS NULL
BEGIN
    CREATE USER [$(PortfolioUser)] FOR LOGIN [$(PortfolioUser)];
END
GO

IF IS_ROLEMEMBER(N'db_owner', N'$(PortfolioUser)') = 0
BEGIN
    ALTER ROLE db_owner ADD MEMBER [$(PortfolioUser)];
END
GO

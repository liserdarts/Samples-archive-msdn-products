
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/13/2012 21:56:00
-- Generated from EDMX file: C:\MSDN Code Samples\5-Edit data in Windows Azure SQL databases from a provider hosted SharePoint application\EditDataInSQLAzureWeb\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [CustomerID] int IDENTITY(1,1) NOT NULL,
    [CompanyName] nvarchar(200)  NOT NULL,
    [ContactName] nvarchar(200)  NULL,
    [ContactTitle] nvarchar(200)  NULL,
    [Address] nvarchar(200)  NULL,
    [City] nvarchar(200)  NULL,
    [Region] nvarchar(200)  NULL,
    [PostalCode] nvarchar(200)  NULL,
    [Country] nvarchar(200)  NULL,
    [Phone] nvarchar(200)  NULL,
    [Fax] nvarchar(200)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CustomerID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([CustomerID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
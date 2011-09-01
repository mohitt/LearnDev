
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/20/2011 22:08:55
-- Generated from EDMX file: Y:\~Projects\Ads\Ads.DummyModel\Entities\AdsModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Ads2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AdCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ads] DROP CONSTRAINT [FK_AdCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_CountryState]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[States] DROP CONSTRAINT [FK_CountryState];
GO
IF OBJECT_ID(N'[dbo].[FK_StateCity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cities] DROP CONSTRAINT [FK_StateCity];
GO
IF OBJECT_ID(N'[dbo].[FK_UserAd]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ads] DROP CONSTRAINT [FK_UserAd];
GO
IF OBJECT_ID(N'[dbo].[FK_AddressCity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Addresses] DROP CONSTRAINT [FK_AddressCity];
GO
IF OBJECT_ID(N'[dbo].[FK_AdAdType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ads] DROP CONSTRAINT [FK_AdAdType];
GO
IF OBJECT_ID(N'[dbo].[FK_AdAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ads] DROP CONSTRAINT [FK_AdAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_CategorySubCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories] DROP CONSTRAINT [FK_CategorySubCategory];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Ads]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ads];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[States]', 'U') IS NOT NULL
    DROP TABLE [dbo].[States];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Keywords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Keywords];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO
IF OBJECT_ID(N'[dbo].[Cities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cities];
GO
IF OBJECT_ID(N'[dbo].[Aspects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Aspects];
GO
IF OBJECT_ID(N'[dbo].[Addresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Addresses];
GO
IF OBJECT_ID(N'[dbo].[AdTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AdTypes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Ads'
CREATE TABLE [dbo].[Ads] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CategoryId] int  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [AdTypeId] int  NOT NULL,
    [AddressId] int  NULL,
    [CreatedOn] datetime  NOT NULL,
    [CreatedBy] int  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [LastUpdatedBy] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DisplayName] nvarchar(20)  NOT NULL,
    [RealName] nvarchar(40)  NOT NULL,
    [Email] nvarchar(40)  NOT NULL,
    [OpenId] nvarchar(max)  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [CreatedBy] int  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [LastUpdatedBy] int  NOT NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Abbriviation] nvarchar(max)  NOT NULL,
    [CountryId] int  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AttDefXML] nvarchar(max)  NULL,
    [DisplayValue] nvarchar(max)  NOT NULL,
    [ParentCategoryId] int  NULL,
    [CreatedOn] datetime  NOT NULL,
    [CreatedBy] int  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [LastUpdatedBy] int  NOT NULL
);
GO

-- Creating table 'Keywords'
CREATE TABLE [dbo].[Keywords] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Cities'
CREATE TABLE [dbo].[Cities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [StateId] int  NOT NULL
);
GO

-- Creating table 'Aspects'
CREATE TABLE [dbo].[Aspects] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Property] nvarchar(max)  NOT NULL,
    [City_Id] int  NOT NULL
);
GO

-- Creating table 'AdTypes'
CREATE TABLE [dbo].[AdTypes] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Ads'
ALTER TABLE [dbo].[Ads]
ADD CONSTRAINT [PK_Ads]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Keywords'
ALTER TABLE [dbo].[Keywords]
ADD CONSTRAINT [PK_Keywords]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [PK_Cities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Aspects'
ALTER TABLE [dbo].[Aspects]
ADD CONSTRAINT [PK_Aspects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [PK_Addresses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AdTypes'
ALTER TABLE [dbo].[AdTypes]
ADD CONSTRAINT [PK_AdTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryId] in table 'Ads'
ALTER TABLE [dbo].[Ads]
ADD CONSTRAINT [FK_AdCategory]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AdCategory'
CREATE INDEX [IX_FK_AdCategory]
ON [dbo].[Ads]
    ([CategoryId]);
GO

-- Creating foreign key on [CountryId] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [FK_CountryState]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryState'
CREATE INDEX [IX_FK_CountryState]
ON [dbo].[States]
    ([CountryId]);
GO

-- Creating foreign key on [StateId] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [FK_StateCity]
    FOREIGN KEY ([StateId])
    REFERENCES [dbo].[States]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StateCity'
CREATE INDEX [IX_FK_StateCity]
ON [dbo].[Cities]
    ([StateId]);
GO

-- Creating foreign key on [UserId] in table 'Ads'
ALTER TABLE [dbo].[Ads]
ADD CONSTRAINT [FK_UserAd]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAd'
CREATE INDEX [IX_FK_UserAd]
ON [dbo].[Ads]
    ([UserId]);
GO

-- Creating foreign key on [City_Id] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [FK_AddressCity]
    FOREIGN KEY ([City_Id])
    REFERENCES [dbo].[Cities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AddressCity'
CREATE INDEX [IX_FK_AddressCity]
ON [dbo].[Addresses]
    ([City_Id]);
GO

-- Creating foreign key on [AdTypeId] in table 'Ads'
ALTER TABLE [dbo].[Ads]
ADD CONSTRAINT [FK_AdAdType]
    FOREIGN KEY ([AdTypeId])
    REFERENCES [dbo].[AdTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AdAdType'
CREATE INDEX [IX_FK_AdAdType]
ON [dbo].[Ads]
    ([AdTypeId]);
GO

-- Creating foreign key on [AddressId] in table 'Ads'
ALTER TABLE [dbo].[Ads]
ADD CONSTRAINT [FK_AdAddress]
    FOREIGN KEY ([AddressId])
    REFERENCES [dbo].[Addresses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AdAddress'
CREATE INDEX [IX_FK_AdAddress]
ON [dbo].[Ads]
    ([AddressId]);
GO

-- Creating foreign key on [ParentCategoryId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [FK_CategorySubCategory]
    FOREIGN KEY ([ParentCategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategorySubCategory'
CREATE INDEX [IX_FK_CategorySubCategory]
ON [dbo].[Categories]
    ([ParentCategoryId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
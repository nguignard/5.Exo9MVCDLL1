
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/12/2017 08:53:51
-- Generated from EDMX file: C:\Users\DL-CDI\Dropbox\AFPA2\74.EntityFrameWork couchepersistance\5.Exo9MVCDLL1\ClassesDAO\Formation.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DbAbi];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SectionsStagiaire]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StagiaireSet] DROP CONSTRAINT [FK_SectionsStagiaire];
GO
IF OBJECT_ID(N'[dbo].[FK_StagiaireDE_inherits_Stagiaire]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StagiaireSet_StagiaireDE] DROP CONSTRAINT [FK_StagiaireDE_inherits_Stagiaire];
GO
IF OBJECT_ID(N'[dbo].[FK_StagiaireCIF_inherits_Stagiaire]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StagiaireSet_StagiaireCIF] DROP CONSTRAINT [FK_StagiaireCIF_inherits_Stagiaire];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[StagiaireSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StagiaireSet];
GO
IF OBJECT_ID(N'[dbo].[SectionsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SectionsSet];
GO
IF OBJECT_ID(N'[dbo].[StagiaireSet_StagiaireDE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StagiaireSet_StagiaireDE];
GO
IF OBJECT_ID(N'[dbo].[StagiaireSet_StagiaireCIF]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StagiaireSet_StagiaireCIF];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'StagiaireSet'
CREATE TABLE [dbo].[StagiaireSet] (
    [NumOsia] int  NOT NULL,
    [NomStagiaire] nvarchar(20)  NOT NULL,
    [PrenomStagiaire] nvarchar(20)  NOT NULL,
    [rueStagiaire] nvarchar(20)  NOT NULL,
    [VilleStagiaire] nvarchar(20)  NOT NULL,
    [CodePostalStagiaire] nchar(5)  NOT NULL,
    [NbreNotes] int  NULL,
    [pointsNote] float  NULL,
    [Sections_IdSection] int  NOT NULL
);
GO

-- Creating table 'SectionsSet'
CREATE TABLE [dbo].[SectionsSet] (
    [IdSection] int  NOT NULL,
    [NomSection] nvarchar(20)  NOT NULL,
    [DateDebut] datetime  NOT NULL,
    [DateFin] datetime  NOT NULL
);
GO

-- Creating table 'StagiaireSet_StagiaireCIF'
CREATE TABLE [dbo].[StagiaireSet_StagiaireCIF] (
    [Fongecif] nvarchar(max)  NOT NULL,
    [TypeCIF] nvarchar(max)  NOT NULL,
    [NumOsia] int  NOT NULL
);
GO

-- Creating table 'StagiaireSet_StagiaireDE'
CREATE TABLE [dbo].[StagiaireSet_StagiaireDE] (
    [RemuAfpa] bit  NOT NULL,
    [NumOsia] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [NumOsia] in table 'StagiaireSet'
ALTER TABLE [dbo].[StagiaireSet]
ADD CONSTRAINT [PK_StagiaireSet]
    PRIMARY KEY CLUSTERED ([NumOsia] ASC);
GO

-- Creating primary key on [IdSection] in table 'SectionsSet'
ALTER TABLE [dbo].[SectionsSet]
ADD CONSTRAINT [PK_SectionsSet]
    PRIMARY KEY CLUSTERED ([IdSection] ASC);
GO

-- Creating primary key on [NumOsia] in table 'StagiaireSet_StagiaireCIF'
ALTER TABLE [dbo].[StagiaireSet_StagiaireCIF]
ADD CONSTRAINT [PK_StagiaireSet_StagiaireCIF]
    PRIMARY KEY CLUSTERED ([NumOsia] ASC);
GO

-- Creating primary key on [NumOsia] in table 'StagiaireSet_StagiaireDE'
ALTER TABLE [dbo].[StagiaireSet_StagiaireDE]
ADD CONSTRAINT [PK_StagiaireSet_StagiaireDE]
    PRIMARY KEY CLUSTERED ([NumOsia] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Sections_IdSection] in table 'StagiaireSet'
ALTER TABLE [dbo].[StagiaireSet]
ADD CONSTRAINT [FK_SectionsStagiaire]
    FOREIGN KEY ([Sections_IdSection])
    REFERENCES [dbo].[SectionsSet]
        ([IdSection])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SectionsStagiaire'
CREATE INDEX [IX_FK_SectionsStagiaire]
ON [dbo].[StagiaireSet]
    ([Sections_IdSection]);
GO

-- Creating foreign key on [NumOsia] in table 'StagiaireSet_StagiaireCIF'
ALTER TABLE [dbo].[StagiaireSet_StagiaireCIF]
ADD CONSTRAINT [FK_StagiaireCIF_inherits_Stagiaire]
    FOREIGN KEY ([NumOsia])
    REFERENCES [dbo].[StagiaireSet]
        ([NumOsia])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [NumOsia] in table 'StagiaireSet_StagiaireDE'
ALTER TABLE [dbo].[StagiaireSet_StagiaireDE]
ADD CONSTRAINT [FK_StagiaireDE_inherits_Stagiaire]
    FOREIGN KEY ([NumOsia])
    REFERENCES [dbo].[StagiaireSet]
        ([NumOsia])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
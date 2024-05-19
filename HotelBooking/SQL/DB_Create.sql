CREATE TABLE [dbo].[TaxMaster]
(
	[TaxId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Value] FLOAT NULL, 
    [BranchId] INT NULL, 
    [isActive] BIT NULL, 
    [isDeleted] BIT NULL
)
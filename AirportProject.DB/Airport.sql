﻿CREATE TABLE [dbo].[Airport]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [name] NVARCHAR(50) NOT NULL UNIQUE, 
    [country] NVARCHAR(50) NOT NULL, 
    [city] NVARCHAR(50) NOT NULL,

)
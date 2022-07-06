CREATE TABLE [dbo].[Passenger]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [firstname] NVARCHAR(50) NOT NULL, 
    [lastname] NVARCHAR(50) NOT NULL, 
    [passport] CHAR(8) NOT NULL UNIQUE, 
    [nationality] NVARCHAR(50) NOT NULL, 
    [birthday] DATETIME NOT NULL, 
    [gender] NVARCHAR(6) NOT NULL,
    CONSTRAINT [CK_Passenger_Gender] CHECK ([gender] in ('male', 'female')),
)

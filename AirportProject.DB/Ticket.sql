CREATE TABLE [dbo].[Ticket]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [price] MONEY NOT NULL, 
    [type] NVARCHAR(8) NOT NULL, 
    [flight_id] INT NOT NULL, 
    CONSTRAINT [FK_Ticket_Flight] FOREIGN KEY ([flight_id]) REFERENCES [Flight]([id]) ON DELETE CASCADE, 
    CONSTRAINT [CK_Ticket_Type] CHECK ([type] in ('economy', 'business')),
)

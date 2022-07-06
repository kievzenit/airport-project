CREATE TABLE [dbo].[Flight]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [arrival_airport_id] INT NOT NULL, 
    [departure_airport_id] INT NOT NULL, 
    [terminal] CHAR(1) NOT NULL, 
    [arrival_time] DATETIME NOT NULL, 
    [departure_time] DATETIME NOT NULL, 
    [status] NVARCHAR(8) NOT NULL, 
    CONSTRAINT [FK_Flight_ArrivalAirport] 
        FOREIGN KEY ([arrival_airport_id]) REFERENCES [Airport]([id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Flight_DepartureAirport]
        FOREIGN KEY ([departure_airport_id]) REFERENCES [Airport]([id]) ON DELETE NO ACTION,
    CONSTRAINT [CK_Flight_Terminal] CHECK ([terminal] like '[A-Z]'), 
    CONSTRAINT [CK_Flight_Status] CHECK ([status] in ('normal', 'delayed', 'canceled')) 
)

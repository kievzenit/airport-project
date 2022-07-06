CREATE TABLE [dbo].[PassengersTickets]
(
	[passenger_id] INT NOT NULL , 
    [ticket_id] INT NOT NULL, 
    CONSTRAINT [FK_PassengersTickets_Passenger] FOREIGN KEY (passenger_id) REFERENCES [Passenger]([id]) ON DELETE NO ACTION, 
    CONSTRAINT [FK_PassengersTickets_Ticket] FOREIGN KEY (ticket_id) REFERENCES [Ticket]([id]) ON DELETE NO ACTION,
    PRIMARY KEY(passenger_id, ticket_id)
)

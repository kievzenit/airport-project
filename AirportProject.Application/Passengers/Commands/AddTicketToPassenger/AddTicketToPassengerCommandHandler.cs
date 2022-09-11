using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Passengers.Commands.AddTicketToPassenger
{
    public class AddTicketToPassengerCommandHandler : IRequestHandler<AddTicketToPassengerCommand>
    {
        private readonly IPassengerRepository repository;

        public AddTicketToPassengerCommandHandler(IPassengerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(
            AddTicketToPassengerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var success = await this.repository.AddTicket(request, cancellationToken);

            if (!success)
            {
                throw new NotFoundException($"Ticket with id: {request.TicketId} or passenger with id: {request.PassengerId} was not found or passenger is already have this ticket");
            }

            return Unit.Value;
        }
    }
}

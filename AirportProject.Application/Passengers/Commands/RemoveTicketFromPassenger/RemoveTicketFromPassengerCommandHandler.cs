using AirportProject.Application.Abstract;
using AirportProject.Application.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Passengers.Commands.RemoveTicketFromPassenger
{
    public class RemoveTicketFromPassengerCommandHandler : IRequestHandler<RemoveTicketFromPassengerCommand>
    {
        private readonly IPassengerRepository repository;

        public RemoveTicketFromPassengerCommandHandler(IPassengerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(
            RemoveTicketFromPassengerCommand request, CancellationToken cancellationToken)
        {
            if (request.TicketId <= 0 && request.PassengerId <= 0)
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var success = await this.repository.DeleteTicket(request.PassengerId, request.TicketId);

            if (!success)
            {
                throw new NotFoundException($"Ticket with id: {request.TicketId} or passenger with id: {request.PassengerId} was not found");
            }

            return Unit.Value;
        }
    }
}

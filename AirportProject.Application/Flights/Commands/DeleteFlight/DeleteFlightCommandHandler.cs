using AirportProject.Application.Abstract;
using AirportProject.Application.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Flights.Commands.DeleteFlight
{
    public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand>
    {
        private readonly IFlightRepository repository;

        public DeleteFlightCommandHandler(IFlightRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Flight id must be not equal or less than zero");
            }

            var success = await this.repository.Delete(request, cancellationToken);

            if (!success)
            {
                throw new NotFoundException($"Flight with id: {request.Id} not found");
            }

            return Unit.Value;
        }
    }
}

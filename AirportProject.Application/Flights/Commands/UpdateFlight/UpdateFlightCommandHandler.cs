using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Flights.Commands.UpdateFlight
{
    public class UpdateFlightCommandHandler : IRequestHandler<UpdateFlightCommand>
    {
        private readonly IFlightRepository repository;

        public UpdateFlightCommandHandler(IFlightRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var success = await this.repository.Update(request, cancellationToken);

            if (!success)
            {
                throw new NotFoundException($"Flight with id: {request.Id} was not found");
            }

            return Unit.Value;
        }
    }
}

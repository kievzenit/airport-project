using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Passengers.Commands.UpdatePassenger
{
    public class UpdatePassengerCommandHandler : IRequestHandler<UpdatePassengerCommand>
    {
        private readonly IPassengerRepository repository;

        public UpdatePassengerCommandHandler(IPassengerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(UpdatePassengerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var success = await this.repository.Update(request, cancellationToken);

            if (!success)
            {
                throw new NotFoundException($"Passenger with id: {request.Id} was not found");
            }

            return Unit.Value;
        }
    }
}

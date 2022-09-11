using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Passengers.Commands.DeletePassenger
{
    public class DeletePassengerCommandHandler : IRequestHandler<DeletePassengerCommand>
    {
        private readonly IPassengerRepository repository;

        public DeletePassengerCommandHandler(IPassengerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(DeletePassengerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Id must be not less than or equal to zero");
            }

            var success = await this.repository.Delete(request, cancellationToken);

            if (!success)
            {
                throw new NotFoundException($"Passenger with id: {request.Id} not found");
            }

            return Unit.Value;
        }
    }
}

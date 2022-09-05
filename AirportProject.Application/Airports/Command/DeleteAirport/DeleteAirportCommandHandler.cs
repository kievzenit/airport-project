using AirportProject.Application.Abstract;
using AirportProject.Application.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Airports.Commands.DeleteAirport
{
    public class DeleteAirportCommandHandler : IRequestHandler<DeleteAirportCommand>
    {
        private readonly IAirportRepository repository;

        public DeleteAirportCommandHandler(IAirportRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(DeleteAirportCommand request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                throw new ArgumentException("Id cannot be less or equal to zero");
            }

            var success = await repository.Delete(request.Id);

            if (!success)
            {
                throw new NotFoundException("Airport with id: {request.Id} not found");
            }

            return Unit.Value;
        }
    }
}

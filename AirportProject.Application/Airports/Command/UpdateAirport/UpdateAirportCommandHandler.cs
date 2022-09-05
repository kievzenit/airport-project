using AirportProject.Application.Abstract;
using AirportProject.Application.Exceptions;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.DTOs.Validation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Airports.Commands.UpdateAirport
{
    public class UpdateAirportCommandHandler : IRequestHandler<UpdateAirportCommand>
    {
        private readonly IAirportRepository repository;

        public UpdateAirportCommandHandler(IAirportRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(
            UpdateAirportCommand request, 
            CancellationToken cancellationToken)
        {
            var airportDTO = new AirportDTO
            {
                Id = request.Id,
                Name = request.Name,
                Country = request.Country,
                City = request.City
            };

            if (airportDTO.Id <= 0 || !airportDTO.IsValid())
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var success = await repository.Update(airportDTO);

            if (!success)
            {
                throw new NotFoundException($"Airport with id: {request.Id} not found");
            }

            return Unit.Value;
        }
    }
}

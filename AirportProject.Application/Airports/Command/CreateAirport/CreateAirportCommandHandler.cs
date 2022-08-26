using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.DTOs.Validation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Airports.Commands.CreateAirport
{
    public class CreateAirportCommandHandler : IRequestHandler<CreateAirportCommand, AirportDTO>
    {
        private readonly IAirportRepository repository;

        public CreateAirportCommandHandler(IAirportRepository repository)
        {
            this.repository = repository;
        }


        public async Task<AirportDTO> Handle(
            CreateAirportCommand request,
            CancellationToken cancellationToken)
        {
            var airportDTO = new AirportDTO()
            {
                Name = request.Name,
                Country = request.Country,
                City = request.City
            };

            if (!airportDTO.IsValid())
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            return await repository.Create(airportDTO);
        }
    }
}

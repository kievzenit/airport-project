using AirportProject.Application.Common.Abstract;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.DTOs.Validation;
using AirportProject.Domain.Models;
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
            if (!await request.IsValid(repository, cancellationToken))
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var airport = await repository.Create(request, cancellationToken);

            return new AirportDTO
            {
                Id = airport.Id,
                Name = airport.Name,
                Country = airport.Country,
                City = airport.City
            };
        }
    }
}

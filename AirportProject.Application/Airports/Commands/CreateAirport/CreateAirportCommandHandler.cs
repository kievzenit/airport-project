using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
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
        private readonly ICaster<Airport, AirportDTO> caster;

        public CreateAirportCommandHandler(IAirportRepository repository, ICaster<Airport, AirportDTO> caster)
        {
            this.repository = repository;
            this.caster = caster;
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

            return await this.caster.Cast(airport, cancellationToken);
        }
    }
}

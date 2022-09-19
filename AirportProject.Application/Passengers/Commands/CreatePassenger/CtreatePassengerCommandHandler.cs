using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Passengers.Commands.CreatePassenger
{
    public class CreatePassengerCommandHandler : IRequestHandler<CreatePassengerCommand, PassengerDTO>
    {
        private readonly IPassengerRepository repository;
        private readonly ICaster<Passenger, PassengerDTO> caster;

        public CreatePassengerCommandHandler(
            IPassengerRepository repository, ICaster<Passenger, PassengerDTO> caster)
        {
            this.repository = repository;
            this.caster = caster;
        }

        public async Task<PassengerDTO> Handle(
            CreatePassengerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var passenger = await this.repository.Create(request, cancellationToken);

            return await this.caster.Cast(passenger, cancellationToken);
        }
    }
}

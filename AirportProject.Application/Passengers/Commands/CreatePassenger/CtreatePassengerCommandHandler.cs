using AirportProject.Application.Abstract;
using AirportProject.Application.Casting;
using AirportProject.Domain.DTOs;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Passengers.Commands.CreatePassenger
{
    public class CtreatePassengerCommandHandler : IRequestHandler<CreatePassengerCommand, PassengerDTO>
    {
        private readonly IPassengerRepository repository;
        private readonly PassengersCaster caster;

        public CtreatePassengerCommandHandler(IPassengerRepository repository, PassengersCaster caster)
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

            return await this.caster.Cast(passenger);
        }
    }
}

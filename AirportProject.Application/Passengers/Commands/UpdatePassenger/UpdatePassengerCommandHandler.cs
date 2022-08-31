using AirportProject.Application.Abstract;
using AirportProject.Application.Exceptions;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.DTOs.Validation;
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
            var passengerDTO = new PassengerDTO
            {
                Id = request.Id,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Passport = request.Passport,
                Nationality = request.Nationality
            };

            if (passengerDTO.Id <= 0 || !passengerDTO.IsValid(false))
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var success = await this.repository.Update(passengerDTO);

            if (!success)
            {
                throw new NotFoundException($"Passenger with id: {request.Id} was not found");
            }

            return Unit.Value;
        }
    }
}

using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.DTOs.Validation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Passengers.Commands.CreatePassenger
{
    public class CtreatePassengerCommandHandler : IRequestHandler<CreatePassengerCommand, PassengerDTO>
    {
        private readonly IPassengerRepository repository;

        public CtreatePassengerCommandHandler(IPassengerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PassengerDTO> Handle(
            CreatePassengerCommand request, CancellationToken cancellationToken)
        {
            var passengerDTO = new PassengerDTO
            {
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Passport = request.Passport,
                Nationality = request.Nationality,
                Birthday = request.Birthday,
                Gender = request.Gender
            };

            if (!passengerDTO.IsValid())
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            return await this.repository.Create(passengerDTO);
        }
    }
}

﻿using AirportProject.Application.Abstract;
using AirportProject.Application.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Passengers.Commands.AddTicketToPassenger
{
    public class AddTicketToPassengerCommandHandler : IRequestHandler<AddTicketToPassengerCommand>
    {
        private readonly IPassengerRepository repository;

        public AddTicketToPassengerCommandHandler(IPassengerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(
            AddTicketToPassengerCommand request, CancellationToken cancellationToken)
        {
            if (request.TicketId <= 0 && request.PassengerId <= 0)
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var success = await this.repository.AddTicket(request.PassengerId, request.TicketId);

            if (!success)
            {
                throw new NotFoundException($"Ticket with id: {request.TicketId} or passenger with id: {request.PassengerId} was not found");
            }

            return Unit.Value;
        }
    }
}
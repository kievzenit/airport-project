using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Passengers.Queries.GetPassengersByLastname
{
    public class GetPassengersByLastnameQueryHandler :
        IRequestHandler<GetPassengersByLastnameQuery, IEnumerable<PassengerDTO>>
    {
        private readonly IPassengerRepository repository;

        public GetPassengersByLastnameQueryHandler(IPassengerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<PassengerDTO>> Handle(
            GetPassengersByLastnameQuery request, CancellationToken cancellationToken)
        {
            if (request.Lastname == null
                || request.Lastname.Length > 50
                || request.Lastname.Length == 0)
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            return await this.repository.SearchByLastname(request.Lastname);
        }
    }
}

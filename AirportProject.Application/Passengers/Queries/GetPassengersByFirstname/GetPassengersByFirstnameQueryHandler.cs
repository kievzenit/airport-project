using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Passengers.Queries.GetPassengersByFirstname
{
    public class GetPassengersByFirstnameQueryHandler :
        IRequestHandler<GetPassengersByFirstnameQuery, IEnumerable<PassengerDTO>>
    {
        private readonly IPassengerRepository repository;

        public GetPassengersByFirstnameQueryHandler(IPassengerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<PassengerDTO>> Handle(
            GetPassengersByFirstnameQuery request, CancellationToken cancellationToken)
        {
            if (request.Firstname == null
                || request.Firstname.Length > 50
                || request.Firstname.Length == 0)
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            return await this.repository.SearchByFirstname(request.Firstname);
        }
    }
}

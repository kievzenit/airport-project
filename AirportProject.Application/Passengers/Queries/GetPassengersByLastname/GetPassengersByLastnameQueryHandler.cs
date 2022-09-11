﻿using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.Casting;
using AirportProject.Application.Common.DTOs;
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
        private readonly PassengersCaster caster;

        public GetPassengersByLastnameQueryHandler(IPassengerRepository repository, PassengersCaster caster)
        {
            this.repository = repository;
            this.caster = caster;
        }

        public async Task<IEnumerable<PassengerDTO>> Handle(
            GetPassengersByLastnameQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var passengers = await this.repository.SearchByLastname(request, cancellationToken);

            return await this.caster.Cast(passengers);
        }
    }
}

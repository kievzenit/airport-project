using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirportProject.DTOs.Validation
{
    public static class DTOValidation
    {
        private static bool isValid<T>(T t, bool validateAllProperties = false)
            where T: class
        {
            var results = default(ICollection<ValidationResult>);

            return Validator.TryValidateObject(t, new ValidationContext(t), results, validateAllProperties);
        }
        public static bool IsValid(this AirportDTO airportDTO)
        {
            return DTOValidation.isValid<AirportDTO>(airportDTO);
        }

        public static bool IsValid(this FlightDTO flightDTO)
        {
            var validationSuccess = DTOValidation.isValid<FlightDTO>(flightDTO);

            return validationSuccess 
                && flightDTO.ArrivalTime > flightDTO.DepartureTime 
                && flightDTO.ArrivalAirportName != flightDTO.DepartureAirportName;
        }

        public static bool IsValid(this TicketDTO ticketDTO)
        {
            return DTOValidation.isValid<TicketDTO>(ticketDTO);
        }

        public static bool IsValid(this PassengerDTO passengerDTO, bool validateAllProperties = true)
        {
            return DTOValidation.isValid<PassengerDTO>(passengerDTO, validateAllProperties);
        }
    }
}

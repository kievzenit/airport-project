using System;
using System.Text.RegularExpressions;

namespace AirportProject.Application.Flights.Commands.UpdateFlight
{
    public static class UpdateFlightCommandValidator
    {
        public static bool IsValid(this UpdateFlightCommand command)
        {
            return IsIdValid(command.Id)
                && IsArrivalAirportNameValid(command.ArrivalAirportName)
                && IsTimeValid(command.ArrivalTime, command.DepartureTime)
                && IsStatusValid(command.Status)
                && IsTerminalValid(command.Terminal)
                && IsPricesValid(command.EconomyPrice, command.BusinessPrice);
        }

        private static bool IsIdValid(int id)
        {
            return id > 0;
        }

        private static bool IsArrivalAirportNameValid(string arrivalAirportName)
        {
            return arrivalAirportName.Length > 0 && arrivalAirportName.Length <= 50;
        }

        private static bool IsTimeValid(DateTime arrivalTime, DateTime departureTime)
        {
            return arrivalTime > departureTime;
        }

        private static bool IsStatusValid(string status)
        {
            var regex = new Regex("^normal$|^delayed$|^canceled$");

            return regex.IsMatch(status);
        }

        private static bool IsTerminalValid(char terminal)
        {
            var regex = new Regex("[A-Z]");

            return regex.IsMatch(terminal.ToString());
        }

        private static bool IsPricesValid(decimal economyPrice, decimal businessPrice)
        {
            return economyPrice >= 100 && economyPrice <= 100000
                && businessPrice >= 100 && businessPrice <= 100000;
        }
    }
}

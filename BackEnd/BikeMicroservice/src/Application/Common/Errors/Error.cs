using Domain.DTOs;

namespace Application.Common.Errors
{
    public class Error
    {
        public static string BikeNotFound { get; set; } = "No Bike with provided Id has been found.";
        public static string HomeBaseNotFound { get; set; } = "No HomeBase with provided Id has been found.";
        public static string HomeBaseFull { get; set; } = "HomeBase with provided Id is full.";
        public static string ErrorWhileProcessingOperation { get; set; } = "Error while processing operation. Try again later.";
    }
}
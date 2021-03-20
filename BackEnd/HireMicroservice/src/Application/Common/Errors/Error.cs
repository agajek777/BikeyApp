namespace Application.Common.Errors
{
    public static class Error
    {
        public static string HireNotFound { get; set; } = "The Hire with provided Id has not been found.";

        public static string BikeNotAvailable { get; set; } = "The bike with provided Id is currently not available for hire.";

        public static string ClientFullHires { get; set; } = "Client with provided Id has currently maximal number of hires.";
        public static string BikeNotExists { get; set; } = "No bike with provided Id has been found.";
        public static string ClientNotExists { get; set; } = "No client with provided Id has been found.";
        public static string CannotModifyHire { get; set; } = "You do not have permission to modify BikeId nor ClientId.";
    }
}
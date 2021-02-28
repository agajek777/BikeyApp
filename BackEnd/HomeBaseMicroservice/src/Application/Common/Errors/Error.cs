namespace Application.Common.Errors
{
    public static class Error
    {
        public static string InvalidHomeBaseName { get; set; } = "Invalid HomeBase name. Name must be unique.";
        public static string HomeBaseNotFound { get; set; } = "No HomeBase with provided Id has been found.";

        public static string ErrorWhileProcessingOperation { get; set; } =
            "Error while processing operation. Try again later.";
    }
}
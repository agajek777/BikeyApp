namespace Application.Common.Errors
{
    public static class Error
    {
        public static string UserNotFound { get; set; } = "No user with provided username has been found.";
        public static string InvalidUsernameOrPassword { get; set; } = "Invalid username or password.";
    }
}
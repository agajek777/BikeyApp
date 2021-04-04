namespace Domain.DTOs
{
    public class UserWithTokenDto
    {
        public string Id { get; set; }
        
        public string UserName { get; set; }

        public string AccessToken { get; set; }
    }
}
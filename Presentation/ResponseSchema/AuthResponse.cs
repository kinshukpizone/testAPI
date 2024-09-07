namespace Presentation.ResponseSchema
{
    public class AuthResponse : SuccessAuthResponse
    {
        public List<string>? Errors { get; set; }
    }

    public class FailedAuthResponse : ErrorResponse
    {
        public bool IsAuthenticate { get; set; }
    }

    public class SuccessAuthResponse : Response
    {
        public string? Token { get; set; }
        public string? Role { get; set; }
        public DateTime? LoginTime { get; set; }
        public bool IsAuthenticate { get; set; }
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}

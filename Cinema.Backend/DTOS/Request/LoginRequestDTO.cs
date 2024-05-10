namespace Cinema.Backend.DTOS.Request
{
    public class LoginRequestDTO
    {
        public string Message { get; init; }
        public bool IsSuccess { get; init; }

        public string Token { get; init; }
    }
}

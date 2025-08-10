namespace Ecommerce.Application.Dto
{
    public class RegisterResultDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public Dictionary<string,string>? Errors { get; set; }
    }
}

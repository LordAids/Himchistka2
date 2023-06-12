namespace Himchistka.Api.DTO
{
    public class RegisterViewModel
    {

        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public List<Guid>? Places { get; set; }
    }
}

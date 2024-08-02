namespace LicenseServerApp.Models
{
    public class Organization 
    {
        public int Id { get; set; }
        public required string Inn { get; set; }
        public string Kpp { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}

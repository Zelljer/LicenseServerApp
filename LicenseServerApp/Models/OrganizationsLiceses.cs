namespace LicenseServerApp.Models
{
    public class OrganizationsLiceses
    {
        public Organization Organization { get; set; }
        public List<LicenseAPI.LicenseResponse> Licenses { get; set; } = new List<LicenseAPI.LicenseResponse>();
    }
}

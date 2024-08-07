namespace LicenseServerApp.Models.View
{
    public class OrganizationView
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationInn { get; set; }
        public string? OrganizationKpp { get; set; }
        public string OrganizationEmail { get; set; }
        public string OrganizationPhone { get; set; }
        public int LicenceId { get; set; }
        public string ProgramName { get; set; }
        public string TarifName { get; set; }
        public string DateCreated { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}

namespace LicenseServerApp.Models.View
{
    public class LicenseView
    {
        public int LicenceId { get; set; }
        public string OrganizationName { get; set; }
        public string ProgramName { get; set; } 
        public string TarifName { get; set; } 
        public string DateCreated { get; set; } 
        public string StartDate { get; set; } 
        public string EndDate { get; set; } 
    }
}

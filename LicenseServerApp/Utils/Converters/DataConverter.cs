using LicenseServerApp.Models.API;
using LicenseServerApp.Models.API.Dependencies;
using LicenseServerApp.Models.View;
using System.ComponentModel;

namespace LicenseServerApp.Utils.Converters
{
    public static class DataConverter
    {
        public static IEnumerable<LicenseView> ToLicenseView(IEnumerable<LicenseAPI.LicenseResponse> data, List<TarifAPI.TarifResponse> tarifs, List<OrganizationAPI.OrganizationResponse> organizations)
        {
            if (data == null)
                return Enumerable.Empty<LicenseView>();

            var result = data.Select(license => new LicenseView
            {
                LicenceId = license.Id,
                OrganizationName = organizations.Find(t => t.Id == license.OrganizationId)?.Name,
                ProgramName = tarifs.Find(t => t.Id == license.TarifId)?.Program.ToString(),
                TarifName = tarifs.Find(t => t.Id == license.TarifId)?.Name,
                DateCreated = license?.DateCreated.ToString("yyyy-MM-dd") ?? string.Empty,
                StartDate = license?.StartDate.ToString("yyyy-MM-dd") ?? string.Empty,
                EndDate = license?.EndDate.ToString("yyyy-MM-dd") ?? string.Empty
            });

            return result;
        }

        public static IEnumerable<OrganizationView> ToOrganizationView(IEnumerable<OrganizationsLiceses> data, List<TarifAPI.TarifResponse> tarifs)
        {
            if (data == null)
                return Enumerable.Empty<OrganizationView>();

            var result = data.SelectMany(orgLicense =>
            {
                if (orgLicense == null)
                    return Enumerable.Empty<OrganizationView>();

                if (orgLicense.Licenses == null || !orgLicense.Licenses.Any())
                {
                    return new List<OrganizationView>
                    {
                        new OrganizationView
                        {
                            OrganizationId = orgLicense.Organization?.Id ?? 0,
                            OrganizationName = orgLicense.Organization?.Name ?? string.Empty,
                            OrganizationInn = orgLicense.Organization?.Inn ?? string.Empty,
                            OrganizationKpp = orgLicense.Organization?.Kpp ?? string.Empty,
                            OrganizationEmail = orgLicense.Organization?.Email ?? string.Empty,
                            OrganizationPhone = orgLicense.Organization?.Phone ?? string.Empty,
                            LicenceId = 0,
                            ProgramName = string.Empty, 
                            TarifName = string.Empty, 
                            DateCreated = string.Empty, 
                            StartDate = string.Empty, 
                            EndDate = string.Empty 
                        }
                    };
                }


                return orgLicense.Licenses.Select(license => new OrganizationView
                    {
                        OrganizationId = orgLicense.Organization?.Id ?? 0,
                        OrganizationName = orgLicense.Organization?.Name ?? string.Empty,
                        OrganizationInn = orgLicense.Organization?.Inn ?? string.Empty,
                        OrganizationKpp = orgLicense.Organization?.Kpp ?? string.Empty,
                        OrganizationEmail = orgLicense.Organization?.Email ?? string.Empty,
                        OrganizationPhone = orgLicense.Organization?.Phone ?? string.Empty,
                        LicenceId = license?.Id ?? 0, 
                        ProgramName = license != null ? tarifs.Find(t => t.Id == license.TarifId)?.Program.ToString() ?? string.Empty : string.Empty,
                        TarifName = license != null ? tarifs.Find(t => t.Id == license.TarifId)?.Name ?? string.Empty : string.Empty,
                        DateCreated = license?.DateCreated.ToString("yyyy-MM-dd") ?? string.Empty,
                        StartDate = license?.StartDate.ToString("yyyy-MM-dd") ?? string.Empty,
                        EndDate = license?.EndDate.ToString("yyyy-MM-dd") ?? string.Empty
                    });
                });

            return result;
        }
    }
}


using LicenseServerApp.Models.API;
using LicenseServerApp.Models.API.Dependencies;
using LicenseServerApp.Models.View;

namespace LicenseServerApp.Utils.Converters
{
    public static class DataConverter
    {
        public static IEnumerable<LicenseView> ToLicenseView(IEnumerable<LicenseAPI.LicenseResponse> data, List<TarifAPI.TarifResponse> tarifs, OrganizationAPI.OrganizationResponse organization)
        {
            if (data == null)
                return [];

            try
            {
                return data.Select(license => new LicenseView
                {
                    LicenceId = license.Id,
                    OrganizationName = organization.Name,
                    ProgramName = tarifs.First(t => t.Id == license.TarifId).Program.ToString(),
                    TarifName = tarifs.First(t => t.Id == license.TarifId).Name,
                    DateCreated = license.DateCreated.ToShortDateString(),
                    StartDate = license.StartDate.ToShortDateString(),
                    EndDate = license.EndDate.ToShortDateString()
                });
            }
            catch 
            {
                return [];
            }
        }

        public static IEnumerable<OrganizationView> ToOrganizationView(IEnumerable<OrganizationsLiceses> data, List<TarifAPI.TarifResponse> tarifs)
        {
            try
            {
                return data.SelectMany(orgLicense =>
                {
                    if (orgLicense == null)
                        return [];

                    if (orgLicense.Licenses == null || !orgLicense.Licenses.Any())
                        return CreateEmptyLicence(orgLicense);

                    return orgLicense.Licenses.Select(license => CreateLicenceView(tarifs, orgLicense, license));
                });
            }
            catch
            {
                return [];
            }
        }

        private static IEnumerable<OrganizationView> CreateEmptyLicence(OrganizationsLiceses orgLicense)
        {
            return new List<OrganizationView>
                    {
                        new() 
                        {
                            OrganizationId = orgLicense.Organization.Id,
                            OrganizationName = orgLicense.Organization.Name,
                            OrganizationInn = orgLicense.Organization.Inn,
                            OrganizationKpp = orgLicense.Organization.Kpp,
                            OrganizationEmail = orgLicense.Organization.Email,
                            OrganizationPhone = orgLicense.Organization.Phone,
                        }
                    };
        }

        private static OrganizationView CreateLicenceView(List<TarifAPI.TarifResponse> tarifs, OrganizationsLiceses orgLicense, LicenseAPI.LicenseResponse license)
        {
            return new OrganizationView
            {
                OrganizationId = orgLicense.Organization.Id,
                OrganizationName = orgLicense.Organization.Name,
                OrganizationInn = orgLicense.Organization.Inn,
                OrganizationKpp = orgLicense.Organization.Kpp,
                OrganizationEmail = orgLicense.Organization.Email,
                OrganizationPhone = orgLicense.Organization.Phone,
                LicenceId = license.Id,
                ProgramName = tarifs.First(t => t.Id == license.TarifId).Program.ToString(),
                TarifName = tarifs.First(t => t.Id == license.TarifId).Name,
                DateCreated = license.DateCreated.ToShortDateString(),
                StartDate = license.StartDate.ToShortDateString(),
                EndDate = license.EndDate.ToShortDateString(),
            };
        }
    }
}


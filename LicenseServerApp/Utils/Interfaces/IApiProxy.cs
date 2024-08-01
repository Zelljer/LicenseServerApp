using LicenseServerApp.Models;
using Refit;

namespace LicenseServerApp.Utils.Interfaces
{
    public interface IApiProxy
	{
        [Get("/api/Licenses/licensesOrg")]
		Task<List<LicenseAPI.LicenseResponse>> GetLicensesByOrg(int orgId);


		[Get("/api/Licenses/licensesOrgProg")]
		Task<LicenseAPI.LicenseResponse> GetLicensesByOrgWithProg(int orgId, int programId);


		[Post("/api/Licenses/create")]
		Task CreateLicense([Body] LicenseAPI.LicenseRequest licenseData);


		[Delete("/api/Licenses/delete")]
		Task DeleteLicense(int licenseId);

		///

		[Get("/api/Organizations/organizations")]
		Task<List<LicenseAPI.LicenseResponse>> GetOrganizationsByPages(int page, int pageSize);


		[Post("/api/Organizations/create")]
		Task CreateOrganization([Body] OrganizationAPI.OrganizationRequest licenseData);

		///

        [Get("/api/Tarifs/tarifs")]
		Task<List<LicenseAPI.LicenseResponse>> GetAllTarifs();


		[Get("/api/Tarifs/tarifsId")]
		Task<List<LicenseAPI.LicenseResponse>> GetTariffById(int tarifId);


		[Post("/api/Tarifs/create")]
		Task CreateTarif([Body] TarifAPI.TarifRequest tarif);

		///

		[Post("/api/User/authorization")]
		Task<UserAPI.UserResponse> UserLogin(UserAPI.UserAuthentificationRequest user);


		[Post("/api/User/registration")]
		Task<List<LicenseAPI.LicenseResponse>> UserRegistration(UserAPI.UserRegistrationRequest user);


		[Get("/api/User/check-token")]
		Task<string> CheckToken();

	}
}

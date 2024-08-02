using LicenseServerApp.Models;
using Refit;

namespace LicenseServerApp.Utils.Interfaces
{
	public interface IApiProxy
	{
        [Get("/api/Licenses/licensesOrg")]
        Task<IHTTPResult<List<LicenseAPI.LicenseResponse>>> GetLicensesByOrg(int orgId);


		[Get("/api/Licenses/licensesOrgProg")]
        Task<IHTTPResult<List<LicenseAPI.LicenseResponse>>> GetLicensesByOrgWithProg(int orgId, int programId);


		[Post("/api/Licenses/create")]
        Task<IHTTPResult<string>> CreateLicense([Body] LicenseAPI.LicenseRequest licenseData);


		[Delete("/api/Licenses/delete")]
		Task DeleteLicense(int licenseId);

		///

		[Get("/api/Organizations/organizations")]
		Task<IHTTPResult<PagedResult<OrganizationsLiceses>>> GetOrganizationsByPages(int page, int pageSize);


		[Post("/api/Organizations/create")]
        Task<IHTTPResult<string>> CreateOrganization([Body] OrganizationAPI.OrganizationRequest licenseData);

		///

        [Get("/api/Tarifs/tarifs")]
        Task<IHTTPResult<List<TarifAPI.TarifResponse>>> GetAllTarifs();


		[Get("/api/Tarifs/tarifsId")]
        Task<IHTTPResult<TarifAPI.TarifResponse>> GetTarifById(int tarifId);


		[Post("/api/Tarifs/create")]
        Task<IHTTPResult<string>> CreateTarif([Body] TarifAPI.TarifRequest tarif);

		///

		[Post("/api/User/authorization")]
		Task<IHTTPResult<string>> UserLogin(UserAPI.UserAuthentificationRequest user);


		[Post("/api/User/registration")]
		Task<IHTTPResult<string>> UserRegistration([Body] UserAPI.UserRegistrationRequest user);


		[Get("/api/User/check-token")]
		Task<IHTTPResult<string>> CheckToken();

	}
}

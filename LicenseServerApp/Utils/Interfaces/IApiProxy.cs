using LicenseServerApp.Models.API;
using Refit;

namespace LicenseServerApp.Utils.Interfaces
{
    public interface IApiProxy
	{
        [Get("/api/Licenses/licensesOrg")]
        Task<IApiResponse<LicensesResult>> GetLicensesByOrg(int orgId);


		[Get("/api/Licenses/licensesOrgProg")]
        Task<IApiResponse<LicensesResult>> GetLicensesByOrgWithProg(int orgId, int programId);


		[Post("/api/Licenses/create")]
        Task<IApiResponse<StringResult>> CreateLicense([Body] LicenseAPI.LicenseRequest licenseData);


		[Delete("/api/Licenses/delete")]
        Task<IApiResponse<StringResult>> DeleteLicense(int licenseId);

		///

		[Get("/api/Organizations/organizations")]
		Task<IApiResponse<OrganizationsResult>> GetOrganizationsByPages(int page, int pageSize);


		[Post("/api/Organizations/create")]
        Task<IApiResponse<StringResult>> CreateOrganization([Body] OrganizationAPI.OrganizationRequest licenseData);

		///

        [Get("/api/Tarifs/tarifs")]
        Task<IApiResponse<TarifsResult>> GetAllTarifs();


		[Get("/api/Tarifs/tarifsId")]
        Task<IApiResponse<TarifResult>> GetTarifById(int tarifId);


		[Post("/api/Tarifs/create")]
        Task<IApiResponse<StringResult>> CreateTarif([Body] TarifAPI.TarifRequest tarif);

		///

		[Post("/api/User/authorization")]
		Task<IApiResponse<StringResult>> UserLogin(UserAPI.UserAuthentificationRequest user);


		[Post("/api/User/registration")]
		Task<IApiResponse<StringResult>> UserRegistration([Body] UserAPI.UserRegistrationRequest user);


		[Get("/api/User/check-token")]
		Task<IApiResponse<StringResult>> CheckToken();

	}
}

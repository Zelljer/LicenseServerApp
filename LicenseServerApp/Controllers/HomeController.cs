using LicenseServerApp.Models;
using LicenseServerApp.Utils.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace LicenseServerApp.Controllers
{
	public class HomeController(ILogger<HomeController> logger, IApiProxy apiProxy) : Controller
	{
		public async Task<IActionResult> Index()
		{
			return View();
		}

        #region Users

        public async Task<IActionResult> UserRegistration(UserAPI.UserRegistrationRequest user)
        {
            await apiProxy.UserRegistration(user);
            return View();
        }

		public async Task<IActionResult> UserAuthentification(UserAPI.UserAuthentificationRequest user)
		{
            await apiProxy.UserLogin(user);
            logger.LogError(apiProxy.UserLogin(user).Result.Id.ToString());
            return RedirectToAction("Index");

        }

        public async Task<string> GetCurrentToken()
        {
            return await apiProxy.CheckToken();
        }

        #endregion

        #region License
        public async Task<IActionResult> CreateLicense(LicenseAPI.LicenseRequest license)
        {
            await apiProxy.CreateLicense(license);
            return View();
        }

        public async Task<IActionResult> DeleteLicense(int licenseId)
        {
            await apiProxy.DeleteLicense(licenseId);
            return View();
        }

        public async Task<IActionResult> GetLicenses(int orgId, int? program)
        {
            if (program != null) 
                await apiProxy.GetLicensesByOrgWithProg(orgId, (int)program);
            else
                await apiProxy.GetLicensesByOrg(orgId);
            return View();
        }
        #endregion

        #region Organization
        public async Task<IActionResult> CreateOrganization(OrganizationAPI.OrganizationRequest organization)
        {
            await apiProxy.CreateOrganization(organization);
            return View();
        }

        public async Task<IActionResult> GetLicenses(int page, int pageSize)
        {
            await apiProxy.GetOrganizationsByPages(page, pageSize);
            return View();
        }
        #endregion

        #region Tarifs
        public async Task<IActionResult> CreateTarif(LicenseAPI.LicenseRequest license)
        {
            await apiProxy.CreateLicense(license);
            return View();
        }

        public async Task<IActionResult> DeleteTarif(int licenseId)
        {
            await apiProxy.DeleteLicense(licenseId);
            return View();
        }

        public async Task<IActionResult> GetTarifs(int? tarifId)
        {
            if (tarifId != null)
                await apiProxy.GetTariffById((int)tarifId);
            else
                await apiProxy.GetAllTarifs();
            return View();
        }
        #endregion

        public async Task<IActionResult> SetPartial(int partialId)
		{
			object model = new();
			var name = string.Empty;
            switch (partialId)
			{
				case 0:
					model = new List<LicenseAPI.LicenseResponse>(); name = "License/_LicenseListByOrgPartial"; break;
                case 1:
                    model = new LicenseAPI.LicenseRequest(); name = "License/_LicenseCreatePartial"; break;
                case 2:
                    model = new LicenseAPI.LicenseResponse(); ; name = "License/_LicenseDeletePartial"; break;


                case 3:
                    model = new List<OrganizationAPI.OrganizationResponse>(); name = "Organization/_OrganizationListByPagePartial"; break;
                case 4:
                    model = new OrganizationAPI.OrganizationRequest(); name = "Organization/_OrganizationAddPartial"; break;


                case 5:
                    model = new List<TarifAPI.TarifResponse>(); name = "Tarif/_TarifListPartial"; break;
                case 6:
                    model = new TarifAPI.TarifRequest(); name = "Tarif/_TarifAddPartial"; break;


                case 7:
                    model = new UserAPI.UserAuthentificationRequest(); name = "User/_UserLoginPartial"; break;
                case 8:
                    model = new UserAPI.UserRegistrationRequest(); name = "User/_UserRegistrationPartial"; break;
                case 9:
                    model = ""; name = "User/_UserGetCurrentTokenPartial"; break;

            }

            return PartialView("Partials/"+name, model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

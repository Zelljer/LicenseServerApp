using LicenseServerApp.Models;
using LicenseServerApp.Utils.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
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
            var result = await apiProxy.UserRegistration(user);
            logger.LogError(result.Data);
            return RedirectToAction("Index");
        }

		public async Task<IActionResult> UserAuthentification(UserAPI.UserAuthentificationRequest user)
		{
            var result = await apiProxy.UserLogin(user);
            Response.Cookies.Append("Authorization", result.Data, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
            return RedirectToAction("Index");
        }

        public string GetCurrentToken()
        {
            var result = apiProxy.CheckToken();
            return result.Result.Data;
        }

        #endregion

        #region License
        public async Task<IActionResult> CreateLicense(LicenseAPI.LicenseRequest license)
        {
            var result = await apiProxy.CreateLicense(license);
            logger.LogError(result.Data);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteLicense(int licenseId)
        {
            await apiProxy.DeleteLicense(licenseId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetLicensesByOrg(int orgId)
        {

            var result = await apiProxy.GetLicensesByOrg(orgId);
            return PartialView("Partials/License/_LicenseListByOrgPartial", result.Data);
        }

        public async Task<IActionResult> GetLicensesByOrgWithProg(int orgId, int programId)
        {

            var result = await apiProxy.GetLicensesByOrgWithProg(orgId,programId);
            return PartialView("Partials/License/_LicenseListByOrgWithProgPartial", result.Data);
        }
        #endregion

        #region Organization
        public async Task<IActionResult> CreateOrganization(OrganizationAPI.OrganizationRequest organization)
        {
            var result = await apiProxy.CreateOrganization(organization);
            logger.LogError(result.Data);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetOrganizationsByPages(int page, int pageSize)
        {
            var result = await apiProxy.GetOrganizationsByPages(page, pageSize);
            return PartialView("Partials/Organization/_OrganizationListByPagePartial", result.Data); 
        }
        #endregion

        #region Tarifs
        public async Task<IActionResult> CreateTarif(TarifAPI.TarifRequest tarif)
        {
            var result = await apiProxy.CreateTarif(tarif);
            logger.LogError(result.Data);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteTarif(int tarifId)
        {
            await apiProxy.DeleteLicense(tarifId);
            return View();
        }

        public async Task<IActionResult> GetTarifs()
        {
            await apiProxy.GetAllTarifs();
            return View();
        }

        public async Task<IActionResult> GetTarifById(int tarifid)
        {
            var result = await apiProxy.GetTarifById(tarifid);
            return PartialView("Partials/Tarif/_TaridByIdPartial", result.Data);
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
                    model = new List<LicenseAPI.LicenseResponse>(); name = "License/_LicenseListByOrgWithProgPartial"; break;
                case 2:
                    model = new LicenseAPI.LicenseRequest(); name = "License/_LicenseCreatePartial"; break;
                case 3:
                    model = new LicenseAPI.LicenseResponse(); ; name = "License/_LicenseDeletePartial"; break;


                case 4:
                    model = new PagedResult<OrganizationsLiceses>(); name = "Organization/_OrganizationListByPagePartial"; break;
                case 5:
                    model = new OrganizationAPI.OrganizationRequest(); name = "Organization/_OrganizationAddPartial"; break;


                case 6:
                    model = apiProxy.GetAllTarifs().Result.Data; name = "Tarif/_TarifListPartial"; break;
                case 7:
                    model = new TarifAPI.TarifResponse(); name = "Tarif/_TaridByIdPartial"; break;
                case 8:
                    model = new TarifAPI.TarifRequest(); name = "Tarif/_TarifAddPartial"; break;


                case 9:
                    model = new UserAPI.UserAuthentificationRequest(); name = "User/_UserLoginPartial"; break;
                case 10:
                    model = new UserAPI.UserRegistrationRequest(); name = "User/_UserRegistrationPartial"; break;
                case 11:
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

using LicenseServerApp.Models;
using LicenseServerApp.Models.API;
using LicenseServerApp.Models.API.Dependencies;
using LicenseServerApp.Models.View;
using LicenseServerApp.Utils.Converters;
using LicenseServerApp.Utils.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LicenseServerApp.Controllers
{
	public class HomeController(ILogger<HomeController> logger, IApi apiProxy) : Controller
	{
		public async Task<IActionResult> Index()
		{
			return View();
		}

        #region Users

        public async Task<IActionResult> UserRegistration(UserAPI.UserRegistrationRequest user)
        {
            try
            {
				var errors = new List<string>();
				var result = await apiProxy.UserRegistration(user);

				if (!result.IsSuccessStatusCode)
					errors.Add("Ошибка при отправке запроса на сервер");

				else if (!result.Content.IsSuccsess)
					errors.AddRange(result.Content.Errors);

				if (errors.Any())
				{
					TempData["AlertMessage"] = errors.ToArray();
					foreach (var error in errors)
						logger.LogError(error);
					return RedirectToAction("Index");
				}

                TempData["AlertMessage"] = new[] { result.Content.Data };
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
				TempData["AlertMessage"] = new[] { "Произошла ошибка" };
				logger.LogError(ex.Message);
                return RedirectToAction("Index");
            }
        }

		public async Task<IActionResult> UserAuthentification(UserAPI.UserAuthentificationRequest user)
		{
            try
            {
                var errors = new List<string>();
                var result = await apiProxy.UserLogin(user);

                if (!result.IsSuccessStatusCode)
                    errors.Add("Ошибка при отправке запроса на сервер");

                else if (!result.Content.IsSuccsess)
                    errors.AddRange(result.Content.Errors);

				if (errors.Any())
				{
					TempData["AlertMessage"] = errors.ToArray();
					foreach (var error in errors)
						logger.LogError(error);
					return RedirectToAction("Index");
				}
				
                Response.Cookies.Append("Authorization", result.Content.Data.ToString(), new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
				TempData["AlertMessage"] = new [] { "Авторизация прошла успешно" };
                return RedirectToAction("Index");
                   
            }
            catch (Exception ex)
            {
				TempData["AlertMessage"] = new[] { "Произошла ошибка" };
				logger.LogError(ex.Message);
				return RedirectToAction("Index");
			}
        }

        public async Task<IActionResult> GetCurrentToken()
        {
            try
            {
				var errors = new List<string>();
				var result = await apiProxy.CheckToken();

				if (!result.IsSuccessStatusCode)
					errors.Add("Ошибка при отправке запроса на сервер");

				else if (!result.Content.IsSuccsess)
					errors.AddRange(result.Content.Errors);

				if (errors.Any())
				{
					TempData["AlertMessage"] = errors.ToArray();
					foreach (var error in errors)
						logger.LogError(error);
					return PartialView("Partials/User/_UserGetCurrentTokenPartial");
				}

                return PartialView("Partials/User/_UserGetCurrentTokenPartial", result.Content.Data);
            }
            catch (Exception ex)
            {
				TempData["AlertMessage"] = new[] { "Произошла ошибка" };
				logger.LogError(ex.Message);
                return PartialView("Partials/User/_UserGetCurrentTokenPartial");
            }
        }

        #endregion

        #region License
        public async Task<IActionResult> CreateLicense(LicenseAPI.LicenseRequest license)
        {
            try
            {
				var errors = new List<string>();
				var result = await apiProxy.CreateLicense(license);

				if (!result.IsSuccessStatusCode)
					errors.Add("Ошибка при отправке запроса на сервер");

				else if (!result.Content.IsSuccsess)
					errors.AddRange(result.Content.Errors);

				if (errors.Any())
				{
					TempData["AlertMessage"] = errors.ToArray();
					foreach (var error in errors)
						logger.LogError(error);
					return RedirectToAction("Index");
				}

                TempData["AlertMessage"] = new[] { result.Content.Data };
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
				TempData["AlertMessage"] = new[] { "Произошла ошибка" };
				logger.LogError(ex.Message);
                return RedirectToAction("Index");
            }
        }

		public async Task<IActionResult> DeleteLicense(int licenseId)
        {
            try
            {
				var errors = new List<string>();
				var result = await apiProxy.DeleteLicense(licenseId);

				if (!result.IsSuccessStatusCode)
					errors.Add("Ошибка при отправке запроса на сервер");

				else if (!result.Content.IsSuccsess)
					errors.AddRange(result.Content.Errors);

				if (errors.Any())
				{
					TempData["AlertMessage"] = errors.ToArray();
					foreach (var error in errors)
						logger.LogError(error);
					return PartialView("Partials/License/_LicenseDeletePartial");
				}

				TempData["AlertMessage"] = new[] { result.Content.Data };
				return PartialView("Partials/License/_LicenseDeletePartial");
			}
            catch (Exception ex)
            {
				TempData["AlertMessage"] = new[] { "Произошла ошибка" };
				logger.LogError(ex.Message);
				return PartialView("Partials/License/_LicenseDeletePartial");
			}
        }

        public async Task<IActionResult> GetLicensesByOrg(int orgId)
        {
            var currentLicense = new List<LicenseView>();
            try
            {
                var errors = new List<string>();
                var licenseResult = await apiProxy.GetLicensesByOrg(orgId);
                var tarifs = GetTarifs();
                var organizationResult = await apiProxy.GetOrganizationById(orgId);

                if (!licenseResult.IsSuccessStatusCode || !organizationResult.IsSuccessStatusCode)
                    errors.Add("Ошибка при отправке запроса на сервер");

                else if (!licenseResult.Content.IsSuccsess || !organizationResult.Content.IsSuccsess)
                {
                    errors.AddRange(licenseResult.Content.Errors);
                    errors.AddRange(organizationResult.Content.Errors);
                }

                if (errors.Any())
                {
                    TempData["AlertMessage"] = errors.ToArray();
                    foreach (var error in errors)
                        logger.LogError(error);
                    return PartialView("Partials/License/_LicenseListByOrgPartial", currentLicense);
                }

                currentLicense = DataConverter.ToLicenseView(licenseResult.Content.Data,tarifs,organizationResult.Content.Data).ToList();
                return PartialView("Partials/License/_LicenseListByOrgPartial", currentLicense);
            }
            catch (Exception ex)
            {
				TempData["AlertMessage"] = new[] { "Произошла ошибка" };
				logger.LogError(ex.Message);
				return PartialView("Partials/License/_LicenseListByOrgPartial", currentLicense);
			}
        }

        public async Task<IActionResult> GetLicensesByOrgWithProg(int orgId, int programId) // Буду исправлять
        {
			var currentLicense = new List<LicenseView>();
			try
			{
				var errors = new List<string>();
				var licenseResult = await apiProxy.GetLicensesByOrgWithProg(orgId, programId);
				var tarifs = GetTarifs();
				var organizationResult = await apiProxy.GetOrganizationById(orgId);

				if (!licenseResult.IsSuccessStatusCode || !organizationResult.IsSuccessStatusCode)
					errors.Add("Ошибка при отправке запроса на сервер");

				else if (!licenseResult.Content.IsSuccsess || !organizationResult.Content.IsSuccsess)
				{
					errors.AddRange(licenseResult.Content.Errors);
					errors.AddRange(organizationResult.Content.Errors);
				}

				if (errors.Any())
				{
					TempData["AlertMessage"] = errors.ToArray();
					foreach (var error in errors)
						logger.LogError(error);
					return PartialView("Partials/License/_LicenseListByOrgWithProgPartial", currentLicense);
				}

				currentLicense = DataConverter.ToLicenseView(licenseResult.Content.Data, tarifs, organizationResult.Content.Data).ToList();
				return PartialView("Partials/License/_LicenseListByOrgWithProgPartial", currentLicense);
			}
			catch (Exception ex)
			{
				TempData["AlertMessage"] = new[] { "Произошла ошибка" };
				logger.LogError(ex.Message);
				return PartialView("Partials/License/_LicenseListByOrgWithProgPartial", currentLicense);
			}
        }
        #endregion

        #region Organization
        public async Task<IActionResult> CreateOrganization(OrganizationAPI.OrganizationRequest organization)
        {
            try
            {
				var errors = new List<string>();
				var result = await apiProxy.CreateOrganization(organization);

				if (!result.IsSuccessStatusCode)
					errors.Add("Ошибка при отправке запроса на сервер");

				else if (!result.Content.IsSuccsess)
					errors.AddRange(result.Content.Errors);

				if (errors.Any())
				{
					TempData["AlertMessage"] = errors.ToArray();
					foreach (var error in errors)
						logger.LogError(error);
					return RedirectToAction("Index");
				}

                TempData["AlertMessage"] = new[] { result.Content.Data };
                return RedirectToAction("Index");
                  
            }
            catch (Exception ex)
            {
				TempData["AlertMessage"] = new[] { "Произошла ошибка" };
				logger.LogError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> GetOrganizationsByPages(int page, int pageSize)
        {
            var currentPage = new PagedResult<OrganizationView> { TotalPages = 1, CurrentPage = 1, Items = new List<OrganizationView>() };
            try
            {
                var errors = new List<string>();
                var result = await apiProxy.GetOrganizationsByPages(page, pageSize);
                var tarifs = GetTarifs();

                if (!result.IsSuccessStatusCode)
                    errors.Add("Произошла ошибка при отправке запроса на сервер");

                else if (!result.Content.IsSuccsess)
                    errors.AddRange(result.Content.Errors);

                if (errors.Any())
                {
                    TempData["AlertMessage"] = errors.ToArray();
                    foreach (var error in errors)
                        logger.LogError(error);
                    return PartialView("Partials/Organization/_OrganizationListByPagePartial", currentPage);
                }

                var data = result.Content.Data;
                currentPage.TotalPages = data?.TotalPages ?? 1;
                currentPage.CurrentPage = data?.TotalPages ?? 1;
                currentPage.Items = DataConverter.ToOrganizationView(data?.Items ?? new List<OrganizationsLiceses>(), tarifs);

                return PartialView("Partials/Organization/_OrganizationListByPagePartial", currentPage);
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = new[] { "Произошла ошибка" };
                logger.LogError(ex.Message);
                return PartialView("Partials/Organization/_OrganizationListByPagePartial", currentPage);
            }
        }
        #endregion

        #region Tarifs
        public async Task<IActionResult> CreateTarif(TarifAPI.TarifRequest tarif)
        {
            try
            {
				var errors = new List<string>();
				var result = await apiProxy.CreateTarif(tarif);

				if (!result.IsSuccessStatusCode)
					errors.Add("Ошибка при отправке запроса на сервер");

				else if (!result.Content.IsSuccsess)
					errors.AddRange(result.Content.Errors);

				if (errors.Any())
				{
					TempData["AlertMessage"] = errors.ToArray();
					foreach (var error in errors)
						logger.LogError(error);
					return RedirectToAction("Index");
				}

                TempData["AlertMessage"] = new[] { result.Content.Data };
                return RedirectToAction("Index");   
            }
            catch(Exception ex)
            {
				TempData["AlertMessage"] = new[] { "Произошла ошибка" };
				logger.LogError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        public List<TarifAPI.TarifResponse> GetTarifs()
        {
            try
            {
				var errors = new List<string>();
                //var auth = Request.Cookies["Authorization"];
                var result = apiProxy.GetAllTarifs();

				if (!result.Result.IsSuccessStatusCode)
					errors.Add("Ошибка при отправке запроса на сервер");

				else if (!result.Result.Content.IsSuccsess)
					errors.AddRange(result.Result.Content.Errors);

				if (errors.Any())
				{
					TempData["AlertMessage"] = errors.ToArray();
					foreach (var error in errors)
						logger.LogError(error);
					return [];
				}

                return result.Result.Content.Data;
            }
            catch (Exception ex)
            {
				TempData["AlertMessage"] = new[] { "Произошла ошибка" };
				logger.LogError(ex.Message);
                return [];
            }
        }

        public async Task<IActionResult> GetTarifById(int tarifid)
        {
            try
            {
				var errors = new List<string>();
				var result = await apiProxy.GetTarifById(tarifid);

				if (!result.IsSuccessStatusCode)
					errors.Add("Ошибка при отправке запроса на сервер");

				else if (!result.Content.IsSuccsess)
					errors.AddRange(result.Content.Errors);

				if (errors.Any())
				{
					TempData["AlertMessage"] = errors.ToArray();
					foreach (var error in errors)
						logger.LogError(error);
					return PartialView("Partials/Tarif/_TaridByIdPartial", new TarifAPI.TarifResponse());
				}

                return PartialView("Partials/Tarif/_TaridByIdPartial", result.Content.Data);
                   
            }
            catch(Exception ex)
            {
				TempData["AlertMessage"] = new[] { "Произошла ошибка" };
				logger.LogError($"{ex.Message}", ex);
                return PartialView("Partials/Tarif/_TaridByIdPartial", new TarifAPI.TarifResponse());
            }
        }
        #endregion

        public async Task<IActionResult> ReloadView(string viewName, object model)
		{
            return PartialView("Partials/" + viewName, model);
		}
		public async Task<IActionResult> SetPartial(int partialId)
		{
            var organizationsViewData = new PagedResult<OrganizationView>
            {
                Items = new List<OrganizationView>(),
                CurrentPage = 1,
                TotalPages=1
            };

            object model = new();
		    var name = string.Empty;
            switch (partialId)
		    {
			    case 0:
				    model = new List<LicenseView>(); name = "License/_LicenseListByOrgPartial"; break;
                case 1:
                    model = new List<LicenseView>(); name = "License/_LicenseListByOrgWithProgPartial"; break;
                case 2:
                    model = new LicenseAPI.LicenseRequest(); name = "License/_LicenseCreatePartial"; break;
                case 3:
                    model = new LicenseAPI.LicenseResponse(); ; name = "License/_LicenseDeletePartial"; break;


                case 4:
                    model = organizationsViewData; name = "Organization/_OrganizationListByPagePartial"; break;
                case 5:
                    model = new OrganizationAPI.OrganizationRequest(); name = "Organization/_OrganizationAddPartial"; break;


                case 6:
                    model = GetTarifs(); name = "Tarif/_TarifListPartial"; break;
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

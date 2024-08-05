using LicenseServerApp.Models;
using LicenseServerApp.Utils.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var result = await apiProxy.UserRegistration(user);
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.IsSuccsess)
                    {
                        TempData["AlertMessage"] = new[] { result.Content.Data };
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["AlertMessage"] = result.Content.Errors;
                        foreach (var error in result.Content.Errors)
                            logger.LogError(error);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var errorText = "Произошла ошибка при отправке запроса на сервер";
                    TempData["AlertMessage"] = errorText;
                    logger.LogError(errorText);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Index");
            }
        }

		public async Task<IActionResult> UserAuthentification(UserAPI.UserAuthentificationRequest user)
		{
            try
            {
                var result = await apiProxy.UserLogin(user);
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.IsSuccsess)
                    {
                        Response.Cookies.Append("Authorization", result.Content.Data.ToString(), new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict
                        });
						TempData["AlertMessage"] = new [] { "Авторизация прошла успешно" };
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["AlertMessage"] = result.Content.Errors;
                        foreach (var error in result.Content.Errors)
                            logger.LogError(error);
						return RedirectToAction("Index");
					}
                }
                else
                {
                    var errorText = "Произошла ошибка при отправке запроса на сервер";
                    TempData["AlertMessage"] = new[] { errorText };
                    logger.LogError(errorText);
					return RedirectToAction("Index");
				}
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
				return RedirectToAction("Index");
			}
        }

        public string GetCurrentToken()
        {
            try
            {
                var result = apiProxy.CheckToken();
                if (result.Result.IsSuccessStatusCode)
                {
                    if (result.Result.Content.IsSuccsess)
                        return result.Result.Content.Data.ToString();
                }
                else
                {
                    var errorText = "Произошла ошибка при отправке запроса на сервер";
                    TempData["AlertMessage"] = errorText;
                    logger.LogError(errorText);
                }
                return "";
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return "";
            }
        }

        #endregion

        #region License
        public async Task<IActionResult> CreateLicense(LicenseAPI.LicenseRequest license)
        {
            try
            {
                var result = await apiProxy.CreateLicense(license);
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.IsSuccsess)
                    {
                        TempData["AlertMessage"] = new[] { result.Content.Data };
                        return RedirectToAction("Index");
                    }
					else
                    {
                        TempData["AlertMessage"] = result.Content.Errors;
                        foreach (var error in result.Content.Errors)
                            logger.LogError(error);
                       return PartialView("Partials/License/_LicenseCreatePartial", result.Content.Data);
                    }
                }
                else
                {
                    var errorText = "Произошла ошибка при отправке запроса на сервер";
                    TempData["AlertMessage"] = errorText;
                    logger.LogError(errorText);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> DeleteLicense(int licenseId)
        {
            try
            {
                var result = await apiProxy.DeleteLicense(licenseId);
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.IsSuccsess)
                        return RedirectToAction("Index");
                    else
                    {
                        TempData["AlertMessage"] = result.Content.Errors;
                        foreach (var error in result.Content.Errors)
                            logger.LogError(error);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var errorText = "Произошла ошибка при отправке запроса на сервер";
                    TempData["AlertMessage"] = errorText;
                    logger.LogError(errorText);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> GetLicensesByOrg(int orgId)
        {
            try
            {
                var result = await apiProxy.GetLicensesByOrg(orgId);
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.IsSuccsess)
                        return PartialView("Partials/License/_LicenseListByOrgPartial", result.Content.Data);
                    else
                    {
                        TempData["AlertMessage"] = result.Content.Errors;
                        foreach (var error in result.Content.Errors)
                            logger.LogError(error);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var errorText = "Произошла ошибка при отправке запроса на сервер";
                    TempData["AlertMessage"] = errorText;
                    logger.LogError(errorText);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> GetLicensesByOrgWithProg(int orgId, int programId)
        {
            try
            {
                var result = await apiProxy.GetLicensesByOrgWithProg(orgId, programId);
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.IsSuccsess)
                        return PartialView("Partials/License/_LicenseListByOrgWithProgPartial", result.Content.Data);
                    else
                    {
                        TempData["AlertMessage"] = result.Content.Errors;
                        foreach (var error in result.Content.Errors)
                            logger.LogError(error);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var errorText = "Произошла ошибка при отправке запроса на сервер";
                    TempData["AlertMessage"] = errorText;
                    logger.LogError(errorText);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Organization
        public async Task<IActionResult> CreateOrganization(OrganizationAPI.OrganizationRequest organization)
        {
            try
            {
                var result = await apiProxy.CreateOrganization(organization);
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.IsSuccsess)
                    {
                        TempData["AlertMessage"] = new[] { result.Content.Data };
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["AlertMessage"] = result.Content.Errors;
                        foreach (var error in result.Content.Errors)
                            logger.LogError(error);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var errorText = "Произошла ошибка при отправке запроса на сервер";
                    TempData["AlertMessage"] = errorText;
                    logger.LogError(errorText);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> GetOrganizationsByPages(int page, int pageSize)
        {
            try
            {
                var result = await apiProxy.GetOrganizationsByPages(page, pageSize);
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.IsSuccsess)
                        return PartialView("Partials/Organization/_OrganizationListByPagePartial", result.Content.Data);
                    else
                    {
                        TempData["AlertMessage"] = result.Content.Errors;
                        foreach (var error in result.Content.Errors)
                            logger.LogError(error);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var errorText = "Произошла ошибка при отправке запроса на сервер";
                    TempData["AlertMessage"] = errorText;
                    logger.LogError(errorText);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Tarifs
        public async Task<IActionResult> CreateTarif(TarifAPI.TarifRequest tarif)
        {
            try
            {
                var result = await apiProxy.CreateTarif(tarif);
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.IsSuccsess)
                    {
                        TempData["AlertMessage"] = new[] { result.Content.Data };
                        return RedirectToAction("Index");
                    }  
                    else
                    {
                        TempData["AlertMessage"] = result.Content.Errors;
                        foreach (var error in result.Content.Errors)
                            logger.LogError(error);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var errorText = "Произошла ошибка при отправке запроса на сервер";
                    TempData["AlertMessage"] = new[] { errorText };
                    logger.LogError(errorText);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> GetTarifs()
        {
            try
            {
                var result = await apiProxy.GetAllTarifs();
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.IsSuccsess)
                        return PartialView("Partials/Tarif/_TarifListPartial", result.Content.Data);
                    else
                    {
                        TempData["AlertMessage"] = result.Content.Errors;
                        foreach (var error in result.Content.Errors)
                            logger.LogError(error);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var errorText = "Произошла ошибка при отправке запроса на сервер";
                    TempData["AlertMessage"] = errorText;
                    logger.LogError(errorText);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> GetTarifById(int tarifid)
        {
            try
            {
                var result = await apiProxy.GetTarifById(tarifid);
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.IsSuccsess)
                        return PartialView("Partials/Tarif/_TaridByIdPartial", result.Content.Data);
                    else
                    {
                        TempData["AlertMessage"] = result.Content.Errors;
                        foreach (var error in result.Content.Errors)
                            logger.LogError(error);
                        return PartialView("Partials/Tarif/_TaridByIdPartial", new TarifAPI.TarifResponse());
                    }
                }
                else 
                {
                    var errorText = "Произошла ошибка при отправке запроса на сервер";
                    TempData["AlertMessage"] = errorText;
                    logger.LogError(errorText);
                    return PartialView("Partials/Tarif/_TaridByIdPartial", new TarifAPI.TarifResponse());
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex.Message}", ex);
                return PartialView("Partials/Tarif/_TaridByIdPartial", new TarifAPI.TarifResponse());
            }
        }
        #endregion

        public async Task<IActionResult> SetPartial(int partialId)
		{
            var organizationsViewData = new PagedResult<OrganizationsLiceses>()
            {
                Items = new List<OrganizationsLiceses>(),
                CurrentPage = 1,
                TotalPages=1
            };

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
                    model = organizationsViewData; name = "Organization/_OrganizationListByPagePartial"; break;
                case 5:
                    model = new OrganizationAPI.OrganizationRequest(); name = "Organization/_OrganizationAddPartial"; break;


                case 6:
                    await GetTarifs(); break;
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

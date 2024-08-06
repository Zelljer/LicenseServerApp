using LicenseServerApp.Models.API;
using LicenseServerApp.Models.API.Types;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LicenseServerApp.Models.View
{
    public class ViewModel
    {
        public int OrganizationId { get; set; }
        public string Inn { get; set; }
        public string? Kpp { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int LicenceId { get; set; } // Id лицензии
        public string ProgramName { get; set; } // Программа
        public string TarifName { get; set; } // Тариф
        public string DateCreate { get; set; } // Дата создания лицензии
        public string DateStart { get; set; } // Дата начала действия лицензии
        public string DateEnd { get; set; } // Дата окончания действия лицензии
    }
}


/*
 Список организаций с их активынми лицензиями:

    1. Id организации = Organization.Id
    2. название организации = Organization.Name
    3. Инн организации = Organization.Inn
    4. Кпп организации = Organization.Kpp
    5. Элю почта организации = Organization.Email
    6. Телефон организации = Organization.Phone
    7. Название тарифа = TarifName
    8. Название программы = ProgramName
    9. Дата создания лицензии = Organization.Id
    10. Дата начала действия лицензии = Organization.Id
    11. Дата окончания действия лицензии = Organization.Id

Список лицензиq:

    1. Id лицензии = LicenceId
    2. название организации = Organization.Name
    3. Название тарифа = TarifName
    4. Название программы = ProgramName
    5. Дата создания лицензии = Organization.Id
    6. Дата начала действия лицензии = Organization.Id
    7. Дата окончания действия лицензии = Organization.Id

 */
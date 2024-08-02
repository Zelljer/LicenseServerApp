namespace LicenseServerApp.Models
{
    public class IHTTPResult<T>() 
    {
        public string Status => "Success";
        public T Data { get; set; }

    }
}

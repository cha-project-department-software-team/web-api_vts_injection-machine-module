namespace InjectionMachineModule.Application.Helpers;

public class MesApiUrlHelper
{
    private readonly string _serverUrl;

    public MesApiUrlHelper()
    {
        _serverUrl = "https://mesmicroserviceapiservice.azurewebsites.net/api/";
    }

    public string GenerateResourceUrl(string resource)
    {
        return _serverUrl + resource;
    }

    public static string GeneratePageQuery(string? idStartedWith, int pageIndex, int pageSize)
    {
        if (idStartedWith == null)
        {
            return $"?PageIndex={pageIndex}&PageSize={pageSize}";
        }
        else
        {
            return $"?IdStartedWith={idStartedWith}&PageIndex={pageIndex}&PageSize={pageSize}";
        }
    }
}

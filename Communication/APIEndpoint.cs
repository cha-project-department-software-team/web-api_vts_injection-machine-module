namespace InjectionMachineModule.Communication;

public static class APIEndpoint
{
    public static string GetEndpoint(string? idStartedWith, int pageIndex, int pageSize)
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

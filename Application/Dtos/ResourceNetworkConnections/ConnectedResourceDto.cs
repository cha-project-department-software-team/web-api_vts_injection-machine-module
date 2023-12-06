namespace InjectionMachineModule.Application.Dtos.ResourceNetworkConnections;

public class ConnectedResourceDto
{
    public string ResourceId { get; set; }
    public string ResourceType { get; set; }

    public ConnectedResourceDto(string resourceId, string resourceType)
    {
        ResourceId = resourceId;
        ResourceType = resourceType;
    }
}

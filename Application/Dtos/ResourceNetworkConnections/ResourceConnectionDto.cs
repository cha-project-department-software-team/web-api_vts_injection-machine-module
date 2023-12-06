namespace InjectionMachineModule.Application.Dtos.ResourceNetworkConnections;

public class ResourceConnectionDto
{
    public string ConnectionId { get; set; }
    public string Description { get; set; }
    public List<PropertyDto> Properties { get; set; }
    public ConnectedResourceDto FromResource { get; set; }
    public ConnectedResourceDto ToResource { get; set; }

    public ResourceConnectionDto(string connectionId, string description, ConnectedResourceDto fromResource, ConnectedResourceDto toResource)
    {
        ConnectionId = connectionId;
        Description = description;
        Properties = new List<PropertyDto>();
        FromResource = fromResource;
        ToResource = toResource;
    }
}

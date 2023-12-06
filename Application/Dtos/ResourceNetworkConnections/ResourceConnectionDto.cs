﻿namespace InjectionMachineModule.Application.Dtos.ResourceNetworkConnections;

public class ResourceConnectionDto
{
    public string ConnectionId { get; set; }
    public string Description { get; set; }
    public List<PropertyDto> Properties { get; set; }
    public string FromResource { get; set; }
    public string ToResource { get; set; }

    public ResourceConnectionDto(string connectionId, string description, string fromResource, string toResource)
    {
        ConnectionId = connectionId;
        Description = description;
        Properties = new List<PropertyDto>();
        FromResource = fromResource;
        ToResource = toResource;
    }
}

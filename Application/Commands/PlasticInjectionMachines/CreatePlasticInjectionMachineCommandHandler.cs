using InjectionMachineModule.Application.Dtos.ResourceNetworkConnections;
using InjectionMachineModule.Application.Helpers;

namespace InjectionMachineModule.Application.Commands.PlasticInjectionMachines;

public class CreatePlasticInjectionMachineCommandHandler : IRequestHandler<CreatePlasticInjectionMachineCommand>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;

    public CreatePlasticInjectionMachineCommandHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }

    public async Task Handle(CreatePlasticInjectionMachineCommand request, CancellationToken cancellationToken)
    {
        var properties = request.Properties.Select(x => new PropertyDto(
            x.PropertyId,
            new PropertyType(x.PropertyId).Description,
            x.ValueString,
            new PropertyType(x.PropertyId).ValueType,
            x.ValueUnitOfMeasure))
            .ToList();

        var equipment = new CreateEquipmentDto(request.EquipmentId, request.Name, properties, "InjectionMoldingMachine", request.AbsolutePath);

        var url = _urlHelper.GenerateResourceUrl("Equipments");
        await _restClient.PostAsync(url, equipment);

        foreach (var moldId in request.Molds)
        {
            var connection = new SaveResourceNetworkConnectionDto(
                Guid.NewGuid().ToString(),
                $"Relationship between {request.EquipmentId} and {moldId}",
                request.EquipmentId,
                moldId);

            var connectionUrl = _urlHelper.GenerateResourceUrl("ResourceRelationshipNetworks/MachineMoldRelationshipId/Connections");

            await _restClient.PostAsync(connectionUrl, connection);
        }
    }
}

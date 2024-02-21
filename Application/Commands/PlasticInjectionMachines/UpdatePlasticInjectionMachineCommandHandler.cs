using InjectionMachineModule.Application.Dtos.ResourceNetworkConnections;
using InjectionMachineModule.Application.Helpers;

namespace InjectionMachineModule.Application.Commands.PlasticInjectionMachines;

public class UpdatePlasticInjectionMachineCommandHandler : IRequestHandler<UpdatePlasticInjectionMachineCommand>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;

    public UpdatePlasticInjectionMachineCommandHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }

    public async Task Handle(UpdatePlasticInjectionMachineCommand request, CancellationToken cancellationToken)
    {
        var properties = request.Properties.ConvertAll(x => new PropertyDto(
            x.PropertyId,
            new PropertyType(x.PropertyId).Description,
            x.ValueString,
            new PropertyType(x.PropertyId).ValueType,
            x.ValueUnitOfMeasure))
;

        var equipment = new UpdateEquipmentDto(request.Name, properties, "InjectionMoldingMachine", request.AbsolutePath);

        var url = _urlHelper.GenerateResourceUrl($"Equipments/{request.EquipmentId}");
        await _restClient.PutAsync(url, equipment);

        var deleteConnectionUrl = _urlHelper.GenerateResourceUrl($"ResourceRelationshipNetworks/MachineMoldRelationshipId/Connections?resourceId={request.EquipmentId}");
        await _restClient.DeleteAsync(deleteConnectionUrl);

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

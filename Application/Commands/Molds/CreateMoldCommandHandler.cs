using InjectionMachineModule.Application.Dtos.ResourceNetworkConnections;
using InjectionMachineModule.Application.Helpers;

namespace InjectionMachineModule.Application.Commands.Molds;
public class CreateMoldCommandHandler : IRequestHandler<CreateMoldCommand>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;

    public CreateMoldCommandHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }

    public async Task Handle(CreateMoldCommand request, CancellationToken cancellationToken)
    {
        var properties = request.Properties.Select(x => new PropertyDto(
            x.PropertyId,
            new PropertyType(x.PropertyId).Description,
            x.ValueString,
            new PropertyType(x.PropertyId).ValueType,
            x.ValueUnitOfMeasure))
            .ToList();

        properties.Add(new PropertyDto("Cycle", "InjectionCycle", request.CycleBySecond.ToString(), EValueType.Decimal, "Second"));

        var equipment = new CreateEquipmentDto(request.MoldId, request.Name, properties, "Mold", request.AbsolutePath);
        var url = _urlHelper.GenerateResourceUrl("Equipments");
        await _restClient.PostAsync(url, equipment);

        foreach (var machineId in request.PlasticInjectionMachines)
        {
            var connection = new SaveResourceNetworkConnectionDto(
                $"{machineId} - {request.MoldId}",
                $"Relationship between {machineId} and {request.MoldId}",
                machineId,
                request.MoldId);

            var connectionUrl = _urlHelper.GenerateResourceUrl("ResourceRelationshipNetworks/MachineMoldRelationshipId/Connections");

            await _restClient.PostAsync(connectionUrl, connection);
        }
    }
}

using InjectionMachineModule.Application.Dtos.ResourceNetworkConnections;
using InjectionMachineModule.Application.Helpers;

namespace InjectionMachineModule.Application.Commands.PlasticProduct;

public class CreatePlasticProductCommandHandler : IRequestHandler<CreatePlasticProductCommand>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;

    public CreatePlasticProductCommandHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }

    public async Task Handle(CreatePlasticProductCommand request, CancellationToken cancellationToken)
    {
        var properties = request.Properties.Select(x => new PropertyDto(
            x.PropertyId,
            new PropertyType(x.PropertyId).Description,
            x.ValueString,
            new PropertyType(x.PropertyId).ValueType,
            x.ValueUnitOfMeasure))
            .ToList();

        var materialDefinition = new CreateMaterialDefinitionDto(request.PlasticProductId, request.Name, request.PrimaryUnit, properties, "InjectionMachine-PlasticProduct");
        var url = _urlHelper.GenerateResourceUrl("MaterialDefinitions");
        await _restClient.PostAsync(url, materialDefinition);

        var operation = new CreateOperationDto();
        var operationUrl = _urlHelper.GenerateResourceUrl($"MaterialDefinitions/{request.PlasticProductId}/operations");
        await _restClient.PostAsync(operationUrl, operation);

        foreach (var moldId in request.Molds)
        {
            var connection = new SaveResourceNetworkConnectionDto(
                Guid.NewGuid().ToString(),
                $"{request.PlasticProductId} is formed with {moldId}",
                request.PlasticProductId,
                moldId);

            var connectionUrl = _urlHelper.GenerateResourceUrl("ResourceRelationshipNetworks/PlasticProductMoldRelationshipId/Connections");

            await _restClient.PostAsync(connectionUrl, connection);
        }

        foreach (var plasticMaterialId in request.PlasticMaterials)
        {
            var connection = new SaveResourceNetworkConnectionDto(
                Guid.NewGuid().ToString(),
                $"{request.PlasticProductId} is made from {plasticMaterialId}",
                request.PlasticProductId,
                plasticMaterialId);

            var connectionUrl = _urlHelper.GenerateResourceUrl("ResourceRelationshipNetworks/ProductMaterialRelationshipId/Connections");

            await _restClient.PostAsync(connectionUrl, connection);
        }
    }
}

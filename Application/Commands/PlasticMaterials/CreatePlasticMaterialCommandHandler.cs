using InjectionMachineModule.Application.Helpers;

namespace InjectionMachineModule.Application.Commands.PlasticMaterials;

public class CreatePlasticMaterialCommandHandler : IRequestHandler<CreatePlasticMaterialCommand>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;

    public CreatePlasticMaterialCommandHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }
    
    public async Task Handle(CreatePlasticMaterialCommand request, CancellationToken cancellationToken)
    {
        var properties = request.Properties.Select(x => new PropertyDto(
            x.PropertyId,
            new PropertyType(x.PropertyId).Description,
            x.ValueString,
            new PropertyType(x.PropertyId).ValueType,
            x.ValueUnitOfMeasure))
            .ToList();

        var materialDefinition = new CreateMaterialDefinitionDto(request.PlasticMaterialId, request.Name, request.PrimaryUnit, properties, "InjectionMachine-PlasticMaterial");
        var url = _urlHelper.GenerateResourceUrl("MaterialDefinitions");
        await _restClient.PostAsync(url, materialDefinition);
    }
}

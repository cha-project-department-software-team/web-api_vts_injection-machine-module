using InjectionMachineModule.Application.Helpers;
using InjectionMachineModule.Infrastructure.Communication;
using Newtonsoft.Json;
using System.Text;

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

        var materialDefinition = new MaterialDefinitionDto(request.MaterialDefinitionId, request.Name, request.PrimaryUnit, request.ModuleType, properties);
        var url = _urlHelper.GenerateResourceUrl("MaterialDefinitions");
        await _restClient.PostAsync(url, materialDefinition);
    }
}

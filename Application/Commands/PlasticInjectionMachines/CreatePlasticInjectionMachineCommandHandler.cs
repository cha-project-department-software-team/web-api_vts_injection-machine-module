using InjectionMachineModule.Application.Helpers;
using InjectionMachineModule.Infrastructure.Communication;
using Newtonsoft.Json;
using System.Text;

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

        var equipment = new EquipmentDto(request.EquipmentId, request.Name, properties, request.WorkUnit, "IM");

        var url = _urlHelper.GenerateResourceUrl("Equipments");
        await _restClient.PostAsync(url, equipment);
    }
}

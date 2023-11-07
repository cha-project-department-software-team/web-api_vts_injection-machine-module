using InjectionMachineModule.Application.Helpers;
using InjectionMachineModule.Infrastructure.Communication;
using Newtonsoft.Json;
using System;
using System.Text;

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

        var equipment = new EquipmentDto(request.MoldId, request.Name, properties, request.WorkUnit, "MOLD");
        var url = _urlHelper.GenerateResourceUrl("Equipments");
        await _restClient.PostAsync(url, equipment);
    }
}

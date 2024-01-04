using InjectionMachineModule.Application.Helpers;

namespace InjectionMachineModule.Application.Commands.ManufacturingOrders;

public class CreateManufacturingOrderCommandHandler : IRequestHandler<CreateManufacturingOrderCommand>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;

    public CreateManufacturingOrderCommandHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }

    public async Task Handle(CreateManufacturingOrderCommand request, CancellationToken cancellationToken)
    {
        var manufacturingOrder = new CreateManufacturingOrderDto(request.ManufacturingOrderId, request.MaterialDefinitionId, request.Quantity, request.Unit, request.DueDate, request.AvailableDate, request.Priority);
        var url = _urlHelper.GenerateResourceUrl("ManufacturingOrders");
        await _restClient.PostAsync(url, manufacturingOrder);
    }
}

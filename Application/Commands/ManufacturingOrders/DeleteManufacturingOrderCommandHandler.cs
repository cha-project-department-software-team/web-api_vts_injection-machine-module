using InjectionMachineModule.Application.Helpers;

namespace InjectionMachineModule.Application.Commands.ManufacturingOrders;

public class DeleteManufacturingOrderCommandHandler : IRequestHandler<DeleteManufacturingOrderCommand>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;

    public DeleteManufacturingOrderCommandHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }

    public async Task Handle(DeleteManufacturingOrderCommand request, CancellationToken cancellationToken)
    {
        var url = _urlHelper.GenerateResourceUrl($"ManufacturingOrders/{request.ManufacturingOrderId}");
        await _restClient.DeleteAsync(url);
    }
}

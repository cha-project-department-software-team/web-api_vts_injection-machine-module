using InjectionMachineModule.Application.Helpers;

namespace InjectionMachineModule.Application.Commands.PlasticInjectionMachines;

public class DeletePlasticInjectionMachineCommandHandler : IRequestHandler<DeletePlasticInjectionMachineCommand>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;

    public DeletePlasticInjectionMachineCommandHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }

    public async Task Handle(DeletePlasticInjectionMachineCommand request, CancellationToken cancellationToken)
    {
        var url = _urlHelper.GenerateResourceUrl($"Equipments/{request.EquipmentId}");
        await _restClient.DeleteAsync(url);
    }
}

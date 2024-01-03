using InjectionMachineModule.Application.Helpers;

namespace InjectionMachineModule.Application.Commands.Molds;

public class DeleteMoldCommandHandler : IRequestHandler<DeleteMoldCommand>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;

    public DeleteMoldCommandHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }

    public async Task Handle(DeleteMoldCommand request, CancellationToken cancellationToken)
    {
        var url = _urlHelper.GenerateResourceUrl($"Equipments/{request.MoldId}");
        await _restClient.DeleteAsync(url);
    }
}

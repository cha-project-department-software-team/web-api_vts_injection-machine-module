using InjectionMachineModule.Application.Helpers;

namespace InjectionMachineModule.Application.Commands.PlasticMaterials;

public class DeletePlasticMaterialCommandHandler : IRequestHandler<DeletePlasticMaterialCommand>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;

    public DeletePlasticMaterialCommandHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }

    public async Task Handle(DeletePlasticMaterialCommand request, CancellationToken cancellationToken)
    {
        var url = _urlHelper.GenerateResourceUrl($"MaterialDefinitions/{request.PlasticMaterialId}");
        await _restClient.DeleteAsync(url);
    }
}

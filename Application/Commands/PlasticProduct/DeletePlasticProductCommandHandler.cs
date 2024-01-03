using InjectionMachineModule.Application.Helpers;

namespace InjectionMachineModule.Application.Commands.PlasticProduct;

public class DeletePlasticProductCommandHandler : IRequestHandler<DeletePlasticProductCommand>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;

    public DeletePlasticProductCommandHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }

    public async Task Handle(DeletePlasticProductCommand request, CancellationToken cancellationToken)
    {
        var url = _urlHelper.GenerateResourceUrl($"MaterialDefinitions/{request.PlasticProductId}");
        await _restClient.DeleteAsync(url);
    }
}

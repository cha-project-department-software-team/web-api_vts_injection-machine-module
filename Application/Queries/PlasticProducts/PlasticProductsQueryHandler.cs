using InjectionMachineModule.Application.Helpers;
using InjectionMachineModule.Application.Queries.ManufacturingOrders;
using InjectionMachineModule.Application.Queries.PlasticInjectionMachines;
using InjectionMachineModule.Infrastructure.Communication;
using Newtonsoft.Json;

namespace InjectionMachineModule.Application.Queries.PlasticProducts;

public class PlasticProductsQueryHandler : IRequestHandler<PlasticProductsQuery, QueryResult<PlasticProductViewModel>>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;

    public PlasticProductsQueryHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }

    public async Task<QueryResult<PlasticProductViewModel>> Handle(PlasticProductsQuery request, CancellationToken cancellationToken)
    {
        var url = _urlHelper.GenerateResourceUrl("ManufacturingOrders") + MesApiUrlHelper.GeneratePageQuery(request.IdStartedWith, request.PageIndex, request.PageSize);
        var viewModel = await _restClient.GetAsync<QueryResult<PlasticProductViewModel>>(url);

        if (viewModel is null)
            throw new Exception("Resource not found");
        else
        {
            var items = viewModel.Items;
            var plasticProducts = items.Where(x => x.ModuleType == "InjectionMachine");
            int totalItems = plasticProducts.Count();

            return new QueryResult<PlasticProductViewModel>(plasticProducts, totalItems);
        }
    }
}

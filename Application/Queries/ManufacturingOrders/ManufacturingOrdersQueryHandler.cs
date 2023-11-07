using InjectionMachineModule.Application.Helpers;
using InjectionMachineModule.Infrastructure.Communication;
using Newtonsoft.Json;

namespace InjectionMachineModule.Application.Queries.ManufacturingOrders;

public class ManufacturingOrdersQueryHandler : IRequestHandler<ManufacturingOrdersQuery, QueryResult<ManufacturingOrderViewModel>>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;

    public ManufacturingOrdersQueryHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }

    public async Task<QueryResult<ManufacturingOrderViewModel>> Handle(ManufacturingOrdersQuery request, CancellationToken cancellationToken)
    {
        var url = _urlHelper.GenerateResourceUrl("ManufacturingOrders") + MesApiUrlHelper.GeneratePageQuery(request.IdStartedWith, request.PageIndex, request.PageSize);
        var viewModel = await _restClient.GetAsync<QueryResult<ManufacturingOrderViewModel>>(url);

        if (viewModel is null)
            throw new Exception("Resource not found");
        else
        {
            var items = viewModel.Items;
            var manufacturingOrders = items.Where(x => x.MaterialDefinition.ModuleType == "InjectionMachine");
            int totalItems = manufacturingOrders.Count();

            return new QueryResult<ManufacturingOrderViewModel>(manufacturingOrders, totalItems);
        }
    }
}

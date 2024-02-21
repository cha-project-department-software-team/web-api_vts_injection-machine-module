using AutoMapper;
using InjectionMachineModule.Application.Helpers;

namespace InjectionMachineModule.Application.Queries.ManufacturingOrders;

public class ManufacturingOrdersQueryHandler : IRequestHandler<ManufacturingOrdersQuery, QueryResult<ManufacturingOrderViewModel>>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;
    private readonly IMapper _mapper;

    public ManufacturingOrdersQueryHandler(RestClient restClient, MesApiUrlHelper urlHelper, IMapper mapper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
        _mapper = mapper;
    }

    public async Task<QueryResult<ManufacturingOrderViewModel>> Handle(ManufacturingOrdersQuery request, CancellationToken cancellationToken)
    {
        var url = _urlHelper.GenerateResourceUrl("ManufacturingOrders") + MesApiUrlHelper.GeneratePageQuery(request.IdStartedWith, request.PageIndex, request.PageSize);
        var viewModel = await _restClient.GetAsync<QueryResult<ManufacturingOrderDto>>(url);

        if (viewModel is null)
            throw new HttpRequestException("Resource not found");
        else
        {
            var items = viewModel.Items;
            items = items.Where(x => x.MaterialDefinition.MaterialClass == "InjectionMachine-PlasticProduct");
            var manufacturingOrders = _mapper.Map<IEnumerable<ManufacturingOrderViewModel>>(items);
            int totalItems = manufacturingOrders.Count();

            return new QueryResult<ManufacturingOrderViewModel>(manufacturingOrders, totalItems);
        }
    }
}

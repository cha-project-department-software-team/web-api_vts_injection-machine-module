using AutoMapper;
using InjectionMachineModule.Application.Helpers;

namespace InjectionMachineModule.Application.Queries.PlasticProducts;

public class PlasticProductsQueryHandler : IRequestHandler<PlasticProductsQuery, QueryResult<PlasticProductViewModel>>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;
    private readonly IMapper _mapper;

    public PlasticProductsQueryHandler(RestClient restClient, MesApiUrlHelper urlHelper, IMapper mapper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
        _mapper = mapper;
    }

    public async Task<QueryResult<PlasticProductViewModel>> Handle(PlasticProductsQuery request, CancellationToken cancellationToken)
    {
        var url = _urlHelper.GenerateResourceUrl("MaterialDefinitions") + MesApiUrlHelper.GeneratePageQuery(request.IdStartedWith, request.PageIndex, request.PageSize);
        var viewModel = await _restClient.GetAsync<QueryResult<MaterialDefinitionViewModelDto>>(url);

        if (viewModel is null)
            throw new HttpRequestException("Resource not found");
        else
        {
            var items = viewModel.Items;
            items = items.Where(x => x.MaterialClass == "InjectionMachine-PlasticProduct");
            var plasticProducts = _mapper.Map<IEnumerable<PlasticProductViewModel>>(items);
            int totalItems = plasticProducts.Count();

            return new QueryResult<PlasticProductViewModel>(plasticProducts, totalItems);
        }
    }
}

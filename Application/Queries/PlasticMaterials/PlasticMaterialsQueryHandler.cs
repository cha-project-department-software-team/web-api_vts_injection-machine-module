using AutoMapper;
using InjectionMachineModule.Application.Helpers;
using InjectionMachineModule.Application.Queries.PlasticProducts;

namespace InjectionMachineModule.Application.Queries.PlasticMaterials;

public class PlasticMaterialsQueryHandler : IRequestHandler<PlasticMaterialsQuery, QueryResult<PlasticMaterialViewModel>>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;
    private readonly IMapper _mapper;

    public PlasticMaterialsQueryHandler(RestClient restClient, MesApiUrlHelper urlHelper, IMapper mapper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
        _mapper = mapper;
    }

    public async Task<QueryResult<PlasticMaterialViewModel>> Handle(PlasticMaterialsQuery request, CancellationToken cancellationToken)
    {
        var url = _urlHelper.GenerateResourceUrl("MaterialDefinitions") + MesApiUrlHelper.GeneratePageQuery(request.IdStartedWith, request.PageIndex, request.PageSize);
        var viewModel = await _restClient.GetAsync<QueryResult<MaterialDefinitionViewModelDto>>(url);

        if (viewModel is null)
            throw new HttpRequestException("Resource not found");
        else
        {
            var items = viewModel.Items;
            items = items.Where(x => x.MaterialClass == "InjectionMachine-PlasticMaterial");
            var plasticMaterials = _mapper.Map<IEnumerable<PlasticMaterialViewModel>>(items);
            int totalItems = plasticMaterials.Count();

            return new QueryResult<PlasticMaterialViewModel>(plasticMaterials, totalItems);
        }
    }
}

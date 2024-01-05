using AutoMapper;
using InjectionMachineModule.Application.Helpers;

namespace InjectionMachineModule.Application.Queries.PlasticInjectionMachines;

public class PlasticInjectionMachinesQueryHandler : IRequestHandler<PlasticInjectionMachinesQuery, QueryResult<PlasticInjectionMachineViewModel>>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;
    private readonly IMapper _mapper;

    public PlasticInjectionMachinesQueryHandler(RestClient restClient, MesApiUrlHelper urlHelper, IMapper mapper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
        _mapper = mapper;
    }

    public async Task<QueryResult<PlasticInjectionMachineViewModel>> Handle(PlasticInjectionMachinesQuery request, CancellationToken cancellationToken)
    {
        var url = _urlHelper.GenerateResourceUrl("Equipments") + MesApiUrlHelper.GeneratePageQuery(request.IdStartedWith, request.PageIndex, request.PageSize);
        var viewModel = await _restClient.GetAsync<QueryResult<EquipmentViewModelDto>>(url);

        if (viewModel is null)
        {
            throw new HttpRequestException("Resource not found");
        }
        else
        {
            var items = viewModel.Items;
            items = items.Where(x => x.EquipmentClass == "InjectionMoldingMachine");
            var plasticInjectionMachines = _mapper.Map<IEnumerable<PlasticInjectionMachineViewModel>>(items);
            int totalItems = plasticInjectionMachines.Count();

            return new QueryResult<PlasticInjectionMachineViewModel>(plasticInjectionMachines, totalItems);
        }
    }
}

using AutoMapper;
using InjectionMachineModule.Application.Dtos.Equipments;
using InjectionMachineModule.Application.Helpers;
using InjectionMachineModule.Infrastructure.Communication;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;

namespace InjectionMachineModule.Application.Queries.Molds;

public class MoldsQueryHandler : IRequestHandler<MoldsQuery, QueryResult<MoldViewModel>>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;
    private readonly IMapper _mapper;

    public MoldsQueryHandler(RestClient restClient, MesApiUrlHelper urlHelper, IMapper mapper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
        _mapper = mapper;
    }

    public async Task<QueryResult<MoldViewModel>> Handle(MoldsQuery request, CancellationToken cancellationToken)
    {
        var url = _urlHelper.GenerateResourceUrl("Equipments") + MesApiUrlHelper.GeneratePageQuery(request.IdStartedWith, request.PageIndex, request.PageSize);
        var viewModel = await _restClient.GetAsync<QueryResult<EquipmentViewModelDto>>(url);

        if (viewModel is null)
        {
            throw new HttpRequestException("Resourcce not found");
        }
        else
        {
            var items = viewModel.Items;
            items = items.Where(x => x.EquipmentClass == "Mold");
            var molds = _mapper.Map<IEnumerable<MoldViewModel>>(items);
            int totalItems = molds.Count();

            return new QueryResult<MoldViewModel>(molds, totalItems);
        }
    }
}

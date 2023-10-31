using AutoMapper;
using InjectionMachineModule.Dtos.Equipments;
using Newtonsoft.Json;

namespace InjectionMachineModule.Application.Queries.PlasticInjectionMachines;

public class PlasticInjectionMachinesQueryHandler : IRequestHandler<PlasticInjectionMachinesQuery, QueryResult<PlasticInjectionMachineViewModel>>
{
    public APIUrls APIUrls { get; set; }
    private readonly IMapper _mapper;

    public PlasticInjectionMachinesQueryHandler(IOptions<APIUrls> aPIUrls, IMapper mapper)
    {
        APIUrls = aPIUrls.Value;
        _mapper = mapper;
    }

    public async Task<QueryResult<PlasticInjectionMachineViewModel>> Handle(PlasticInjectionMachinesQuery request, CancellationToken cancellationToken)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                var endpoint = APIEndpoint.GetEndpoint(request.IdStartedWith, request.PageIndex, request.PageSize);
                var response = await httpClient.GetAsync(APIUrls.Equipments + endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var viewModel = JsonConvert.DeserializeObject<QueryResult<EquipmentDto>>(content);

                    if (viewModel is null)
                        throw new Exception("Resourcce not found");
                    else
                    {
                        var items = viewModel.Items;
                        items = items.Where(x => x.EquipmentClass == "IM");
                        var plasticInjectionMachines = _mapper.Map<IEnumerable<PlasticInjectionMachineViewModel>>(items);
                        int totalItems = plasticInjectionMachines.Count();

                        return new QueryResult<PlasticInjectionMachineViewModel>(plasticInjectionMachines, totalItems);
                    }    
                }
                else
                {
                    throw new Exception("API request failed with status code:" + response.StatusCode);
                }
            }

            catch (HttpRequestException ex)
            {
                throw new Exception($"Request exception: {ex.Message}");
            }
        }
    }
}

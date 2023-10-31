using AutoMapper;
using InjectionMachineModule.Dtos.Equipments;
using Newtonsoft.Json;

namespace InjectionMachineModule.Application.Queries.Molds;

public class MoldsQueryHandler : IRequestHandler<MoldsQuery, QueryResult<MoldViewModel>>
{
    public APIUrls APIUrls { get; set; }
    private readonly IMapper _mapper;
    public MoldsQueryHandler(IOptions<APIUrls> aPIUrls, IMapper mapper)
    {
        APIUrls = aPIUrls.Value;
        _mapper = mapper;
    }

    public async Task<QueryResult<MoldViewModel>> Handle(MoldsQuery request, CancellationToken cancellationToken)
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
                        items = items.Where(x => x.EquipmentClass == "MOLD");
                        var molds = _mapper.Map<IEnumerable<MoldViewModel>>(items);
                        int totalItems = molds.Count();

                        return new QueryResult<MoldViewModel>(molds, totalItems);
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

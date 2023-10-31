using Newtonsoft.Json;

namespace InjectionMachineModule.Application.Queries.ManufacturingOrders;

public class ManufacturingOrdersQueryHandler : IRequestHandler<ManufacturingOrdersQuery, QueryResult<ManufacturingOrderViewModel>>
{
    public APIUrls APIUrls { get; set; }

    public ManufacturingOrdersQueryHandler(IOptions<APIUrls> aPIUrls)
    {
        APIUrls = aPIUrls.Value;
    }

    public async Task<QueryResult<ManufacturingOrderViewModel>> Handle(ManufacturingOrdersQuery request, CancellationToken cancellationToken)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                var endpoint = APIEndpoint.GetEndpoint(request.IdStartedWith, request.PageIndex, request.PageSize);
                var response = await httpClient.GetAsync(APIUrls.ManufacturingOrders + endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var viewModel = JsonConvert.DeserializeObject<QueryResult<ManufacturingOrderViewModel>>(content);

                    if (viewModel is null)
                        throw new Exception("Resourcce not found");
                    else
                    {
                        var items = viewModel.Items;
                        var manufacturingOrders = items.Where(x => x.MaterialDefinition.ModuleType == "InjectionMachine");
                        int totalItems = manufacturingOrders.Count();

                        return new QueryResult<ManufacturingOrderViewModel>(manufacturingOrders, totalItems);
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

using InjectionMachineModule.Application.Queries.PlasticInjectionMachines;
using Newtonsoft.Json;

namespace InjectionMachineModule.Application.Queries.PlasticProducts;

public class PlasticProductsQueryHandler : IRequestHandler<PlasticProductsQuery, QueryResult<PlasticProductViewModel>>
{
    public APIUrls APIUrls { get; set; }

    public PlasticProductsQueryHandler(IOptions<APIUrls> aPIUrls)
    {
        APIUrls = aPIUrls.Value;
    }

    public async Task<QueryResult<PlasticProductViewModel>> Handle(PlasticProductsQuery request, CancellationToken cancellationToken)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                var endpoint = APIEndpoint.GetEndpoint(request.IdStartedWith, request.PageIndex, request.PageSize);
                var response = await httpClient.GetAsync(APIUrls.MaterialDefinitions + endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var viewModel = JsonConvert.DeserializeObject<QueryResult<PlasticProductViewModel>>(content);

                    if (viewModel is null)
                        throw new Exception("Resourcce not found");
                    else
                    {
                        var items = viewModel.Items;
                        var plasticProducts = items.Where(x => x.ModuleType == "InjectionMachine");
                        int totalItems = plasticProducts.Count();

                        return new QueryResult<PlasticProductViewModel>(plasticProducts, totalItems);
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

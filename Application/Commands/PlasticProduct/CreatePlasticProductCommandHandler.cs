using InjectionMachineModule.Dtos.MaterialDefinitions;
using Newtonsoft.Json;
using System.Text;

namespace InjectionMachineModule.Application.Commands.PlasticProduct;

public class CreatePlasticProductCommandHandler : IRequestHandler<CreatePlasticProductCommand, HttpResponseMessage>
{
    public APIUrls APIUrls { get; set; }

    public CreatePlasticProductCommandHandler(IOptions<APIUrls> aPIUrls)
    {
        APIUrls = aPIUrls.Value;
    }

    public async Task<HttpResponseMessage> Handle(CreatePlasticProductCommand request, CancellationToken cancellationToken)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                var properties = request.Properties.Select(x => new PropertyDto(
                    x.PropertyId,
                    new PropertyType(x.PropertyId).Description,
                    x.ValueString,
                    new PropertyType(x.PropertyId).ValueType,
                    x.ValueUnitOfMeasure))
                    .ToList();

                var materialDefinition = new MaterialDefinitionDto(request.MaterialDefinitionId, request.Name, request.PrimaryUnit, request.ModuleType, properties);
                string json = JsonConvert.SerializeObject(materialDefinition);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(APIUrls.MaterialDefinitions, content);
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }

                var operation = new OperationDto();
                string jsonOperation = JsonConvert.SerializeObject(operation);
                var contentOperation = new StringContent(jsonOperation, Encoding.UTF8, "application/json");
                return await httpClient.PostAsync(APIUrls.MaterialDefinitions + $"/{request.MaterialDefinitionId}/operations", contentOperation);
            }

            catch (HttpRequestException ex)
            {
                throw new Exception($"Request exception: {ex.Message}");
            }
        }
    }
}

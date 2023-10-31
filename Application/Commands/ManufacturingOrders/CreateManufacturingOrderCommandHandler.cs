using Newtonsoft.Json;
using System.Text;

namespace InjectionMachineModule.Application.Commands.ManufacturingOrders;

public class CreateManufacturingOrderCommandHandler : IRequestHandler<CreateManufacturingOrderCommand, HttpResponseMessage>
{
    public APIUrls APIUrls { get; set; }

    public CreateManufacturingOrderCommandHandler(IOptions<APIUrls> aPIUrls)
    {
        APIUrls = aPIUrls.Value;
    }

    public async Task<HttpResponseMessage> Handle(CreateManufacturingOrderCommand request, CancellationToken cancellationToken)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                var manufacturingOrder = new ManufacturingOrderDto(request.ManufacturingOrderId, request.MaterialDefinitionId, request.Quantity, request.Unit, request.DueDate);
                
                string json = JsonConvert.SerializeObject(manufacturingOrder);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return await httpClient.PostAsync(APIUrls.ManufacturingOrders, content);
            }

            catch (HttpRequestException ex)
            {
                throw new Exception($"Request exception: {ex.Message}");
            }
        }
    }
}

using Newtonsoft.Json;
using System.Text;

namespace InjectionMachineModule.Application.Commands.Molds;
public class CreateMoldCommandHandler : IRequestHandler<CreateMoldCommand, HttpResponseMessage>
{
    public APIUrls APIUrls { get; set; }

    public CreateMoldCommandHandler(IOptions<APIUrls> aPIUrls)
    {
        APIUrls = aPIUrls.Value;
    }

    public async Task<HttpResponseMessage> Handle(CreateMoldCommand request,  CancellationToken cancellationToken)
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

                var equipment = new EquipmentDto(request.MoldId, request.Name, properties, request.WorkUnit, "MOLD");

                string json = JsonConvert.SerializeObject(equipment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return await httpClient.PostAsync(APIUrls.Equipments, content);
            }

            catch (HttpRequestException ex)
            {
                throw new Exception($"Request exception: {ex.Message}");
            }
        }
    }
}

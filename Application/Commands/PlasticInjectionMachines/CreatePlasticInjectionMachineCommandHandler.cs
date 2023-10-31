using Newtonsoft.Json;
using System.Text;

namespace InjectionMachineModule.Application.Commands.PlasticInjectionMachines;

public class CreatePlasticInjectionMachineCommandHandler : IRequestHandler<CreatePlasticInjectionMachineCommand, HttpResponseMessage>
{
    public APIUrls APIUrls { get; set; }

    public CreatePlasticInjectionMachineCommandHandler(IOptions<APIUrls> aPIUrls)
    {
        APIUrls = aPIUrls.Value;
    }

    public async Task<HttpResponseMessage> Handle(CreatePlasticInjectionMachineCommand request, CancellationToken cancellationToken)
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

                var equipment = new EquipmentDto(request.EquipmentId, request.Name, properties, request.WorkUnit, "IM");

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

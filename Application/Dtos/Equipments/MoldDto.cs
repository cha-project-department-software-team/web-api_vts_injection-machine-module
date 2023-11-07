using InjectionMachineModule.Application.Dtos;

namespace InjectionMachineModule.Application.Dtos.Equipments;

public class MoldDto
{
    public string MoldId { get; set; }
    public string MoldName { get; set; }
    public List<PropertyDto> Properties { get; set; }
    public List<EquipmentDto> Machines { get; set; }

    public MoldDto(string moldId, string moldName, List<PropertyDto> properties, List<EquipmentDto> machines)
    {
        MoldId = moldId;
        MoldName = moldName;
        Properties = properties;
        Machines = machines;
    }
}

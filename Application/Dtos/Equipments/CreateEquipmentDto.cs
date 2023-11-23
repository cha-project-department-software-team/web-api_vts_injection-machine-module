namespace InjectionMachineModule.Application.Dtos.Equipments;

public class CreateEquipmentDto
{
    public string EquipmentId { get; set; }
    public string Name { get; set; }
    public List<PropertyDto> Properties { get; set; }
    public string WorkUnit { get; set; }
    public string EquipmentClass { get; set; }

    public CreateEquipmentDto(string equipmentId, string name, List<PropertyDto> properties, string workUnit, string equipmentClass)
    {
        EquipmentId = equipmentId;
        Name = name;
        Properties = properties;
        WorkUnit = workUnit;
        EquipmentClass = equipmentClass;
    }
}

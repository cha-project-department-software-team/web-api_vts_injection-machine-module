namespace InjectionMachineModule.Dtos.Equipments;

public class EquipmentDto
{
    public string EquipmentId { get; set; }
    public string Name { get; set; }
    public List<PropertyDto> Properties { get; set; }
    public string WorkUnit { get; set; }
    public string EquipmentClass { get; set; }

    public EquipmentDto(string equipmentId, string name, List<PropertyDto> properties, string workUnit, string equipmentClass)
    {
        EquipmentId = equipmentId;
        Name = name;
        Properties = properties;
        WorkUnit = workUnit;
        EquipmentClass = equipmentClass;
    }
}

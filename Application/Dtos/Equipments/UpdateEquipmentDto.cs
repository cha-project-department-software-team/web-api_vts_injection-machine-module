namespace InjectionMachineModule.Application.Dtos.Equipments;

public class UpdateEquipmentDto
{
    public string Name { get; set; }
    public List<PropertyDto> Properties { get; set; }
    public string WorkUnit { get; set; }
    public string EquipmentClass { get; set; }

    public UpdateEquipmentDto(string name, List<PropertyDto> properties, string workUnit, string equipmentClass )
    {
        Name = name;
        Properties = properties;
        WorkUnit = workUnit;
        EquipmentClass = equipmentClass;
    }
}

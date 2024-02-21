namespace InjectionMachineModule.Application.Dtos.Equipments;

public class CreateEquipmentDto
{
    public string EquipmentId { get; set; }
    public string Name { get; set; }
    public List<PropertyDto> Properties { get; set; }
    public string EquipmentClass { get; set; }
    public string AbsolutePath { get; set; }
    public CreateEquipmentDto(string equipmentId, string name, List<PropertyDto> properties, string equipmentClass, string absolutePath)
    {
        EquipmentId = equipmentId;
        Name = name;
        Properties = properties;
        AbsolutePath = absolutePath;
        EquipmentClass = equipmentClass;
    }
}

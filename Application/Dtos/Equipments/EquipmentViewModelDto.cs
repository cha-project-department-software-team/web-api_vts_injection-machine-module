namespace InjectionMachineModule.Application.Dtos.Equipments;

public class EquipmentViewModelDto
{
    public string EquipmentId { get; set; }
    public string Name { get; set; }
    public List<PropertyDto> Properties { get; set; }
    public string EquipmentClass { get; set; }
    public string AbsolutePath { get; set; }

    public EquipmentViewModelDto(string equipmentId, string name, List<PropertyDto> properties, string equipmentClass, string absolutePath)
    {
        EquipmentId = equipmentId;
        Name = name;
        Properties = properties;
        EquipmentClass = equipmentClass;
        AbsolutePath = absolutePath;
    }
}

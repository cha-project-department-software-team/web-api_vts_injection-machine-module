namespace InjectionMachineModule.Application.Dtos.Equipments;

public class UpdateEquipmentDto
{
    public string Name { get; set; }
    public List<PropertyDto> Properties { get; set; }
    public string EquipmentClass { get; set; }
    public string AbsolutePath { get; set; }

    public UpdateEquipmentDto(string name, List<PropertyDto> properties, string equipmentClass, string absolutePath)
    {
        Name = name;
        Properties = properties;
        AbsolutePath = absolutePath;
        EquipmentClass = equipmentClass;
    }
}

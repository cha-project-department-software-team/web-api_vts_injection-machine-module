namespace InjectionMachineModule.Dtos.MaterialDefinitions;

public class MaterialDefinitionDto
{
    public string MaterialDefinitionId { get; set; }
    public string Name { get; set; }
    public string PrimaryUnit { get; set; }
    public string ModuleType { get; set; }
    public List<PropertyDto> Properties { get; set; }

    public MaterialDefinitionDto(string materialDefinitionId, string name, string primaryUnit, string moduleType, List<PropertyDto> properties)
    {
        MaterialDefinitionId = materialDefinitionId;
        Name = name;
        PrimaryUnit = primaryUnit;
        ModuleType = moduleType;
        Properties = properties;
    }
}

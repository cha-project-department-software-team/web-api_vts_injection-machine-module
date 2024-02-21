namespace InjectionMachineModule.Application.Dtos.MaterialDefinitions;

public class CreateMaterialDefinitionDto
{
    public string MaterialDefinitionId { get; set; }
    public string Name { get; set; }
    public string PrimaryUnit { get; set; }
    public List<PropertyDto> Properties { get; set; }
    public string MaterialClass { get; set; }

    public CreateMaterialDefinitionDto(string materialDefinitionId, string name, string primaryUnit, List<PropertyDto> properties, string materialClass)
    {
        MaterialDefinitionId = materialDefinitionId;
        Name = name;
        PrimaryUnit = primaryUnit;
        Properties = properties;
        MaterialClass = materialClass;
    }
}

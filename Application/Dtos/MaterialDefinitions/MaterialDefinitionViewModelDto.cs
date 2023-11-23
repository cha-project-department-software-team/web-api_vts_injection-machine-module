namespace InjectionMachineModule.Application.Dtos.MaterialDefinitions;

public class MaterialDefinitionViewModelDto
{
    public string MaterialDefinitionId { get; set; }
    public string Name { get; set; }
    public string PrimaryUnit { get; set; }
    public string ModuleType { get; set; }
    public List<PropertyDto> Properties { get; set; }
    public List<MaterialUnitViewModelDto> SecondaryUnits { get; set; }
    public List<OperationViewModelDto> Operations { get; set; }

    public MaterialDefinitionViewModelDto(string materialDefinitionId, string name, string primaryUnit, string moduleType, List<PropertyDto> properties, List<MaterialUnitViewModelDto> secondaryUnits, List<OperationViewModelDto> operations)
    {
        MaterialDefinitionId = materialDefinitionId;
        Name = name;
        PrimaryUnit = primaryUnit;
        ModuleType = moduleType;
        Properties = properties;
        SecondaryUnits = secondaryUnits;
        Operations = operations;
    }
}

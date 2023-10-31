namespace InjectionMachineModule.Application.Queries.PlasticProducts;

public class PlasticProductViewModel
{
    public string MaterialDefinitionId { get; set; }
    public string Name { get; set; }
    public string PrimaryUnit { get; set; }
    public string ModuleType { get; set; }
    public List<PropertyViewModel> Properties { get; set; }
    public List<MaterialUnitViewModel> SecondaryUnits { get; set; }
    public List<OperationViewModel> Operations { get; set; }

    public PlasticProductViewModel(string materialDefinitionId, string name, string primaryUnit, string moduleType, List<PropertyViewModel> properties, List<MaterialUnitViewModel> secondaryUnits, List<OperationViewModel> operations)
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

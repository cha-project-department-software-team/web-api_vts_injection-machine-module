using InjectionMachineModule.Application.Queries.PlasticProducts;

namespace InjectionMachineModule.Application.Queries.PlasticMaterials;

public class PlasticMaterialViewModel
{
    public string PlasticMaterialId { get; set; }
    public string Name { get; set; }
    public string PrimaryUnit { get; set; }
    public List<PropertyViewModel> Properties { get; set; }
    public List<MaterialUnitViewModel> SecondaryUnits { get; set; }
    public List<OperationViewModel> Operations { get; set; }
    public string MaterialClass { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private PlasticMaterialViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public PlasticMaterialViewModel(string plasticMaterialId, string name, string primaryUnit, List<PropertyViewModel> properties, List<MaterialUnitViewModel> secondaryUnits, List<OperationViewModel> operations, string materialClass)
    {
        PlasticMaterialId = plasticMaterialId;
        Name = name;
        PrimaryUnit = primaryUnit;
        Properties = properties;
        SecondaryUnits = secondaryUnits;
        Operations = operations;
        MaterialClass = materialClass;
    }
}

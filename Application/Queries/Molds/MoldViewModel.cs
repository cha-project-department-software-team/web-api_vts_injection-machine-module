namespace InjectionMachineModule.Application.Queries.Molds;

public class MoldViewModel
{
    public string MoldId { get; set; }
    public string Name { get; set; }
    public List<PropertyViewModel> Properties { get; set; }
    public string WorkUnit { get; set; }
    public string EquipmentClass { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private MoldViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public MoldViewModel(string moldId, string name, List<PropertyViewModel> properties, string workUnit, string equipmentClass)
    {
        MoldId = moldId;
        Name = name;
        Properties = properties;
        WorkUnit = workUnit;
        EquipmentClass = equipmentClass;
    }
}

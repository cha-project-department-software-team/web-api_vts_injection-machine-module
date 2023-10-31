namespace InjectionMachineModule.Application.Queries.PlasticInjectionMachines;

public class PlasticInjectionMachineViewModel
{
    public string EquipmentId { get; set; }
    public string Name { get; set; }
    public List<PropertyViewModel> Properties { get; set; }
    public string WorkUnit { get; set; }
    public string EquipmentClass { get; set; }

    public PlasticInjectionMachineViewModel(string equipmentId, string name, List<PropertyViewModel> properties, string workUnit, string equipmentClass)
    {
        EquipmentId = equipmentId;
        Name = name;
        Properties = properties;
        WorkUnit = workUnit;
        EquipmentClass = equipmentClass;
    }
}

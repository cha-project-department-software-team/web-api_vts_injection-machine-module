namespace InjectionMachineModule.Application.Queries.PlasticInjectionMachines;

public class PlasticInjectionMachineViewModel
{
    public string EquipmentId { get; set; }
    public string Name { get; set; }
    public List<PropertyViewModel> Properties { get; set; }
    public string EquipmentClass { get; set; }
    public string AbsolutePath { get; set; }

    public PlasticInjectionMachineViewModel(string equipmentId, string name, List<PropertyViewModel> properties, string equipmentClass, string absolutePath)
    {
        EquipmentId = equipmentId;
        Name = name;
        Properties = properties;
        EquipmentClass = equipmentClass;
        AbsolutePath = absolutePath;
    }
}

namespace InjectionMachineModule.Application.Commands.PlasticInjectionMachines;

public class UpdatePlasticInjectionMachineCommand : IRequest
{
    public string EquipmentId { get; set; }
    public string Name { get; set; }
    public List<SavePropertyViewModel> Properties { get; set; }
    public List<string> Molds { get; set; }
    public string WorkUnit { get; set; }

    public UpdatePlasticInjectionMachineCommand(string equipmentId, string name, List<SavePropertyViewModel> properties, List<string> molds, string workUnit)
    {
        EquipmentId = equipmentId;
        Name = name;
        Properties = properties;
        Molds = molds;
        WorkUnit = workUnit;
    }
}

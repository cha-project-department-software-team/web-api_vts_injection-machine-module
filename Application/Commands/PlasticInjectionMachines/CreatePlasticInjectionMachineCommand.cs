using System.Runtime.Serialization;

namespace InjectionMachineModule.Application.Commands.PlasticInjectionMachines;

[DataContract]
public class CreatePlasticInjectionMachineCommand : IRequest
{
    [DataMember]
    public string EquipmentId { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public List<SavePlasticInjectionPropertyViewModel> Properties { get; set; }
    [DataMember]
    public List<string> Molds { get; set; }
    [DataMember]
    public string WorkUnit { get; set; }

    public CreatePlasticInjectionMachineCommand(string equipmentId, string name, List<SavePlasticInjectionPropertyViewModel> properties, List<string> molds, string workUnit)
    {
        EquipmentId = equipmentId;
        Name = name;
        Properties = properties;
        Molds = molds;
        WorkUnit = workUnit;
    }
}

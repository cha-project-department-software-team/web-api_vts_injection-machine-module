using System.Runtime.Serialization;

namespace InjectionMachineModule.Application.Commands.Molds;
[DataContract]
public class CreateMoldCommand : IRequest
{
    [DataMember]
    public string MoldId { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public List<SavePropertyViewModel> Properties { get; set; }
    [DataMember]
    public List<string> PlasticInjectionMachines { get; set; }
    [DataMember]
    public string WorkUnit { get; set; }

    public CreateMoldCommand(string moldId, string name, List<SavePropertyViewModel> properties, List<string> plasticInjectionMachines, string workUnit)
    {
        MoldId = moldId;
        Name = name;
        Properties = properties;
        PlasticInjectionMachines = plasticInjectionMachines;
        WorkUnit = workUnit;
    }
}

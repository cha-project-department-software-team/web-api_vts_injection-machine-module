using System.Runtime.Serialization;

namespace InjectionMachineModule.Application.Commands.PlasticInjectionMachines;
[DataContract]
public class UpdatePlasticInjectionMachineViewModel
{
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public List<SavePropertyViewModel> Properties { get; set; }
    [DataMember]
    public List<string> Molds { get; set; }
    [DataMember]
    public string WorkUnit { get; set; }

    public UpdatePlasticInjectionMachineViewModel(string name, List<SavePropertyViewModel> properties, List<string> molds, string workUnit)
    {
        Name = name;
        Properties = properties;
        Molds = molds;
        WorkUnit = workUnit;
    }
}

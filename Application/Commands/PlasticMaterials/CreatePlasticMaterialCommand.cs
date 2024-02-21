using System.Runtime.Serialization;

namespace InjectionMachineModule.Application.Commands.PlasticMaterials;

public class CreatePlasticMaterialCommand : IRequest
{
    [DataMember]
    public string PlasticMaterialId { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public string PrimaryUnit { get; set; }
    [DataMember]
    public List<SavePropertyViewModel> Properties { get; set; }

    public CreatePlasticMaterialCommand(string plasticMaterialId, string name, string primaryUnit, List<SavePropertyViewModel> properties)
    {
        PlasticMaterialId = plasticMaterialId;
        Name = name;
        PrimaryUnit = primaryUnit;
        Properties = properties;
    }
}

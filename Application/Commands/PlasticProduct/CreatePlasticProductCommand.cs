using System.Runtime.Serialization;

namespace InjectionMachineModule.Application.Commands.PlasticProduct;

[DataContract]
public class CreatePlasticProductCommand : IRequest
{
    [DataMember]
    public string PlasticProductId { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public string PrimaryUnit { get; set; }
    [DataMember]
    public List<SavePropertyViewModel> Properties { get; set; }
    [DataMember]
    public List<string> Molds { get; set; }
    [DataMember]
    public List<string> PlasticMaterials { get; set; }

    public CreatePlasticProductCommand(string plasticProductId, string name, string primaryUnit, List<SavePropertyViewModel> properties, List<string> molds, List<string> plasticMaterials)
    {
        PlasticProductId = plasticProductId;
        Name = name;
        PrimaryUnit = primaryUnit;
        Properties = properties;
        Molds = molds;
        PlasticMaterials = plasticMaterials;
    }
}

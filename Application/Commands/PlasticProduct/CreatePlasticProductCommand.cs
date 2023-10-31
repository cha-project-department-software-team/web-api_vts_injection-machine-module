using System.Runtime.Serialization;

namespace InjectionMachineModule.Application.Commands.PlasticProduct;

[DataContract]
public class CreatePlasticProductCommand : IRequest<HttpResponseMessage>
{
    [DataMember]
    public string MaterialDefinitionId { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public string PrimaryUnit { get; set; }
    [DataMember]
    public string ModuleType { get; set; }
    [DataMember]
    public List<SavePlasticProductPropertyViewModel> Properties { get; set; }

    public CreatePlasticProductCommand(string materialDefinitionId, string name, string primaryUnit, string moduleType, List<SavePlasticProductPropertyViewModel> properties)
    {
        MaterialDefinitionId = materialDefinitionId;
        Name = name;
        PrimaryUnit = primaryUnit;
        ModuleType = moduleType;
        Properties = properties;
    }
}

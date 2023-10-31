using System.Runtime.Serialization;

namespace InjectionMachineModule.Application.Commands.PlasticProduct;

[DataContract]
public class SavePlasticProductPropertyViewModel
{
    [DataMember]
    public string PropertyId { get; set; }
    [DataMember]
    public string ValueString { get; set; }
    [DataMember]
    public string ValueUnitOfMeasure { get; set; }

    public SavePlasticProductPropertyViewModel(string propertyId, string valueString, string valueUnitOfMeasure)
    {
        PropertyId = propertyId;
        ValueString = valueString;
        ValueUnitOfMeasure = valueUnitOfMeasure;
    }
}

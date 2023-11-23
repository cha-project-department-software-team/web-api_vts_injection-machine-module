using System.Runtime.Serialization;

namespace InjectionMachineModule.Application.Commands;

[DataContract]
public class SavePropertyViewModel
{
    [DataMember]
    public string PropertyId { get; set; }
    [DataMember]
    public string ValueString { get; set; }
    [DataMember]
    public string ValueUnitOfMeasure { get; set; }

    public SavePropertyViewModel(string propertyId, string valueString, string valueUnitOfMeasure)
    {
        PropertyId = propertyId;
        ValueString = valueString;
        ValueUnitOfMeasure = valueUnitOfMeasure;
    }
}

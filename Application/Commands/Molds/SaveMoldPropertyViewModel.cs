using System.Runtime.Serialization;

namespace InjectionMachineModule.Application.Commands.Molds;
[DataContract]
public class SaveMoldPropertyViewModel
{
    [DataMember]
    public string PropertyId { get; set; }
    [DataMember]
    public string ValueString { get; set; }
    [DataMember]
    public string ValueUnitOfMeasure { get; set; }

    public SaveMoldPropertyViewModel(string propertyId, string valueString, string valueUnitOfMeasure)
    {
        PropertyId = propertyId;
        ValueString = valueString;
        ValueUnitOfMeasure = valueUnitOfMeasure;
    }
}

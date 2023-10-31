using System.Runtime.Serialization;

namespace InjectionMachineModule.Application.Commands.PlasticInjectionMachines;

[DataContract]
public class SavePlasticInjectionPropertyViewModel
{
    [DataMember]
    public string PropertyId { get; set; }
    [DataMember]
    public string ValueString { get; set; }
    [DataMember]
    public string ValueUnitOfMeasure { get; set; }

    public SavePlasticInjectionPropertyViewModel(string propertyId, string valueString, string valueUnitOfMeasure)
    {
        PropertyId = propertyId;
        ValueString = valueString;
        ValueUnitOfMeasure = valueUnitOfMeasure;
    }
}

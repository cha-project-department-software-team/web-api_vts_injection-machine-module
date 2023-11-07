namespace InjectionMachineModule.Application.Dtos;

public class PropertyDto
{
    public string PropertyId { get; set; }
    public string Description { get; set; }
    public string ValueString { get; set; }
    public EValueType ValueType { get; set; }
    public string ValueUnitOfMeasure { get; set; }

    public PropertyDto(string propertyId, string description, string valueString, EValueType valueType, string valueUnitOfMeasure)
    {
        PropertyId = propertyId;
        Description = description;
        ValueString = valueString;
        ValueType = valueType;
        ValueUnitOfMeasure = valueUnitOfMeasure;
    }
}

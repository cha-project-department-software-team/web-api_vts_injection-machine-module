using InjectionMachineModule.Application.Dtos;

namespace InjectionMachineModule.Application.Queries;

public class PropertyViewModel
{
    public string PropertyId { get; set; }
    public string Description { get; set; }
    public string ValueString { get; set; }
    public EValueType ValueType { get; set; }
    public string ValueUnitOfMeasure { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private PropertyViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public PropertyViewModel(string propertyId, string description, string valueString, EValueType valueType, string valueUnitOfMeasure)
    {
        PropertyId = propertyId;
        Description = description;
        ValueString = valueString;
        ValueType = valueType;
        ValueUnitOfMeasure = valueUnitOfMeasure;
    }
}

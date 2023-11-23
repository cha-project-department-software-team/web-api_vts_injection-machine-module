namespace InjectionMachineModule.Application.Dtos;

public class PropertyType
{
    public string Description { get; set; }
    public EValueType ValueType { get; set; }

    public PropertyType(string propertyId)
    {
        if (propertyId == "MaxPow")
        {
            Description = "Maximum Power";
            ValueType = EValueType.Decimal;
        }
        else
        {
            throw new Exception("You type wrong PropertyId");
        }
    }
}

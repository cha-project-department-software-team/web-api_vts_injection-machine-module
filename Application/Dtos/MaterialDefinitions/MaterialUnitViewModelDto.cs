namespace InjectionMachineModule.Application.Dtos.MaterialDefinitions;

public class MaterialUnitViewModelDto
{
    public string UnitId { get; set; }
    public string UnitName { get; set; }
    public decimal ConversionValueToPrimaryUnit { get; set; }

    public MaterialUnitViewModelDto(string unitId, string unitName, decimal conversionValueToPrimaryUnit)
    {
        UnitId = unitId;
        UnitName = unitName;
        ConversionValueToPrimaryUnit = conversionValueToPrimaryUnit;
    }
}

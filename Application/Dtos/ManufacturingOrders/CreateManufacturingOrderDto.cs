namespace InjectionMachineModule.Application.Dtos.ManufacturingOrders;

public class CreateManufacturingOrderDto
{
    public string ManufacturingOrderId { get; set; }
    public string MaterialDefinitionId { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public DateTime DueDate { get; set; }

    public CreateManufacturingOrderDto(string manufacturingOrderId, string materialDefinitionId, decimal quantity, string unit, DateTime dueDate)
    {
        ManufacturingOrderId = manufacturingOrderId;
        MaterialDefinitionId = materialDefinitionId;
        Quantity = quantity;
        Unit = unit;
        DueDate = dueDate;
    }
}

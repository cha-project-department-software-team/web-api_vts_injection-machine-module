namespace InjectionMachineModule.Application.Dtos.ManufacturingOrders;

public class ManufacturingOrderDto
{
    public string ManufacturingOrderId { get; set; }
    public MaterialDefinitionViewModelDto MaterialDefinition { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime AvailableDate { get; set; }
    public List<string> WorkOrders { get; set; }
    public int Priority { get; set; }

    public ManufacturingOrderDto(string manufacturingOrderId, MaterialDefinitionViewModelDto materialDefinition, decimal quantity, string unit, DateTime dueDate, DateTime availableDate, List<string> workOrders, int priority)
    {
        ManufacturingOrderId = manufacturingOrderId;
        MaterialDefinition = materialDefinition;
        Quantity = quantity;
        Unit = unit;
        DueDate = dueDate;
        AvailableDate = availableDate;
        WorkOrders = workOrders;
        Priority = priority;
    }
}

namespace InjectionMachineModule.Application.Dtos.ManufacturingOrders;

public class ManufacturingOrderViewModelDto
{
    public string ManufacturingOrderId { get; set; }
    public MaterialDefinitionViewModelDto MaterialDefinition { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public DateTime DueDate { get; set; }
    public List<string> WorkOrders { get; set; }

    public ManufacturingOrderViewModelDto(string manufacturingOrderId, MaterialDefinitionViewModelDto materialDefinition, decimal quantity, string unit, DateTime dueDate, List<string> workOrders)
    {
        ManufacturingOrderId = manufacturingOrderId;
        MaterialDefinition = materialDefinition;
        Quantity = quantity;
        Unit = unit;
        DueDate = dueDate;
        WorkOrders = workOrders;
    }
}

using InjectionMachineModule.Application.Queries.PlasticProducts;

namespace InjectionMachineModule.Application.Queries.ManufacturingOrders;

public class ManufacturingOrderViewModel
{
    public string ManufacturingOrderId { get; set; }
    public PlasticProductViewModel MaterialDefinition { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public DateTime DueDate { get; set; }
    public List<string> WorkOrders { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ManufacturingOrderViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    
    public ManufacturingOrderViewModel(string manufacturingOrderId, PlasticProductViewModel materialDefinition, decimal quantity, string unit, DateTime dueDate, List<string> workOrders)
    {
        ManufacturingOrderId = manufacturingOrderId;
        MaterialDefinition = materialDefinition;
        Quantity = quantity;
        Unit = unit;
        DueDate = dueDate;
        WorkOrders = workOrders;
    }
}

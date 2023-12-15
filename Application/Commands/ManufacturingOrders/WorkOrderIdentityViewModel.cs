namespace InjectionMachineModule.Application.Commands.ManufacturingOrders;

public class WorkOrderIdentityViewModel
{
    public string ManufacturingOrderId { get; set; }
    public string WorkOrderId { get; set; }

    public WorkOrderIdentityViewModel(string manufacturingOrderId, string workOrderId)
    {
        ManufacturingOrderId = manufacturingOrderId;
        WorkOrderId = workOrderId;
    }
}

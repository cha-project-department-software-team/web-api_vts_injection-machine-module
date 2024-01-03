namespace InjectionMachineModule.Application.Commands.ManufacturingOrders;

public class DeleteManufacturingOrderCommand : IRequest
{
    public string ManufacturingOrderId { get; set; }

    public DeleteManufacturingOrderCommand(string manufacturingOrderId)
    {
        ManufacturingOrderId = manufacturingOrderId;
    }
}

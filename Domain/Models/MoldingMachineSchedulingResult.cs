namespace InjectionMachineModule.Domain.Models;

public class MoldingMachineSchedulingResult
{
    public List<WorkOrder> WorkOrders { get; set; }

    public MoldingMachineSchedulingResult(List<WorkOrder> workOrders)
    {
        WorkOrders = workOrders;
    }
}
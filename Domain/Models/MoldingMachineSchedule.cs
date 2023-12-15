namespace InjectionMachineModule.Domain.Models;

public class MoldingMachineSchedule
{
    public MoldingMachine MoldingMachine { get; set; }
    public List<WorkOrder> WorkOrders { get; set; } = new();

    public MoldingMachineSchedule(MoldingMachine moldingMachine)
    {
        MoldingMachine = moldingMachine;
    }
}

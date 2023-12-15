namespace InjectionMachineModule.Domain.Models;
public class MoldSchedule
{
    public Mold Mold { get; set; }
    public List<WorkOrder> WorkOrders { get; set; } = new();

    public MoldSchedule(Mold mold)
    {
        Mold = mold;
    }
}
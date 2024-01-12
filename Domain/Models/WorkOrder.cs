namespace InjectionMachineModule.Domain.Models;

public class WorkOrder
{
    public string ManufacturingOrderId { get; set; }
    public string Id { get; set; }
    public double Priority { get; set; }
    public double Quantity { get; set; }
    public TimeSpan? Duration => Mold is null ? null : TimeSpan.FromSeconds(Quantity * Mold.Cycle.TotalSeconds);
    public List<Mold> AvailableMolds { get; set; }
    public Mold? Mold { get; set; }
    public List<MoldingMachine> AvailableMachines { get; set; }
    public MoldingMachine? MoldingMachine { get; set; }
    public DateTime AvailableTime { get; set; }
    public DateTime DueTime { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime
    {
        get
        {
            if (StartTime is null)
            {
                return null;
            }
            var endTime = StartTime.Value.Add(Duration!.Value);
            var startTime = StartTime.Value;

            DateTime closestSunday = startTime.Date.AddDays(DayOfWeek.Sunday - startTime.DayOfWeek).Date;

            while (endTime > closestSunday)
            {
                endTime = endTime.AddDays(1);
                closestSunday = closestSunday.AddDays(7);
            }

            return endTime;
        }
    }
    public TimeSpan? Lateness => (EndTime!.Value - DueTime) < TimeSpan.Zero ? TimeSpan.Zero : EndTime!.Value - DueTime;

    public WorkOrder(string manufacturingOrderId, string id, double priority, double quantity, List<Mold> availableMolds, Mold? mold, List<MoldingMachine> availableMachines, MoldingMachine? moldingMachine, DateTime availableTime, DateTime dueTime, DateTime? startTime)
    {
        ManufacturingOrderId = manufacturingOrderId;
        Id = id;
        Priority = priority;
        Quantity = quantity;
        AvailableMolds = availableMolds;
        Mold = mold;
        AvailableMachines = availableMachines;
        MoldingMachine = moldingMachine;
        AvailableTime = availableTime;
        DueTime = dueTime;
        StartTime = startTime;
    }

    public static bool CheckConflict(DateTime startTime1, DateTime endTime1, DateTime startTime2, DateTime endTime2)
    {
        if (startTime1 > startTime2 && startTime1 < endTime2)
        {
            return true;
        }
        if (endTime1 > startTime2 && endTime1 < endTime2)
        {
            return true;
        }
        if (startTime2 > startTime1 && startTime2 < endTime1)
        {
            return true;
        }
        if (endTime2 > startTime1 && endTime2 < endTime1)
        {
            return true;
        }

        return false;
    }
}

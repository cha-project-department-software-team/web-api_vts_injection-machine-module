namespace InjectionMachineModule.Application.Dtos.WorkOrders;

public class WorkOrderDto
{
    public string ManufacturingOrder { get; set; }
    public string WorkOrderId { get; set; }
    public DateTime DueDate { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public DateTime? ActuallyStartTime { get; set; }
    public DateTime? ActuallyEndTime { get; set; }
    public List<string> PrerequisiteOperations { get; set; }
    public string? WorkCenter { get; set; }
    public EWorkOrderStatus WorkOrderStatus { get; set; }

    public WorkOrderDto(string manufacturingOrder, string workOrderId, DateTime dueDate, TimeSpan duration, DateTime? startTime, DateTime? endTime, DateTime? actuallyStartTime, DateTime? actuallyEndTime, List<string> prerequisiteOperations, string? workCenter, EWorkOrderStatus workOrderStatus)
    {
        ManufacturingOrder = manufacturingOrder;
        WorkOrderId = workOrderId;
        DueDate = dueDate;
        Duration = duration;
        StartTime = startTime;
        EndTime = endTime;
        ActuallyStartTime = actuallyStartTime;
        ActuallyEndTime = actuallyEndTime;
        PrerequisiteOperations = prerequisiteOperations;
        WorkCenter = workCenter;
        WorkOrderStatus = workOrderStatus;
    }
}

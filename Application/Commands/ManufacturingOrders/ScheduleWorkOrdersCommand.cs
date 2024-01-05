using InjectionMachineModule.Application.Dtos.WorkOrders;
using InjectionMachineModule.Domain.Helpers;

namespace InjectionMachineModule.Application.Commands.ManufacturingOrders;

public class ScheduleWorkOrdersCommand: IRequest<IEnumerable<WorkOrderDto>>
{
    public List<WorkOrderIdentityViewModel> OrderIds { get; set; }
    public SchedulingStrategy SchedulingStrategy { get; set; }

    public ScheduleWorkOrdersCommand(List<WorkOrderIdentityViewModel> orderIds, SchedulingStrategy? schedulingStrategy)
    {
        OrderIds = orderIds;
        SchedulingStrategy = schedulingStrategy ?? SchedulingStrategy.Default;
    }
}

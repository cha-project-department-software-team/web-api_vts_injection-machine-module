using InjectionMachineModule.Application.Dtos.WorkOrders;

namespace InjectionMachineModule.Application.Commands.ManufacturingOrders;

public class ScheduleWorkOrdersCommand: IRequest<IEnumerable<WorkOrderDto>>
{
    public List<WorkOrderIdentityViewModel> OrderIds { get; private set; }

    public ScheduleWorkOrdersCommand(List<WorkOrderIdentityViewModel> orderIds)
    {
        OrderIds = orderIds;
    }
}

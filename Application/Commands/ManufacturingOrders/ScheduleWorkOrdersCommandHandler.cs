using InjectionMachineModule.Application.Dtos.ResourceNetworkConnections;
using InjectionMachineModule.Application.Dtos.WorkOrders;
using InjectionMachineModule.Application.Helpers;
using InjectionMachineModule.Application.Queries;
using InjectionMachineModule.Domain.Helpers;
using InjectionMachineModule.Domain.Models;
using Microsoft.AspNetCore.Mvc.Routing;

namespace InjectionMachineModule.Application.Commands.ManufacturingOrders;

public class ScheduleWorkOrdersCommandHandler : IRequestHandler<ScheduleWorkOrdersCommand, IEnumerable<WorkOrderDto>>
{
    private readonly RestClient _restClient;
    private readonly MesApiUrlHelper _urlHelper;

    private readonly List<Mold> molds = new List<Mold>();
    private readonly List<MoldingMachine> moldingMachines = new List<MoldingMachine>();

    public ScheduleWorkOrdersCommandHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }

    public async Task<IEnumerable<WorkOrderDto>> Handle(ScheduleWorkOrdersCommand request, CancellationToken cancellationToken)
    {
        var orders = new List<WorkOrder>();
        var availbleTime = DateTime.Now;

        for (int i = 0; i < request.OrderIds.Count; i++)
        {
            var workOrderDto = await GetWorkOrderAsync(request.OrderIds[i]);
            var manufacturingOrder = await GetManufacturingOrderAsync(request.OrderIds[i]);
            var availableMolds = await GetAvailableMoldsForWorkOrder(request.OrderIds[i], manufacturingOrder);
            var moldMoldingMachines = availableMolds
                .Select(async x => await GetMoldMoldingMachines(x))
                .SelectMany(x => x.Result);

            var workOrder = new WorkOrder(workOrderDto.WorkOrderId,
                                          i,
                                          (double) manufacturingOrder.Quantity,
                                          availableMolds,
                                          null,
                                          moldMoldingMachines.ToList(),
                                          null,
                                          availbleTime,
                                          workOrderDto.DueDate,
                                          null,
                                          null);
            orders.Add(workOrder);
        }

        var scheduleResult = MoldingMachineScheduling.ScheduleWorkOrders(orders, moldingMachines);

        var workOrders = scheduleResult.WorkOrders;
        var viewModels = workOrders.Select(x => new WorkOrderDto(
            request.OrderIds.First(o => o.WorkOrderId == x.Id).ManufacturingOrderId,
            x.Id,
            x.DueTime,
            x.Duration!.Value,
            x.StartTime,
            x.EndTime,
            null,
            null,
            new List<string>(),
            x.MoldingMachine!.WorkCenter,
            EWorkOrderStatus.Draft
            ));
        return viewModels;
    }

    public async Task<WorkOrderDto> GetWorkOrderAsync(WorkOrderIdentityViewModel orderId)
    {
        var url = _urlHelper.GenerateResourceUrl($"workOrders/{orderId.ManufacturingOrderId}/{orderId.WorkOrderId}");
        var workOrder = await _restClient.GetAsync<WorkOrderDto>(url);
        return workOrder is null
            ? throw new Exception($"No work order with id {orderId.ManufacturingOrderId}.{orderId.WorkOrderId}")
            : workOrder;
    }

    public async Task<List<Mold>> GetAvailableMoldsForWorkOrder(WorkOrderIdentityViewModel orderId, ManufacturingOrderDto manufacturingOrder)
    {
        var productMoldConnectionQueryResult = await _restClient.GetAsync<QueryResult<ResourceConnectionDto>>(_urlHelper.GenerateResourceUrl($"resourceRelationshipNetworks/PlasticProductMoldRelationshipId/Connections?FromResourceId={manufacturingOrder.MaterialDefinition.MaterialDefinitionId}"));

        var orderMolds = new List<Mold>();
        foreach (var productMoldConnection in productMoldConnectionQueryResult!.Items)
        {
            var moldId = productMoldConnection!.ToResource.ResourceId;

            Mold mold;
            if (molds!.Exists(x => x.Id == moldId))
            {
                mold = molds.First(x => x.Id == moldId);
            }
            else
            {
                var moldEquipment = await _restClient.GetAsync<EquipmentViewModelDto>(_urlHelper.GenerateResourceUrl($"equipments/{moldId}"));
                var cycleString = moldEquipment!.Properties.First(x => x.PropertyId == "Cycle").ValueString;
                var cycleBySecond = Convert.ToDouble(cycleString);
                var cycle = TimeSpan.FromSeconds(cycleBySecond);

                mold = new Mold(productMoldConnection!.ToResource.ResourceId, cycle);
                molds.Add(mold);
            }
            orderMolds.Add(mold);
        }

        return orderMolds;
    }

    public async Task<ManufacturingOrderDto> GetManufacturingOrderAsync(WorkOrderIdentityViewModel orderId)
    {
        var manufacturingOrder = await _restClient.GetAsync<ManufacturingOrderDto>(_urlHelper.GenerateResourceUrl($"manufacturingOrders/{orderId.ManufacturingOrderId}"));
        return manufacturingOrder is null
            ? throw new Exception($"No work order with id {orderId.ManufacturingOrderId}.{orderId.WorkOrderId}")
            : manufacturingOrder;
    }

    public async Task<List<MoldingMachine>> GetMoldMoldingMachines(Mold mold)
    {
        var machineMoldConnectionQueryResult = await _restClient.GetAsync<QueryResult<ResourceConnectionDto>>(_urlHelper.GenerateResourceUrl($"resourceRelationshipNetworks/MachineMoldRelationshipId/Connections?ToResourceId={mold.Id}"));

        var moldMoldingMachines = new List<MoldingMachine>();
        foreach (var machineMoldConnnection in machineMoldConnectionQueryResult!.Items)
        {
            MoldingMachine moldingMachine;
            var machineId = machineMoldConnnection!.FromResource.ResourceId;

            if (moldingMachines!.Exists(x => x.Id == machineId))
            {
                moldingMachine = moldingMachines.First(x => x.Id == machineId);
            }
            else
            {
                var moldEquipment = await _restClient.GetAsync<EquipmentViewModelDto>(_urlHelper.GenerateResourceUrl($"equipments/{machineId}"));
                var workUnit = moldEquipment!.WorkUnit;
                var workCenter = string.Join("/",workUnit.Split("/").SkipLast(1));

                moldingMachine = new MoldingMachine(machineId, new List<Mold>(), TimeSpan.FromMinutes(30), workCenter);
                moldingMachines.Add(moldingMachine);
            }
            moldingMachine.PossibleMolds.Add(mold);
            moldMoldingMachines.Add(moldingMachine);
        }

        return moldMoldingMachines;
    }
}

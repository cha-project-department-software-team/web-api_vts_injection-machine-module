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

    private readonly List<Mold> molds = new();
    private readonly List<MoldingMachine> moldingMachines = new();

    public ScheduleWorkOrdersCommandHandler(RestClient restClient, MesApiUrlHelper urlHelper)
    {
        _restClient = restClient;
        _urlHelper = urlHelper;
    }

    public async Task<IEnumerable<WorkOrderDto>> Handle(ScheduleWorkOrdersCommand request, CancellationToken cancellationToken)
    {
        var orders = new List<WorkOrder>();

        for (int i = 0; i < request.OrderIds.Count; i++)
        {
            var workOrderDto = await GetWorkOrderAsync(request.OrderIds[i]);
            var manufacturingOrder = await GetManufacturingOrderAsync(request.OrderIds[i]);
            var availableMolds = await GetAvailableMoldsForWorkOrder(manufacturingOrder);
            var moldMoldingMachines = availableMolds
                .Select(async x => await GetMoldMoldingMachines(x))
                .SelectMany(x => x.Result);

            var workOrder = new WorkOrder(workOrderDto.ManufacturingOrder,
                                          workOrderDto.WorkOrderId,
                                          manufacturingOrder.Priority,
                                          (double)manufacturingOrder.Quantity,
                                          availableMolds,
                                          null,
                                          moldMoldingMachines.ToList(),
                                          null,
                                          manufacturingOrder.AvailableDate,
                                          workOrderDto.DueDate,
                                          null);
            orders.Add(workOrder);
        }

        var scheduleResult = MoldingMachineScheduling.ScheduleWorkOrders(orders, moldingMachines, request.SchedulingStrategy);

        var workOrders = scheduleResult.WorkOrders;

        return workOrders.Select(x => new WorkOrderDto(
            x.ManufacturingOrderId,
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
    }

    public async Task<WorkOrderDto> GetWorkOrderAsync(WorkOrderIdentityViewModel orderId)
    {
        var url = _urlHelper.GenerateResourceUrl($"workOrders/{orderId.ManufacturingOrderId}/{orderId.WorkOrderId}");
        var workOrder = await _restClient.GetAsync<WorkOrderDto>(url);
        return workOrder ?? throw new Exception($"No work order with id {orderId.ManufacturingOrderId}.{orderId.WorkOrderId}");
    }

    public async Task<List<Mold>> GetAvailableMoldsForWorkOrder(ManufacturingOrderDto manufacturingOrder)
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
        return manufacturingOrder ?? throw new Exception($"No work order with id {orderId.ManufacturingOrderId}.{orderId.WorkOrderId}");
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
                var absolutePath = moldEquipment!.AbsolutePath;
                var hierarchyModelIds = absolutePath.Split('/');
                string workCenter = string.Empty;
                if (hierarchyModelIds.Length >= 4)
                {
                    workCenter = hierarchyModelIds[3];
                }

                moldingMachine = new MoldingMachine(machineId, new List<Mold>(), TimeSpan.FromMinutes(30), workCenter);
                moldingMachines.Add(moldingMachine);
            }
            moldingMachine.PossibleMolds.Add(mold);
            moldMoldingMachines.Add(moldingMachine);
        }

        return moldMoldingMachines;
    }
}

using InjectionMachineModule.Application.Commands.ManufacturingOrders;
using InjectionMachineModule.Application.Queries;
using InjectionMachineModule.Application.Queries.ManufacturingOrders;
using Microsoft.AspNetCore.Mvc;

namespace InjectionMachineModule.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManufacturingOrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public ManufacturingOrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateManufacturingOrder([FromBody] CreateManufacturingOrderCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    [HttpGet]
    public async Task<QueryResult<ManufacturingOrderViewModel>> GetManufacturingOrders([FromQuery] ManufacturingOrdersQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpDelete]
    [Route("{manufacturingOrderId}")]
    public async Task<IActionResult> DeleteManufacturingOrder([FromRoute] string manufacturingOrderId)
    {
        var command = new DeleteManufacturingOrderCommand(manufacturingOrderId);
        await _mediator.Send(command);

        return Ok();
    }

    [HttpPost]
    [Route("workorders/schedule")]
    public async Task<IActionResult> ScheduleWorkOrder(ScheduleWorkOrdersCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}

using InjectionMachineModule.Application.Commands.PlasticInjectionMachines;
using InjectionMachineModule.Application.Queries;
using InjectionMachineModule.Application.Queries.PlasticInjectionMachines;
using Microsoft.AspNetCore.Mvc;

namespace InjectionMachineModule.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PlasticInjectionMachinesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlasticInjectionMachinesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlasticInjectionMachine([FromBody] CreatePlasticInjectionMachineCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    [HttpPut]
    [Route("{equipmentId}")]
    public async Task<IActionResult> UpdatePlasticInjectionMachine([FromRoute]string equipmentId, [FromBody]UpdatePlasticInjectionMachineViewModel equipment)
    {
        var command = new UpdatePlasticInjectionMachineCommand(equipmentId, equipment.Name, equipment.Properties, equipment.Molds, equipment.AbsolutePath);
        await _mediator.Send(command);

        return Ok();
    }

    [HttpGet]
    public async Task<QueryResult<PlasticInjectionMachineViewModel>> GetPlasticInjectionMachine([FromQuery] PlasticInjectionMachinesQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpDelete]
    [Route("{equipmentId}")]
    public async Task<IActionResult> DeletePlasticInjectionMachine([FromRoute] string equipmentId)
    {
        var command = new DeletePlasticInjectionMachineCommand(equipmentId);
        await _mediator.Send(command);

        return Ok();
    }
}

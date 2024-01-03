using InjectionMachineModule.Application.Commands.Molds;
using InjectionMachineModule.Application.Queries;
using InjectionMachineModule.Application.Queries.Molds;
using Microsoft.AspNetCore.Mvc;

namespace InjectionMachineModule.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MoldsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoldsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMold([FromBody] CreateMoldCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    [HttpGet]
    public async Task<QueryResult<MoldViewModel>> GetMold([FromQuery] MoldsQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpDelete]
    [Route("{moldId}")]
    public async Task<IActionResult> DeleteMold([FromRoute] string moldId)
    {
        var command = new DeleteMoldCommand(moldId);
        await _mediator.Send(command);

        return Ok();
    }
}

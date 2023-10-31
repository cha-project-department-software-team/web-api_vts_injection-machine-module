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
        var response = await _mediator.Send(command);
        string responseData = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return BadRequest(responseData);

        return Ok(responseData);
    }

    [HttpGet]
    public async Task<QueryResult<MoldViewModel>> GetMold([FromQuery] MoldsQuery query)
    {
        return await _mediator.Send(query);
    }
}

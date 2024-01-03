using InjectionMachineModule.Application.Commands.PlasticMaterials;
using InjectionMachineModule.Application.Queries;
using InjectionMachineModule.Application.Queries.PlasticMaterials;
using Microsoft.AspNetCore.Mvc;

namespace InjectionMachineModule.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PlasticMaterialsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlasticMaterialsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlasticMaterial([FromBody]CreatePlasticMaterialCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    [HttpGet]
    public async Task<QueryResult<PlasticMaterialViewModel>> GetPlasticMaterials([FromQuery]PlasticMaterialsQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpDelete]
    [Route("{plasticMaterialId}")]
    public async Task<IActionResult> DeletePlasticMaterial([FromRoute] string plasticMaterialId)
    {
        var command = new DeletePlasticMaterialCommand(plasticMaterialId);
        await _mediator.Send(command);

        return Ok();
    }
}

using InjectionMachineModule.Application.Commands.PlasticMaterials;
using InjectionMachineModule.Application.Commands.PlasticProduct;
using InjectionMachineModule.Application.Queries;
using InjectionMachineModule.Application.Queries.PlasticProducts;
using Microsoft.AspNetCore.Mvc;

namespace InjectionMachineModule.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PlasticProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlasticProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlasticProduct([FromBody] CreatePlasticProductCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    [HttpGet]
    public async Task<QueryResult<PlasticProductViewModel>> GetPlasticProducts([FromQuery] PlasticProductsQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpDelete]
    [Route("{plasticProductId}")]
    public async Task<IActionResult> DeletePlasticProduct([FromRoute] string plasticProductId)
    {
        var command = new DeletePlasticProductCommand(plasticProductId);
        await _mediator.Send(command);

        return Ok();
    }
}

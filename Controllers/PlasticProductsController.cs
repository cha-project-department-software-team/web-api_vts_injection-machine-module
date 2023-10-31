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
        var response = await _mediator.Send(command);
        string responseData = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return BadRequest(responseData);

        return Ok(responseData);
    }

    [HttpGet]
    public async Task<QueryResult<PlasticProductViewModel>> GetPlasticProducts([FromQuery] PlasticProductsQuery query)
    {
        return await _mediator.Send(query);
    }
}

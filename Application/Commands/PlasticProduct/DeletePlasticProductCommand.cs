namespace InjectionMachineModule.Application.Commands.PlasticProduct;

public class DeletePlasticProductCommand : IRequest
{
    public string PlasticProductId { get; set; }

    public DeletePlasticProductCommand(string plasticProductId)
    {
        PlasticProductId = plasticProductId;
    }
}

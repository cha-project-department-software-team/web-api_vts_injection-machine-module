namespace InjectionMachineModule.Application.Commands.PlasticMaterials;

public class DeletePlasticMaterialCommand : IRequest
{
    public string PlasticMaterialId { get; set; }

    public DeletePlasticMaterialCommand(string plasticMaterialId)
    {
        PlasticMaterialId = plasticMaterialId;
    }
}

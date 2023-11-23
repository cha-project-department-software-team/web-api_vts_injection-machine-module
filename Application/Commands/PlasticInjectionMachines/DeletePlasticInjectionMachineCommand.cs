namespace InjectionMachineModule.Application.Commands.PlasticInjectionMachines;

public class DeletePlasticInjectionMachineCommand : IRequest
{
    public string EquipmentId { get; set; }

    public DeletePlasticInjectionMachineCommand(string equipmentId)
    {
        EquipmentId = equipmentId;
    }
}

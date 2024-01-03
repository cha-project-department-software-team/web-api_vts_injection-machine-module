namespace InjectionMachineModule.Application.Commands.Molds;

public class DeleteMoldCommand : IRequest
{
    public string MoldId { get; set; }

    public DeleteMoldCommand(string moldId)
    {
        MoldId = moldId;
    }
}

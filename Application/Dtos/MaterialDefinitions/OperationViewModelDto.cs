namespace InjectionMachineModule.Application.Dtos.MaterialDefinitions;

public class OperationViewModelDto
{
    public string OperationId { get; set; }
    public string Name { get; set; }
    public TimeSpan Duration { get; set; }
    public List<string> PrerequisiteOperation { get; set; }

    public OperationViewModelDto(string operationId, string name, TimeSpan duration, List<string> prerequisiteOperation)
    {
        OperationId = operationId;
        Name = name;
        Duration = duration;
        PrerequisiteOperation = prerequisiteOperation;
    }
}

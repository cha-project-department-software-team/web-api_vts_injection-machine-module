namespace InjectionMachineModule.Application.Dtos.MaterialDefinitions;

public class OperationViewModelDto
{
    public string OperationId { get; set; }
    public string Name { get; set; }
    public List<string> PrerequisiteOperation { get; set; }

    public OperationViewModelDto(string operationId, string name, List<string> prerequisiteOperation)
    {
        OperationId = operationId;
        Name = name;
        PrerequisiteOperation = prerequisiteOperation;
    }
}

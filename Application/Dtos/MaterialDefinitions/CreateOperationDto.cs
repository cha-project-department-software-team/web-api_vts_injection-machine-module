namespace InjectionMachineModule.Application.Dtos.MaterialDefinitions;

public class CreateOperationDto
{
    public string OperationId { get; set; }
    public string Name { get; set; }
    public List<string> PrerequisiteOperation { get; set; } = new List<string>();

    public CreateOperationDto()
    {
        OperationId = "Ép máy";
        Name = "Công đoạn ép";
    }
}

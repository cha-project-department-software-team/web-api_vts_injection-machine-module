namespace InjectionMachineModule.Application.Dtos.MaterialDefinitions;

public class CreateOperationDto
{
    public string OperationId { get; set; }
    public string Name { get; set; }
    public TimeSpan Duration { get; set; }
    public List<string> PrerequisiteOperation { get; set; } = new List<string>();

    public CreateOperationDto(string materialId)
    {
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        int day = DateTime.Now.Day;

        OperationId = $"OP-{(object)year}{(object)month:D2}{(object)day:D2}-{(object)new Random().Next(100, 1000):D3}-{materialId}";
        Name = "Công đoạn ép";
        Duration = TimeSpan.Zero;
    }
}

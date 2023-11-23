namespace InjectionMachineModule.Application.Dtos.MaterialDefinitions;

public class CreateOperationDto
{
    public string OperationId { get; set; }
    public string Name { get; set; }
    public TimeSpan Duration { get; set; }
    public List<string> PrerequisiteOperation { get; set; } = new List<string>();

    public CreateOperationDto()
    {
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        int day = DateTime.Now.Day;

        OperationId = string.Format("OP-{0}{1:D2}{2:D2}-{3:D3}", year, month, day, new Random().Next(100, 1000));
        Name = "Công đoạn ép";
        Duration = TimeSpan.Zero;
    }
}

namespace InjectionMachineModule.Domain.Models;

public class Mold
{
    public string Id { get; set; }
    public TimeSpan Cycle { get; set; }

    public Mold(string id, TimeSpan cycle)
    {
        Id = id;
        Cycle = cycle;
    }
}

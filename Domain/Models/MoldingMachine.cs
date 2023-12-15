namespace InjectionMachineModule.Domain.Models;

public class MoldingMachine
{
    public string Id { get; set; }
    public TimeSpan ChangeoverTime { get; set; }
    public List<Mold> PossibleMolds { get; set; }
    public string WorkCenter { get; set; }

    public MoldingMachine(string id, List<Mold> possibleMolds, TimeSpan changeoverTime, string workStation)
    {
        Id = id;
        PossibleMolds = possibleMolds;
        ChangeoverTime = changeoverTime;
        WorkCenter = workStation;
    }
}

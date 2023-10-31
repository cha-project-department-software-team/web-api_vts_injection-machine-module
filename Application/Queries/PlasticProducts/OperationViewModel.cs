﻿namespace InjectionMachineModule.Application.Queries.PlasticProducts;

public class OperationViewModel
{
    public string OperationId { get; set; }
    public string Name { get; set; }
    public TimeSpan Duration { get; set; }
    public List<string> PrerequisiteOperation { get; set; }

    public OperationViewModel(string operationId, string name, TimeSpan duration, List<string> prerequisiteOperation)
    {
        OperationId = operationId;
        Name = name;
        Duration = duration;
        PrerequisiteOperation = prerequisiteOperation;
    }
}

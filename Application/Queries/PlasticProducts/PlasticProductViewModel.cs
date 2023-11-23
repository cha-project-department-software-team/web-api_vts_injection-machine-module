﻿namespace InjectionMachineModule.Application.Queries.PlasticProducts;

public class PlasticProductViewModel
{
    public string PlasticProductId { get; set; }
    public string Name { get; set; }
    public string PrimaryUnit { get; set; }
    public string ModuleType { get; set; }
    public List<PropertyViewModel> Properties { get; set; }
    public List<MaterialUnitViewModel> SecondaryUnits { get; set; }
    public List<OperationViewModel> Operations { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private PlasticProductViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public PlasticProductViewModel(string plasticProductId, string name, string primaryUnit, string moduleType, List<PropertyViewModel> properties, List<MaterialUnitViewModel> secondaryUnits, List<OperationViewModel> operations)
    {
        PlasticProductId = plasticProductId;
        Name = name;
        PrimaryUnit = primaryUnit;
        ModuleType = moduleType;
        Properties = properties;
        SecondaryUnits = secondaryUnits;
        Operations = operations;
    }
}
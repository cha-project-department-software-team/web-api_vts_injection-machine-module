using System.Runtime.Serialization;

namespace InjectionMachineModule.Application.Commands.ManufacturingOrders;

[DataContract]
public class CreateManufacturingOrderCommand : IRequest
{
    [DataMember]
    public string ManufacturingOrderId { get; set; }
    [DataMember]
    public string MaterialDefinitionId { get; set; }
    [DataMember]
    public decimal Quantity { get; set; }
    [DataMember]
    public string Unit { get; set; }
    [DataMember]
    public DateTime DueDate { get; set; }
    [DataMember]
    public DateTime AvailableDate { get; set; }
    [DataMember]
    public int Priority { get; set; }
    [DataMember]
    public List<string> Equipments { get; set; }

    public CreateManufacturingOrderCommand(string manufacturingOrderId, string materialDefinitionId, decimal quantity, string unit, DateTime dueDate, DateTime availableDate, int priority, List<string> equipments)
    {
        ManufacturingOrderId = manufacturingOrderId;
        MaterialDefinitionId = materialDefinitionId;
        Quantity = quantity;
        Unit = unit;
        DueDate = dueDate;
        AvailableDate = availableDate;
        Priority = priority;
        Equipments = equipments;
    }
}

namespace InjectionMachineModule.Domain.Helpers;

public class SchedulingStrategy
{
    public OrderPriortizingStrategy PriortizingStrategy { get; set; }

    public SchedulingStrategy(OrderPriortizingStrategy priortizingStrategy)
    {
        PriortizingStrategy = priortizingStrategy;
    }

    public static SchedulingStrategy Default => new(OrderPriortizingStrategy.ManualPriority);
}

namespace InjectionMachineModule.Application.Queries.PlasticInjectionMachines;

public class PlasticInjectionMachinesQuery : PaginatedQuery, IRequest<QueryResult<PlasticInjectionMachineViewModel>>
{
    public string? IdStartedWith { get; set; }
}

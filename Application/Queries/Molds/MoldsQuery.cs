namespace InjectionMachineModule.Application.Queries.Molds;

public class MoldsQuery : PaginatedQuery, IRequest<QueryResult<MoldViewModel>>
{
    public string? IdStartedWith { get; set; }
}

namespace InjectionMachineModule.Application.Queries.PlasticProducts;

public class PlasticProductsQuery : PaginatedQuery, IRequest<QueryResult<PlasticProductViewModel>>
{
    public string? IdStartedWith { get; set; }
}

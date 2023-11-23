namespace InjectionMachineModule.Application.Queries.PlasticMaterials;

public class PlasticMaterialsQuery : PaginatedQuery, IRequest<QueryResult<PlasticMaterialViewModel>>
{
    public string? IdStartedWith { get; set; }
}

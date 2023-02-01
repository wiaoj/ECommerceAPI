namespace ECommerceAPI.Application.Features.Queries.Roles.GetAllRoles;

public class GetRolesQueryResponse {
    public Int32 TotalRoleCount { get; set; }
    public IDictionary<String,String> Roles { get; set; }
}
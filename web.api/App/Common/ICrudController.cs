using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace web.api.App.Common
{
    public interface ICrudController<
        T_GetEntityQuery,
        T_AddEntityRequest,
        T_EditEntityRequest,
        T_DeleteEntityRequest,
        T_SearchEntityQuery>
    {
        Task<ObjectResult> Get(T_GetEntityQuery query);
        Task<ObjectResult> Post(T_AddEntityRequest request);
        Task<ObjectResult> Put(int id, T_EditEntityRequest command);
        Task<ObjectResult> Delete(T_DeleteEntityRequest command);
        Task<ObjectResult> Search(T_SearchEntityQuery searchParams);
    }
}
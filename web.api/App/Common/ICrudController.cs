using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace web.api.App.Common
{
    public interface ICrudController<
        T_GetEntityQuery,
        T_AddEntityCommand,
        T_EditEntityCommand,
        T_DeleteEntityCommand,
        T_SearchEntityQuery>
    {
        Task<ObjectResult> Get(T_GetEntityQuery query);
        Task<ObjectResult> Post(T_AddEntityCommand command);
        Task<ObjectResult> Put(int id, T_EditEntityCommand command);
        Task<ObjectResult> Delete(T_DeleteEntityCommand command);
        Task<ObjectResult> Search(T_SearchEntityQuery searchParams);
    }
}
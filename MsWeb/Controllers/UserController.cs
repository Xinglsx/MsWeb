using System.Threading.Tasks;
using System.Web.Http;

using MsWeb.DataObjects;
using MsWeb.IServices;

using Winning.Framework.DMSP.Web.WebApi;

namespace MsWeb.Controllers
{
    public class UserController : WebApiController<UsersModel, IUsersService>
    {
    }
}

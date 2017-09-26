using System.Threading.Tasks;
using System.Web.Http;

using MsWeb.DataObjects;
using MsWeb.IServices;

using Winning.Framework.DMSP.Web.WebApi;

namespace MsWeb.Controllers
{
    public class UserController : WebApiController<UsersModel, IUsersService>
    {
        [HttpPost]
        public async Task<object> Login(LoginModel loginModel)
        {
            var user = await Service.Login(loginModel.LoginName, loginModel.Password);
            return user;
        }

        [HttpGet]
        public async Task<object> GetAllUsers()
        {
            return await Try(() => Service.GetAll(), () => ResponseResult.Fail("登录失败"));
        }

        [HttpPost]
        public async Task<bool> Add(UsersModel user)
        {
            user.SetNewID();
            return await Service.Add(user);
        }

        [HttpPost]
        public async Task<bool> Update(UsersModel user)
        {
            return await Service.Update(user);
        }
    }

    public class LoginModel
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}

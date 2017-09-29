using MsWeb.DataObjects;
using MsWeb.IServices;
using System.Threading.Tasks;
using System.Web.Mvc;
using Winning.Framework.DMSP.Web.WebApi;

namespace MsWeb.Api.Controllers
{
    public class QuestionController : WebApiController<QuestionsModel, IQuestionsService>
    {
        [HttpPost]
        public async Task<ReturnResult<bool>> AddQuestion(QuestionsModel questionInfo)
        {
            return await Service.AddQuestion(questionInfo);
        }
    }
}
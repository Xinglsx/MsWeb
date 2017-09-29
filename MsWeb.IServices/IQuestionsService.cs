using MsWeb.DataObjects;
using System.Threading.Tasks;
using Winning.Framework.DMSP.Services;
using Winning.Framework.DMSP.Services.ServiceContract;

namespace MsWeb.IServices
{
    public interface IQuestionsService : IBaseServiceKeyedString<QuestionsModel>, IServiceContract
    {
        /// <summary>
        /// 保存用户反馈信息
        /// </summary>
        /// <param name="userInfo">用户反馈信息</param>
        /// <returns></returns>
        Task<ReturnResult<bool>> AddQuestion(QuestionsModel questionInfo);
    }
}

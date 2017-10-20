using System.Runtime.Serialization;
using Mingshu.Framework.MSWeb.Core.BaseDataObject;

namespace MsWeb.DataObjects
{
    public class VersionInfo: Model<string>
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public int versionNumber { get; set; }
        /// <summary>
        /// 版本编号
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        public string downloadAddress { get; set; }
        /// <summary>
        /// 更新内容
        /// </summary>
        public string updateContent { get; set; }
    }
}

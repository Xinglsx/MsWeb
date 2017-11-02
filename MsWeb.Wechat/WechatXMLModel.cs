using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsWeb.Wechat
{
    /// <summary>
    /// 微信接口XmlModel
    /// XML解析
    /// </summary>
    public class WechatXMLModel
    {
        /// <summary>
        /// 消息接收方微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 消息发送方微信号
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 信息类型 地理位置:location,文本消息:text,消息类型:image
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public string Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 图片链接，开发者可以用HTTP GET获取
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 事件类型，subscribe(订阅/扫描带参数二维码订阅)、unsubscribe(取消订阅)、CLICK(自定义菜单点击事件) 、SCAN（已关注的状态下扫描带参数二维码）
        /// </summary>
        public string Event { get; set; }
        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 二维码的ticket，可以用来换取二维码
        /// </summary>
        public string Ticket { get; set; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId { get; set; }
        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取该媒体
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 语音识别结果，UTF8编码
        /// </summary>
        public string Recognition { get; set; }
        /// <summary>
        /// 语音格式：amr
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Android.XiaoMi.Push
{
    /// <summary>
    /// 消息创建
    /// </summary>
    public class MessageBuilder
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        private string _title;
        /// <summary>
        /// 透传消息
        /// </summary>
        private string _payload;
        /// <summary>
        /// 消息内容
        /// </summary>
        private string _description;
        /// <summary>
        /// 提示音
        /// </summary>
        private int _notifyType=-1;
        /// <summary>
        /// 服务器保存时间
        /// </summary>
        private long? _timeToLive;
        /// <summary>
        /// 是否透传消息
        /// </summary>
        private int _passThrough;
        /// <summary>
        /// //取时间戳，避免通知覆盖
        /// </summary>
        private int? _notifyId;
        /// <summary>
        /// 是否及时发送
        /// </summary>
        private long? _timeToSend;

        /// <summary>
        /// 包名
        /// </summary>
        private string _packageName;

        private Dictionary<string, string> _extra;


        public Dictionary<string,string> Build()
        {
            if (string.IsNullOrEmpty(_title))
                throw new Exception("title不能为空");

            if (string.IsNullOrEmpty(_description))
                throw new Exception("description不能为空");

            _passThrough = _passThrough == 0 ? 0 : 1;

            var dicPara = new Dictionary<string, string>
            {
                {"notify_type", _notifyType.ToString()},
                {"title", _title},
                {"description", _description},
                {"pass_through", _passThrough.ToString()}
            };

            if (!string.IsNullOrEmpty(_payload))
                dicPara.Add("payload", _payload);

            if (!string.IsNullOrEmpty(_packageName))
                dicPara.Add("restricted_package_name", _packageName);

            if (_timeToLive != null)
                dicPara.Add("time_to_live", _timeToLive.ToString());

            if (_timeToSend != null)
                dicPara.Add("time_to_send", _timeToSend.ToString());

            if (_notifyId != null)
                dicPara.Add("notify_id", _notifyId.ToString());

            return dicPara;
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        public MessageBuilder SetTile(string title)
        {
            _title = title;
            return this;
        }

        /// <summary>
        /// 设置透传消息
        /// </summary>
        public MessageBuilder SetPayload(string payload)
        {
            _payload = payload;
            return this;
        }

        /// <summary>
        /// 设置消息内容
        /// </summary>
        public MessageBuilder SetDescription(string description)
        {
            _description = description;
            return this;
        }

        /// <summary>
        /// 设置消息提示音
        /// </summary>
        public MessageBuilder SetNotifyType(int notifyType)
        {
            _notifyType = notifyType;
            return this;
        }

        /// <summary>
        /// 设置消息过期时间
        /// </summary>
        public MessageBuilder SetTimeToLive(long timeToLive)
        {
            _timeToLive = timeToLive;
            return this;
        }

        /// <summary>
        /// 设置消息类型是否透传（0 表示通知栏消息 1 表示透传消息）
        /// </summary>
        public MessageBuilder SetPassThrough(int passThrough)
        {
            _passThrough = passThrough;
            return this;
        }

        /// <summary>
        /// 设置消息是否重复显示
        /// </summary>
        public MessageBuilder SetNotifyId(int notifyId)
        {
            _notifyId = notifyId;
            return this;
        }

        /// <summary>
        /// 设置消息发送时间
        /// </summary>
        public MessageBuilder SetTimeToSend(long timeToSend)
        {
            _timeToSend = timeToSend;
            return this;
        }

        /// <summary>
        /// 扩展消息
        /// </summary>
        public MessageBuilder SetExtra(string title, string content)
        {
            if(_extra==null)
                _extra=new Dictionary<string, string>();
            _extra.TryAdd(title, content);
            return this;
        }

        /// <summary>
        /// 包名
        /// </summary>
        public MessageBuilder SetPackageName(string packageName)
        {
            _packageName = packageName;
            return this;
        }

    }
}

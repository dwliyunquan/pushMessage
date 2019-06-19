using System.Collections.Generic;
using Newtonsoft.Json;

namespace Android.Huawei.Push
{
    public class PayloadFomat
    {
        /// <summary>
        /// 创建普通消息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string FormatNotice(string title,string content)
        {
            var param = new Dictionary<string, string> {{"intent", "#intent"}};
            var action = new Dictionary<string, object> {{"type", 1}, {"param", param}};
            var body = new Dictionary<string, string> {{"title", title}, {"content", content}};
            var msg = new Dictionary<string, object> {{"type", 3}, {"body", body}, {"action", action}};
            var hps = new Dictionary<string, object> {{"msg", msg}};
            var messagePara = new Dictionary<string, object> {{"hps", hps}};
            return JsonConvert.SerializeObject(messagePara);
        }
    }
}

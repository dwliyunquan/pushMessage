using System;
using System.Collections.Generic;
using Android.Huawei.Push;
using Android.XiaoMi.Push;

namespace Android.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestXiaoMi();
        }

        private void TestHuawei()
        {
            try
            {
                var options = new HuaweiPushOptions()
                {
                    AppId = "",
                    AppSecret = ""
                };
                var huaweiPush = new HuaweiPush(options);
                var accessToken = huaweiPush.GetAccessToken();

                for (int i = 0; i < 10; i++)
                {
                    var payloadStr = PayloadFomat.FormatNotice("收到一笔转账", $"钱包到账{i}");
                    var deviceTokens = new List<string>
                    {
                        "\"ABTCm3yFqWKBv1gjITcdCF_8iWzeJwMjmGUoYrWeTx4EOUc5WhaExV18jJJ2aLuO38wA8MXIhktHK6qmi76tjNBwloHph46ayMRIhcnJaZKCx03QsRxO7NZbrSuVDhfpeQ\""
                    };
                    huaweiPush.PushMessage(accessToken, payloadStr, deviceTokens);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void TestXiaoMi()
        {
            try
            {

                var xiaomiPush = new XiaoMiPush(new XiaoMiOptions() {AppSercet = "自己的秘钥"});
                for (int i = 1; i < 5; i++)
                {
                    var messBuilder = new MessageBuilder()
                        .SetTile($"消息{i.ToString()}")
                        .SetDescription($"收到一笔转账{i.ToString()}ETH")
                        .SetPassThrough(0)
                        .SetPayload("1")
                        .SetPackageName("org.sasa.planet")
                        .SetNotifyType(1)
                        .SetTimeToLive(3600000 * 336)
                        .SetNotifyId(i)
                        .Build();
                    var deviceIds = new List<string> { "joPpiUzmMStEDLzseMWdIVBNm+nLq0XcyBOI0rhfRLl+ptzcxOTp1Tps71dWBfoR" };
                    var result = xiaomiPush.PushByRegid(messBuilder, deviceIds);
                }



            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

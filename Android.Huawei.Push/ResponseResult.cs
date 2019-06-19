namespace Android.Huawei.Push
{
    public class PushResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Msg { get; set; }
    }

    public class AccessTokenResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string Access_token { get; set; }

        /// <summary>
        /// 有效时间
        /// </summary>
        public int Expires_in { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Token_type { get; set; }
    }
}

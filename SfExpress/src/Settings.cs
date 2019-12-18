namespace SfExpress.src
{
    public class Settings
    {    /// <summary>
         /// 顺风配置（这些配置信息都是丰桥官网去申请的）
         /// </summary>
        public class ShunFeng
        {
            /// <summary>
            /// 丰桥 客户授权url
            /// </summary>
            public const string Url = "Infrastructure.ShunFeng.Url";

            /// <summary>
            /// 丰桥 用户编码
            /// </summary>
            public const string ClientCode = "Infrastructure.ShunFeng.ClientCode";

            /// <summary>
            /// 丰桥 授权检测码
            /// </summary>
            public const string CheckWord = "Infrastructure.ShunFeng.CheckWord";
            /// <summary>
            /// 月结卡号
            /// </summary>
            public const string CustId = "Infrastructure.ShunFeng.CustId";

            /// <summary>
            /// 打印请求的url 例如三联打印:http://localhost:4040/sf/waybill/print?type=V3.0.FM_poster_100mm210mm&output=image
            /// </summary>
            public const string PrintUrl = "Infrastructure.ShunFeng.PrintUrl";
        }
    }

}

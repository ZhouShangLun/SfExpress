using System.Collections.Generic;

namespace SfExpress.src.SfRequest
{
    public class SfPrintRequest
    {
        /// <summary>
        ///  丰桥顾客编码
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 丰桥校验码
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        public string MailNo { get; set; }

        /// <summary>
        /// 快递产品类型
        /// </summary>
        public ExpressType ExpressType { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public SfPayMethod PayMethod { get; set; }

        /// <summary>
        /// 收件地代码
        /// </summary>
        public string DestCode { get; set; }

        /// <summary>
        /// 寄件人公司
        /// </summary>
        public string DeliverCompany { get; set; }

        /// <summary>
        /// 寄件人姓名
        /// </summary>
        public string DeliverName { get; set; }

        /// <summary>
        /// 寄件人手机
        /// </summary>
        public string DeliverMobile { get; set; }

        /// <summary>
        /// 寄件人省
        /// </summary>

        public string DeliverProvince { get; set; }

        /// <summary>
        /// 寄件人市
        /// </summary>
        public string DeliverCity { get; set; }

        /// <summary>
        /// 寄件人区
        /// </summary>
        public string DeliverCounty { get; set; }

        /// <summary>
        /// 寄件人地址
        /// </summary>
        public string DeliverAddress { get; set; }


        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string ConsignerName { get; set; }

        /// <summary>
        /// 收件人手机
        /// </summary>
        public string ConsignerMobile { get; set; }

        /// <summary>
        /// 收件人省
        /// </summary>

        public string ConsignerProvince { get; set; }

        /// <summary>
        /// 收件人市
        /// </summary>
        public string ConsignerCity { get; set; }

        /// <summary>
        /// 收件人区
        /// </summary>
        public string ConsignerCounty { get; set; }

        /// <summary>
        /// 收件人地址
        /// </summary>
        public string ConsignerAddress { get; set; }

        /// <summary>
        /// 加密电话
        /// </summary>
        public bool EncryptMobile { get; set; }

        /// <summary>
        /// 加密寄件及收件联系人
        /// </summary>
        public bool EncryptCustName { get; set; }

        public ICollection<CargoInfo> CargoInfoDtoList { get; set; } = new HashSet<CargoInfo>();

        public ICollection<RlsInfo> RlsInfoDtoList { get; set; } = new HashSet<RlsInfo>();
    }

    public class CargoInfo
    {
        /// <summary>
        /// 货物名称
        /// </summary>
        public string Cargo { get; set; }

        /// <summary>
        /// 货物数量
        /// </summary>
        public string CargoCount { get; set; }

        public string CargoUnit { get; set; } = "";

        public string CargoWeight { get; set; } = "";

        public string CargoAmount { get; set; } = "";

        public string Remark { get; set; } = "";
    }
    public class RlsInfo
    {
        /// <summary>
        /// 运单号
        /// </summary>
        public string WaybillNo { get; set; }



        /// <summary>
        /// 产品代码(一般给T6)
        /// </summary>
        public string ProCode { get; set; }

        /// <summary>
        /// 终点路由标签
        /// </summary>
        public string DestRouteLabel { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        public string Qrcode { get; set; }

        /// <summary>
        /// printIcon,如未返回或该值为空，则填‘00000000’
        /// </summary>
        public string PrintIcon { get; set; }
    }

    public enum ExpressType
    {
        /// <summary>
        /// 标准快递
        /// </summary>
        StandardExpress = 1,

        /// <summary>
        ///  顺丰特惠
        /// </summary>
        SfEconomyExpress = 2,

        /// <summary>
        ///  电商特惠
        /// </summary>
        OnlineRetailersEconomyExpress = 3,
        /// <summary>
        /// 其他
        /// </summary>
        Others = 4,

        /// <summary>
        ///  顺丰次晨
        /// </summary>
        SFNextMorningDelivery = 5,


        /// <summary>
        ///  顺丰即日
        /// </summary>
        SfSameDayDelivery = 6,




        //还有好多枚举，不一一列出了


    }

    public enum SfPayMethod
    {
        /// <summary>
        /// 寄付
        /// </summary>
        DeliveryFeePrepaid = 1,

        /// <summary>
        /// 到付
        /// </summary>
        DeliverFeeToBeCollected = 2,

        /// <summary>
        /// 第三方付
        /// </summary>
        ThirdPartyPay = 3
    }
}

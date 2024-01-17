namespace BanooClub.Models
{
    public class NextPayModel
    {
        public string api_key { get; set; }
        public string order_id { get; set; }
        public long amount { get; set; }
        public string callback_uri { get; set; }
        public string currency { get; set; }
        public string customer_phone { get; set; }
    }
    public class NextPayVerifyResponse
    {
        public int code { get; set; }
        public string trans_id { get; set; }
        public long amount { get; set; }
    }
    public class CheckPay
    {
        public string api_key { get; set; }
        public string trans_id { get; set; }
        public long amount { get; set; }
        public string currency { get; set; }
    }
    public class VerifyCheckPay
    {
        public int code { get; set; }
        public long amount { get; set; }
        public string orderId { get; set; }
        public string card_holder { get; set; }
        public string customer_phone { get; set; }
        public string Shaparak_Ref_Id { get; set; }
        public string custom { get; set; }
    }
}

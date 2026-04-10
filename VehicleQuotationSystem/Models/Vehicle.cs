namespace QuotationAPI.Models
{
    public class Vehicle
    {
        public string FDVNO1 { get; set; }
        public string FDVNO2 { get; set; }
        public string FDPROV { get; set; }
        public string FDTPROV { get; set; }
        public string FDTVNO1 { get; set; }
        public string FDTVNO2 { get; set; }
        public string FDCHANO { get; set; }
        public string POLICYTYPENAME { get; set; }
        public string POLID { get; set; }
        public string FDVEHICLECATEGORY { get; set; }
        public string FDVEHITYPE { get; set; }
        public string FDVEHIID { get; set; }
        public string SUBCATE { get; set; }
        public string FDBUSITYPE { get; set; }
        public string FDPERIOD { get; set; }
        public string FDPERIODID { get; set; }
        public string FDDAYS { get; set; }
        public string FDVEHITRAIL { get; set; }
        public string FDSINGVEHI { get; set; }
        public string FDYEAR { get; set; }
        public string FDISPOLFEE { get; set; }
        public string FDPERMENTVEH { get; set; }
        public string FDVEHCATEG { get; set; }
        public string FDVEHVAL { get; set; }
        public string FDTRAVAL { get; set; }
        public string ISINCLTAX { get; set; }
        public string EXCLUDTAXTYPE { get; set; }
        public string FDNUMPASSEN { get; set; }
        public string ISNEWDUPQUOT { get; set; }

        public List<Cover> Covers { get; set; }
    }
}
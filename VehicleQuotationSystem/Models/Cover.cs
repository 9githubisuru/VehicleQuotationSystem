using Oracle.ManagedDataAccess.Client;

namespace QuotationAPI.Models
{
    public class Cover
    {
        public string FDVN01 { get; internal set; }
        public string FDVN02 { get; internal set; }
        public string CoverName { get; set; }
        public string COVER_CODE { get; set; }
        public string TYPE { get; set; }
        public string COVVALUE { get; set; }
        public string COVPREC { get; set; }
        public string MAXVAL { get; set; }
        public string NOOFPASS { get; set; }
        public string IS_SELECTED { get; set; }
        public string RATECODE { get; set; }
        //public OracleDbType FDVNO2 { get; internal set; }
        //public OracleDbType FDVNO1 { get; internal set; }
        
    }
}
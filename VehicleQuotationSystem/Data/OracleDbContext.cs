//using Oracle.ManagedDataAccess.Client;
//using System.Data;

//namespace QuotationAPI.Data
//{
//    public class OracleDbContext
//    {
//        private readonly string _connectionString;
//        private object _context;

//        public OracleDbContext(IConfiguration configuration)
//        {
//            _connectionString = configuration.GetConnectionString("OracleDb");
//        }

//        public IDbConnection CreateConnection
//        {
//            get
//            {
//                return new OracleConnection(_connectionString);
//            }
//        }

//        internal object CreateConnection()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
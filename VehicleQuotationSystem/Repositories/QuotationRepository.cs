//using QuotationAPI.Models;
//using QuotationAPI.Data;
//using Oracle.ManagedDataAccess.Client;

//namespace QuotationAPI.Repositories
//{
//    public class QuotationRepository
//    {
//        private readonly OracleDbContext _context;

//        public QuotationRepository(OracleDbContext context)
//        {
//            _context = context;
//        }

//        public void SaveQuotation(List<Vehicle> vehicles)
//        {
//            using var connection = _context.CreateConnection();
//            connection.Open();

//            using var transaction = connection.BeginTransaction();

//            try
//            {
//                foreach (var vehicle in vehicles)
//                {
//                    // =========================
//                    // INSERT VEHICLE
//                    // =========================
//                    var vehicleCmd = connection.CreateCommand();
//                    vehicleCmd.Transaction = transaction;

//                    vehicleCmd.CommandText = @"
//                        INSERT INTO VEHICLE_TABLE (
//                            FDVNO1, FDPROV, FDVEHIID
//                        ) VALUES (
//                            :FDVNO1, :FDPROV, :FDVEHIID
//                        ) RETURNING ID INTO :OUT_ID";

//                    vehicleCmd.Parameters.Add(new OracleParameter("FDVNO1", vehicle.FDVNO1));
//                    vehicleCmd.Parameters.Add(new OracleParameter("FDPROV", vehicle.FDPROV));
//                    vehicleCmd.Parameters.Add(new OracleParameter("FDVEHIID", vehicle.FDVEHIID));

//                    var outParam = new OracleParameter("OUT_ID", OracleDbType.Int32)
//                    {
//                        Direction = System.Data.ParameterDirection.Output
//                    };
//                    vehicleCmd.Parameters.Add(outParam);

//                    vehicleCmd.ExecuteNonQuery();

//                    int vehicleId = Convert.ToInt32(outParam.Value.ToString());

//                    // =========================
//                    // INSERT COVERS (CHILD)
//                    // =========================
//                    foreach (var cover in vehicle.Covers)
//                    {
//                        var coverCmd = connection.CreateCommand();
//                        coverCmd.Transaction = transaction;

//                        coverCmd.CommandText = @"
//                            INSERT INTO COVER_TABLE (
//                                VEHICLE_ID, COVER_CODE, TYPE, COVVALUE
//                            ) VALUES (
//                                :VEHICLE_ID, :COVER_CODE, :TYPE, :COVVALUE
//                            )";

//                        coverCmd.Parameters.Add(new OracleParameter("VEHICLE_ID", vehicleId));
//                        coverCmd.Parameters.Add(new OracleParameter("COVER_CODE", cover.COVER_CODE));
//                        coverCmd.Parameters.Add(new OracleParameter("TYPE", cover.TYPE));
//                        coverCmd.Parameters.Add(new OracleParameter("COVVALUE", cover.COVVALUE));

//                        coverCmd.ExecuteNonQuery();
//                    }
//                }

//                transaction.Commit();
//            }
//            catch
//            {
//                transaction.Rollback();
//                throw;
//            }
//        }
//    }
//}
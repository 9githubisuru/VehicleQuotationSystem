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


using Oracle.ManagedDataAccess.Client;
using QuotationAPI.Data;
using QuotationAPI.Models;
using System.Diagnostics;

namespace QuotationAPI.Repositories
{
    public class QuotationRepository
    {
        private readonly OracleDbContext _context;

      
        public QuotationRepository(OracleDbContext context)
        {
            _context = context;
        }

        public void SaveQuotation(List<Vehicle> vehicles)
        {
            using var connection = _context.GetConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {

                Debug.WriteLine("Start Save Quotations");
                foreach (var vehicle in vehicles)
                {
                    Debug.WriteLine($"Vehicle: {vehicle.FDVNO1}-{vehicle.FDVNO2}");
                    Debug.WriteLine("COVER COUNT: " + vehicle.Covers?.Count);
                    // =========================
                    // INSERT VEHICLE
                    // =========================
                    var vehicleCmd = connection.CreateCommand();
                    vehicleCmd.Transaction = transaction;

                    vehicleCmd.CommandText = @"
                    INSERT INTO FLEET.T_VEHICLE (
                        FDVNO1, FDVNO2, FDPROV, FDVEHIID
                    ) VALUES (
                        :FDVNO1, :FDVNO2, :FDPROV, :FDVEHIID
                    )";

                    vehicleCmd.Parameters.Add(new OracleParameter("FDVNO1", vehicle.FDVNO1?.Trim()));
                    vehicleCmd.Parameters.Add(new OracleParameter("FDVNO2", vehicle.FDVNO2?.Trim()));
                    vehicleCmd.Parameters.Add(new OracleParameter("FDPROV", vehicle.FDPROV));
                    vehicleCmd.Parameters.Add(new OracleParameter("FDVEHIID", vehicle.FDVEHIID));

                    //transaction.Commit();

                    //var outParam = new OracleParameter("OUT_ID", OracleDbType.Int32)
                    //{
                    //    Direction = System.Data.ParameterDirection.Output
                    //};

                    //vehicleCmd.Parameters.Add(outParam);

                    vehicleCmd.ExecuteNonQuery();   

                    //int vehicleId = Convert.ToInt32(outParam.Value.ToString());

                    // =========================
                    // INSERT COVERS
                    // =========================
                    foreach (var cover in vehicle.Covers)
                    {
                        Debug.WriteLine($"Cover: {cover.COVER_CODE}");

                        var isSelected = cover.IS_SELECTED?.Trim().ToLower();

                        if (isSelected != "true" && isSelected != "1" && isSelected != "y")
                            continue;


                        //Console.WriteLine($"Cover: {cover.COVER_CODE}, IS_SELECTED: '{cover.IS_SELECTED}'");
                        var coverCmd = connection.CreateCommand();
                        coverCmd.Transaction = transaction;

                        coverCmd.CommandText = @"INSERT INTO FLEET.T_VEHICLE_COVER
                    (FDVNO1, FDVNO2, COVER_CODE, TYPE, COVVALUE, IS_SELECTED)
                    VALUES (:FDVNO1, :FDVNO2, :COVER_CODE, :TYPE, :COVVALUE, :IS_SELECTED)";

                        coverCmd.Parameters.Add(new OracleParameter("FDVNO1", vehicle.FDVNO1));
                        coverCmd.Parameters.Add(new OracleParameter("FDVNO2", vehicle.FDVNO2));
                        coverCmd.Parameters.Add(new OracleParameter("COVER_CODE", cover.COVER_CODE));
                        coverCmd.Parameters.Add(new OracleParameter("TYPE", cover.TYPE));
                        coverCmd.Parameters.Add(new OracleParameter("COVVALUE", cover.COVVALUE));
                        coverCmd.Parameters.Add(new OracleParameter("IS_SELECTED", cover.IS_SELECTED));

                        Debug.WriteLine("BEFORE INSERT");
                        coverCmd.ExecuteNonQuery();
                        Debug.WriteLine("AFTER INSERT");
                    }

                   
                }
                    transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("DB Insert Failed: " + ex.Message);

                //System.Exception: 'DB Insert Failed: ORA-00942: table or view does not exist
            }
        }
    }
}
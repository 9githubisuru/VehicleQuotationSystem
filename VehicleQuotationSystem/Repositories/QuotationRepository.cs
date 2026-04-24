
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

                    //Insert Vehicle Details 
                    vehicleCmd.ExecuteNonQuery();

                    // =========================
                    // INSERT COVERS
                    // =========================
                    foreach (var cover in vehicle.Covers)
                    {
                        try {
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
                        catch(Exception ex)
                        {

                        }
                       
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


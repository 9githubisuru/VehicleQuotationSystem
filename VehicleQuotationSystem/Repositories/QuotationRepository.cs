
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
                        FDVNO1, FDVNO2, FDPROV, FDTPROV, FDTVNO1, FDTVNO2,
                        FDCHANO, POLICYTYPENAME, POLID, FDVEHICLECATEGORY,
                        FDVEHITYPE, FDVEHIID, SUBCATE, FDBUSITYPE,
                        FDPERIOD, FDPERIODID, FDDAYS, FDVEHITRAIL, FDSINGVEHI,
                        FDYEAR, FDISPOLFEE, FDPERMENTVEH, FDVEHCATEG,
                        FDVEHVAL, FDTRAVAL, ISINCLTAX, EXCLUDTAXTYPE,
                        FDNUMPASSEN, ISNEWDUPQUOT
                    ) VALUES (
                        :FDVNO1, :FDVNO2, :FDPROV, :FDTPROV, :FDTVNO1, :FDTVNO2,
                        :FDCHANO, :POLICYTYPENAME, :POLID, :FDVEHICLECATEGORY,
                        :FDVEHITYPE, :FDVEHIID, :SUBCATE, :FDBUSITYPE,
                        :FDPERIOD, :FDPERIODID, :FDDAYS, :FDVEHITRAIL, :FDSINGVEHI,
                        :FDYEAR, :FDISPOLFEE, :FDPERMENTVEH, :FDVEHCATEG,
                        :FDVEHVAL, :FDTRAVAL, :ISINCLTAX, :EXCLUDTAXTYPE,
                        :FDNUMPASSEN, :ISNEWDUPQUOT
                    )";

                    vehicleCmd.Parameters.Add("FDVNO1", vehicle.FDVNO1?.Trim());
                    vehicleCmd.Parameters.Add("FDVNO2", vehicle.FDVNO2?.Trim());
                    vehicleCmd.Parameters.Add("FDPROV", vehicle.FDPROV);
                    vehicleCmd.Parameters.Add("FDTPROV", vehicle.FDTPROV);
                    vehicleCmd.Parameters.Add("FDTVNO1", vehicle.FDTVNO1);
                    vehicleCmd.Parameters.Add("FDTVNO2", vehicle.FDTVNO2);
                    vehicleCmd.Parameters.Add("FDCHANO", vehicle.FDCHANO);
                    vehicleCmd.Parameters.Add("POLICYTYPENAME", vehicle.POLICYTYPENAME);
                    vehicleCmd.Parameters.Add("POLID", vehicle.POLID);
                    vehicleCmd.Parameters.Add("FDVEHICLECATEGORY", vehicle.FDVEHICLECATEGORY);
                    vehicleCmd.Parameters.Add("FDVEHITYPE", vehicle.FDVEHITYPE);
                    vehicleCmd.Parameters.Add("FDVEHIID", vehicle.FDVEHIID);
                    vehicleCmd.Parameters.Add("SUBCATE", vehicle.SUBCATE);
                    vehicleCmd.Parameters.Add("FDBUSITYPE", vehicle.FDBUSITYPE);
                    vehicleCmd.Parameters.Add("FDPERIOD", vehicle.FDPERIOD);
                    vehicleCmd.Parameters.Add("FDPERIODID", vehicle.FDPERIODID);
                    vehicleCmd.Parameters.Add("FDDAYS", vehicle.FDDAYS);
                    vehicleCmd.Parameters.Add("FDVEHITRAIL", vehicle.FDVEHITRAIL);
                    vehicleCmd.Parameters.Add("FDSINGVEHI", vehicle.FDSINGVEHI);
                    vehicleCmd.Parameters.Add("FDYEAR", vehicle.FDYEAR);
                    vehicleCmd.Parameters.Add("FDISPOLFEE", vehicle.FDISPOLFEE);
                    vehicleCmd.Parameters.Add("FDPERMENTVEH", vehicle.FDPERMENTVEH);
                    vehicleCmd.Parameters.Add("FDVEHCATEG", vehicle.FDVEHCATEG);
                    vehicleCmd.Parameters.Add("FDVEHVAL", vehicle.FDVEHVAL);
                    vehicleCmd.Parameters.Add("FDTRAVAL", vehicle.FDTRAVAL);
                    vehicleCmd.Parameters.Add("ISINCLTAX", vehicle.ISINCLTAX);
                    vehicleCmd.Parameters.Add("EXCLUDTAXTYPE", vehicle.EXCLUDTAXTYPE);
                    vehicleCmd.Parameters.Add("FDNUMPASSEN", vehicle.FDNUMPASSEN);
                    vehicleCmd.Parameters.Add("ISNEWDUPQUOT", vehicle.ISNEWDUPQUOT);

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


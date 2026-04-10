//using OfficeOpenXml;
//using QuotationAPI.Models;

//namespace QuotationAPI.Services
//{
//    public class ExcelService
//    {
//        public QuotationResponse ReadExcel(IFormFile file)
//        {
//            // Set the license context using the new EPPlus 8+ API
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

//            using var stream = new MemoryStream();
//            file.CopyTo(stream);

//            using var package = new ExcelPackage(stream);

//            var response = new QuotationResponse
//            {
//                Vehicles = new List<Vehicle>(),
//                Covers = new List<Cover>()
//            };

//            // =========================
//            // SHEET 1 → VEHICLES
//            // =========================
//            var vehicleSheet = package.Workbook.Worksheets[0];
//            int vRows = vehicleSheet.Dimension.Rows;

//            for (int row = 2; row <= vRows; row++)
//            {
//                var v = new Vehicle
//                {
//                    FDVNO1 = vehicleSheet.Cells[row, 1].Text,
//                    FDVNO2 = vehicleSheet.Cells[row, 2].Text,
//                    FDPROV = vehicleSheet.Cells[row, 3].Text,
//                    FDTPROV = vehicleSheet.Cells[row, 4].Text,
//                    FDTVNO1 = vehicleSheet.Cells[row, 5].Text,
//                    FDTVNO2 = vehicleSheet.Cells[row, 6].Text,
//                    FDCHANO = vehicleSheet.Cells[row, 7].Text,
//                    POLICYTYPENAME = vehicleSheet.Cells[row, 8].Text,
//                    POLID = vehicleSheet.Cells[row, 9].Text,
//                    FDVEHICLECATEGORY = vehicleSheet.Cells[row, 10].Text,
//                    FDVEHITYPE = vehicleSheet.Cells[row, 11].Text,
//                    FDVEHIID = vehicleSheet.Cells[row, 12].Text,
//                    SUBCATE = vehicleSheet.Cells[row, 13].Text,
//                    FDBUSITYPE = vehicleSheet.Cells[row, 14].Text,
//                    FDPERIOD = vehicleSheet.Cells[row, 15].Text,
//                    FDPERIODID = vehicleSheet.Cells[row, 16].Text,
//                    FDDAYS = vehicleSheet.Cells[row, 17].Text,
//                    FDVEHITRAIL = vehicleSheet.Cells[row, 18].Text,
//                    FDSINGVEHI = vehicleSheet.Cells[row, 19].Text,
//                    FDYEAR = vehicleSheet.Cells[row, 20].Text,
//                    FDISPOLFEE = vehicleSheet.Cells[row, 21].Text,
//                    FDPERMENTVEH = vehicleSheet.Cells[row, 22].Text,
//                    FDVEHCATEG = vehicleSheet.Cells[row, 23].Text,
//                    FDVEHVAL = vehicleSheet.Cells[row, 24].Text,
//                    FDTRAVAL = vehicleSheet.Cells[row, 25].Text,
//                    ISINCLTAX = vehicleSheet.Cells[row, 26].Text,
//                    EXCLUDTAXTYPE = vehicleSheet.Cells[row, 27].Text,
//                    FDNUMPASSEN = vehicleSheet.Cells[row, 28].Text,
//                    ISNEWDUPQUOT = vehicleSheet.Cells[row, 29].Text
//                };

//                if (!string.IsNullOrWhiteSpace(v.FDVNO1))
//                    response.Vehicles.Add(v);
//            }

//            // =========================
//            // SHEET 2 → COVERS
//            // =========================
//            var coverSheet = package.Workbook.Worksheets[1];
//            int cRows = coverSheet.Dimension.Rows;

//            for (int row = 2; row <= cRows; row++)
//            {
//                var c = new Cover
//                {
//                    CoverName = coverSheet.Cells[row, 1].Text,
//                    COVER_CODE = coverSheet.Cells[row, 2].Text,
//                    TYPE = coverSheet.Cells[row, 3].Text,
//                    COVVALUE = coverSheet.Cells[row, 4].Text,
//                    COVPREC = coverSheet.Cells[row, 5].Text,
//                    MAXVAL = coverSheet.Cells[row, 6].Text,
//                    NOOFPASS = coverSheet.Cells[row, 7].Text,
//                    IS_SELECTED = coverSheet.Cells[row, 8].Text,
//                    RATECODE = coverSheet.Cells[row, 9].Text
//                };

//                if (!string.IsNullOrWhiteSpace(c.COVER_CODE))
//                    response.Covers.Add(c);
//            }

//            return response;
//        }
//    }
//}



//using OfficeOpenXml;
//using QuotationAPI.Models;

//namespace QuotationAPI.Services
//{
//    public class ExcelService
//    {
//        public QuotationResponse ReadExcel(IFormFile file)
//        {
//            // ✅ EPPlus 8+ FIX
//            ExcelPackage.License.SetNonCommercialPersonal("Isuru Lakmal");

//            using var stream = new MemoryStream();
//            file.CopyTo(stream);

//            using var package = new ExcelPackage(stream);

//            var response = new QuotationResponse
//            {
//                Vehicles = new List<Vehicle>(),
//                //Covers = new List<Cover>()
//            };

//            var vehicleSheet = package.Workbook.Worksheets[0];
//            int vRows = vehicleSheet.Dimension.Rows;

//            for (int row = 2; row <= vRows; row++)
//            {
//                var v = new Vehicle
//                {
//                    FDVNO1 = vehicleSheet.Cells[row, 1].Text,
//                    FDVNO2 = vehicleSheet.Cells[row, 2].Text,
//                    FDPROV = vehicleSheet.Cells[row, 3].Text,
//                    FDTPROV = vehicleSheet.Cells[row, 4].Text,
//                    FDTVNO1 = vehicleSheet.Cells[row, 5].Text,
//                    FDTVNO2 = vehicleSheet.Cells[row, 6].Text,
//                    FDCHANO = vehicleSheet.Cells[row, 7].Text,
//                    POLICYTYPENAME = vehicleSheet.Cells[row, 8].Text,
//                    POLID = vehicleSheet.Cells[row, 9].Text,
//                    FDVEHICLECATEGORY = vehicleSheet.Cells[row, 10].Text,
//                    FDVEHITYPE = vehicleSheet.Cells[row, 11].Text,
//                    FDVEHIID = vehicleSheet.Cells[row, 12].Text,
//                    SUBCATE = vehicleSheet.Cells[row, 13].Text,
//                    FDBUSITYPE = vehicleSheet.Cells[row, 14].Text,
//                    FDPERIOD = vehicleSheet.Cells[row, 15].Text,
//                    FDPERIODID = vehicleSheet.Cells[row, 16].Text,
//                    FDDAYS = vehicleSheet.Cells[row, 17].Text,
//                    FDVEHITRAIL = vehicleSheet.Cells[row, 18].Text,
//                    FDSINGVEHI = vehicleSheet.Cells[row, 19].Text,
//                    FDYEAR = vehicleSheet.Cells[row, 20].Text,
//                    FDISPOLFEE = vehicleSheet.Cells[row, 21].Text,
//                    FDPERMENTVEH = vehicleSheet.Cells[row, 22].Text,
//                    FDVEHCATEG = vehicleSheet.Cells[row, 23].Text,
//                    FDVEHVAL = vehicleSheet.Cells[row, 24].Text,
//                    FDTRAVAL = vehicleSheet.Cells[row, 25].Text,
//                    ISINCLTAX = vehicleSheet.Cells[row, 26].Text,
//                    EXCLUDTAXTYPE = vehicleSheet.Cells[row, 27].Text,
//                    FDNUMPASSEN = vehicleSheet.Cells[row, 28].Text,
//                    ISNEWDUPQUOT = vehicleSheet.Cells[row, 29].Text
//                };

//                if (!string.IsNullOrWhiteSpace(v.FDVNO1))
//                    response.Vehicles.Add(v);
//            }

//            var coverSheet = package.Workbook.Worksheets[1];
//            int cRows = coverSheet.Dimension.Rows;

//            for (int row = 2; row <= cRows; row++)
//            {
//                var c = new Cover
//                {
//                    CoverName = coverSheet.Cells[row, 1].Text,
//                    COVER_CODE = coverSheet.Cells[row, 2].Text,
//                    TYPE = coverSheet.Cells[row, 3].Text,
//                    COVVALUE = coverSheet.Cells[row, 4].Text,
//                    COVPREC = coverSheet.Cells[row, 5].Text,
//                    MAXVAL = coverSheet.Cells[row, 6].Text,
//                    NOOFPASS = coverSheet.Cells[row, 7].Text,
//                    IS_SELECTED = coverSheet.Cells[row, 8].Text,
//                    RATECODE = coverSheet.Cells[row, 9].Text
//                };

//                if (!string.IsNullOrWhiteSpace(c.COVER_CODE))
//                    response.Covers.Add(c);
//            }

//            return response;
//        }
//    }
//}

using OfficeOpenXml;
using QuotationAPI.Models;

namespace QuotationAPI.Services
{
    public class ExcelService
    {
        public QuotationResponse ReadExcel(IFormFile file)
        {
            ExcelPackage.License.SetNonCommercialPersonal("Isuru Lakmal");

            using var stream = new MemoryStream();
            file.CopyTo(stream);

            using var package = new ExcelPackage(stream);

            var response = new QuotationResponse
            {
                Vehicles = new List<Vehicle>()
            };

            // =========================
            // STEP 1: READ ALL COVERS
            // =========================
            var coverSheet = package.Workbook.Worksheets[1];
            int cRows = coverSheet.Dimension.Rows;

            var allCovers = new List<Cover>();

            for (int row = 2; row <= cRows; row++)
            {
                var c = new Cover
                {
                    CoverName = coverSheet.Cells[row, 1].Text,
                    COVER_CODE = coverSheet.Cells[row, 2].Text,
                    TYPE = coverSheet.Cells[row, 3].Text,
                    COVVALUE = coverSheet.Cells[row, 4].Text,
                    COVPREC = coverSheet.Cells[row, 5].Text,
                    MAXVAL = coverSheet.Cells[row, 6].Text,
                    NOOFPASS = coverSheet.Cells[row, 7].Text,
                    IS_SELECTED = coverSheet.Cells[row, 8].Text,
                    RATECODE = coverSheet.Cells[row, 9].Text
                };

                if (!string.IsNullOrWhiteSpace(c.COVER_CODE))
                    allCovers.Add(c);
            }

            // =========================
            // STEP 2: READ VEHICLES
            // =========================
            var vehicleSheet = package.Workbook.Worksheets[0];
            int vRows = vehicleSheet.Dimension.Rows;

            for (int row = 2; row <= vRows; row++)
            {
                var v = new Vehicle
                {
                    FDVNO1 = vehicleSheet.Cells[row, 1].Text,
                    FDVNO2 = vehicleSheet.Cells[row, 2].Text,
                    FDPROV = vehicleSheet.Cells[row, 3].Text,
                    FDTPROV = vehicleSheet.Cells[row, 4].Text,
                    FDTVNO1 = vehicleSheet.Cells[row, 5].Text,
                    FDTVNO2 = vehicleSheet.Cells[row, 6].Text,
                    FDCHANO = vehicleSheet.Cells[row, 7].Text,
                    POLICYTYPENAME = vehicleSheet.Cells[row, 8].Text,
                    POLID = vehicleSheet.Cells[row, 9].Text,
                    FDVEHICLECATEGORY = vehicleSheet.Cells[row, 10].Text,
                    FDVEHITYPE = vehicleSheet.Cells[row, 11].Text,
                    FDVEHIID = vehicleSheet.Cells[row, 12].Text,
                    SUBCATE = vehicleSheet.Cells[row, 13].Text,
                    FDBUSITYPE = vehicleSheet.Cells[row, 14].Text,
                    FDPERIOD = vehicleSheet.Cells[row, 15].Text,
                    FDPERIODID = vehicleSheet.Cells[row, 16].Text,
                    FDDAYS = vehicleSheet.Cells[row, 17].Text,
                    FDVEHITRAIL = vehicleSheet.Cells[row, 18].Text,
                    FDSINGVEHI = vehicleSheet.Cells[row, 19].Text,
                    FDYEAR = vehicleSheet.Cells[row, 20].Text,
                    FDISPOLFEE = vehicleSheet.Cells[row, 21].Text,
                    FDPERMENTVEH = vehicleSheet.Cells[row, 22].Text,
                    FDVEHCATEG = vehicleSheet.Cells[row, 23].Text,
                    FDVEHVAL = vehicleSheet.Cells[row, 24].Text,
                    FDTRAVAL = vehicleSheet.Cells[row, 25].Text,
                    ISINCLTAX = vehicleSheet.Cells[row, 26].Text,
                    EXCLUDTAXTYPE = vehicleSheet.Cells[row, 27].Text,
                    FDNUMPASSEN = vehicleSheet.Cells[row, 28].Text,
                    ISNEWDUPQUOT = vehicleSheet.Cells[row, 29].Text,

                    // ✅ Attach covers
                    Covers = allCovers.Select(c => new Cover
                    {
                        CoverName = c.CoverName,
                        COVER_CODE = c.COVER_CODE,
                        TYPE = c.TYPE,
                        COVVALUE = c.COVVALUE,
                        COVPREC = c.COVPREC,
                        MAXVAL = c.MAXVAL,
                        NOOFPASS = c.NOOFPASS,
                        IS_SELECTED = c.IS_SELECTED,
                        RATECODE = c.RATECODE
                    }).ToList()
                };

                if (!string.IsNullOrWhiteSpace(v.FDVNO1))
                    response.Vehicles.Add(v);
            }

            return response;
        }
    }
}
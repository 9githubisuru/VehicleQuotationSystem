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
            stream.Position = 0; // reset

            using var package = new ExcelPackage(stream);
            var coversSheet = package.Workbook.Worksheets["Covers"] ?? package.Workbook.Worksheets[2];

            if (coversSheet?.Dimension == null)
                throw new InvalidOperationException("Covers sheet is empty or missing required data.");

            var response = new QuotationResponse
            {
                Vehicles = new List<Vehicle>()
            };

            // =========================
            // STEP 1: READ ALL COVERS 
            // =========================
        
            int cRows = coversSheet.Dimension.Rows;

            var allCovers = new List<Cover>();

            for (int row = 2; row <= cRows; row++)
            {
                var c = new Cover
                {
                    FDVN01 = coversSheet.Cells[row, 1].Text,
                    FDVN02 = coversSheet.Cells[row, 2].Text,    
                    CoverName = coversSheet.Cells[row, 3].Text,
                    COVER_CODE = coversSheet.Cells[row, 4].Text,
                    TYPE = coversSheet.Cells[row, 5].Text,
                    COVVALUE = coversSheet.Cells[row, 6].Text,
                    COVPREC = coversSheet.Cells[row, 7].Text,
                    MAXVAL = coversSheet.Cells[row, 8].Text,
                    NOOFPASS = coversSheet.Cells[row, 9].Text,
                    IS_SELECTED = coversSheet.Cells[row, 10].Text,
                    RATECODE = coversSheet.Cells[row, 11].Text
                };

                if (!string.IsNullOrWhiteSpace(c.COVER_CODE))
                    allCovers.Add(c);
            }

            // =========================
            // STEP 2: READ VEHICLES
            // =========================
            var vehicleSheet = package.Workbook.Worksheets["Vehicles"];
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
                        FDVN01 = c.FDVN01,
                        FDVN02 = c.FDVN02,
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMTech.Utilities
{
    internal class Msg
    {
        public const string LossDataNotFoundInELT = "Couldn't find loss data from ELT database.";
        public const string EventNotFoundInCvTable = "Couldn't find Event ID in IND CV Lookup table";
        public const string InvalidPerspective = "Specified invalid perspective, please select GU or DR for EQE database.";
        public const string RebuildingDatabaseIndexError = "Error occured while rebuilding database indexes: ";
        public const string ExposureDataNotFound = "Exposure data not found.";
        public const string FailedConnectionToSql = "Failed to create SQL database connection. /n/n Error Message: ";
        public const string FailedConnectionToOledb = "Failed to create Oledb database connection. /n/n Error Message: ";
    }
}

using System;
using System.Data;

namespace rulesmngt.Utils {

    public class DataUtils {
        internal static string getStringValue(DataRow row, string field)
        {
            if (row[field] != null)
            {
                return row[field].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        internal static int getIntValue(DataRow row, string field)
        {
            if (row[field] != null && row[field].ToString() != string.Empty)
            {
                return Int32.Parse(row[field].ToString());
            }
            else
            {
                return 0;
            }
        }


        internal static int? getIntNValue(DataRow row, string field)
        {
            if (row[field] != null && row[field].ToString() != string.Empty)
            {
                return Int32.Parse(row[field].ToString());
            }
            else
            {
                return null;
            }
        }

        internal static decimal getDecimalValue(DataRow row, string field)
        {
            if (row[field] != null && row[field].ToString() != string.Empty)
            {
                return decimal.Parse(row[field].ToString());
            }
            else
            {
                return 0;
            }
        }

        internal static decimal? getDecimalNValue(DataRow row, string field)
        {
            if (row[field] != null && row[field].ToString() != string.Empty)
            {
                return decimal.Parse(row[field].ToString());
            }
            else
            {
                return null;
            }
        }

        internal static DateTime getDateTimeValue(DataRow row, string field)
        {
            if (row[field] != null && row[field].ToString() != string.Empty)
            {
                return DateTime.Parse(row[field].ToString());
            }
            else
            {
                return DateTime.MinValue;
            }
        }
    }


}
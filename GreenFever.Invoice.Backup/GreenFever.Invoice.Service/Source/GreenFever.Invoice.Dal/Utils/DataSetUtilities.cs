using System;
using System.Data;

namespace GreenFever.Invoice.Dal.Utils
{
    public class DataSetUtilities
    {
        public static string GetDataItemString(DataRow dr, string field)
        {
            if (dr.Table.Columns.Contains(field) && dr[field] != null && dr[field] != DBNull.Value)
            {
                return dr[field].ToString();
            }

            return null;
        }

        public static long? GetDataItemLong(DataRow dr, string field)
        {
            if (dr.Table.Columns.Contains(field) && dr[field] != null && dr[field] != DBNull.Value)
            {
                return Convert.ToInt64(dr[field]);
            }
            
            return null;
        }

        public static int? GetDataItemInteger(DataRow dr, string field)
        {
            if (dr.Table.Columns.Contains(field) && dr[field] != null && dr[field] != DBNull.Value)
            {
                return Convert.ToInt32(dr[field]);
            }

            return null;
        }

        public static DateTime? GetDataItemDateTime(DataRow dr, string field)
        {
            if (dr.Table.Columns.Contains(field) && dr[field] != null && dr[field] != DBNull.Value)
            {
                return Convert.ToDateTime(dr[field]);
            }

            return null;
        }

        public static decimal? GetDataItemDecimal(DataRow dr, string field)
        {
            if (dr.Table.Columns.Contains(field) && dr[field] != null && dr[field] != DBNull.Value)
            {
                return Convert.ToDecimal(dr[field]);
            }

            return null;
        }

        public static bool GetDataItemBoolean(DataRow dr, string field)
        {
            if (dr.Table.Columns.Contains(field) && dr[field] != null && dr[field] != DBNull.Value)
            {
                return Convert.ToBoolean(dr[field]);
            }

            return false;
        }
    }
}

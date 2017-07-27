using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
   public static class BaseHelpBLL
    {
        private static string FormatDateToDataBasePatern(DateTime data)
        {
            string data_ = "";
            data_ += data.Year + "-";

            if (data.Month + "".Length == 1)
            {
                data_ += "0" + data.Month + "-";
            }
            else
            {

                data_ += data.Month + "-";
            }

            if (data.Day + "".Length == 1)
            {
                data_ += "0" + data.Day;
            }
            else
            {
                data_ += data.Day + "";
            }

            return data_;
        }

        public static string DateToInsert_or_UpdateDatabse(DateTime data)
        {
            return $"TO_DATE('{FormatDateToDataBasePatern(data)}', 'YYYY-MM-DD')";
        }

        public static string FormatDateTimeToDataBasePatern(DateTime data)
        {
            return data.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}

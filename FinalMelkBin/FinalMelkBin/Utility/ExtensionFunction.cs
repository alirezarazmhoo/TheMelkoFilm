using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace System
{
    public static class ExtensionFunction
    {
        public static String ToPersian(this DateTime dt)
        {
         
            PersianCalendar p = new PersianCalendar();
            int day = p.GetDayOfMonth(dt);
            int month = p.GetMonth(dt);
            int year = p.GetYear(dt);

            return year+"/"+month+"/"+day;
        }
        public static DateTime shamsitomiladi(this DateTime dt)
        {
            PersianCalendar _persian = new PersianCalendar();
            int _year = dt.Year;
            int _month = dt.Month;
            int _day = dt.Day;
            int _hour = dt.Hour;
            int _min = dt.Minute;
            int _sec = dt.Second;
            int _milisec = dt.Millisecond;
            return _persian.ToDateTime(_year, _month, _day, _hour, _min, _sec, _milisec);
        }
    }
}
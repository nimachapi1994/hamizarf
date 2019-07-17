using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HamiZarf.CommonMethods
{
    public class CalendarManager
    {
        public static string EngToPersian(DateTime dt)
        {
            PersianCalendar pcal = new PersianCalendar();
            return $"{pcal.GetYear(dt)}/{pcal.GetMonth(dt)}/{pcal.GetDayOfMonth(dt)}";
        }
    }
}
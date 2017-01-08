using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace Lesson6
{
    public class Base
    {
        protected static bool IsListSortedAsc(IEnumerable<string> listToCheck)
        {
            var l = new List<string>(listToCheck);
            l.Sort();
            return l.SequenceEqual(listToCheck);
        }

        protected static string Argb2RgbaFormat(Color c)
        {
            return String.Format("rgba({0}, {1}, {2}, {3})", c.R, c.G, c.B, 1);  // вместо c.A пришслось вписать 1
        }
    }
}

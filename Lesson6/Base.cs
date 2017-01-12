using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using OpenQA.Selenium;


namespace Lesson6
{
    public class Base
    {
        public static bool IsListSortedAsc(IEnumerable<string> listToCheck)
        {
            var l = new List<string>(listToCheck);
            l.Sort();
            return l.SequenceEqual(listToCheck);
        }

        public static string Argb2RgbaFormat(Color c)
        {
            return String.Format("rgba({0}, {1}, {2}, {3})", c.R, c.G, c.B, 1);  // вместо c.A пришслось вписать 1
        }

        public static void SendKeys(IWebElement element, string keys)
        {
            element.Clear();
            element.SendKeys(keys);
        }

        public static string GetRandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Lesson3.PageObjects;
using Lesson5;
using Lesson5.PageObjects.Admin;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Lesson5
{
    public class Base
    {

        public bool IsListSortedAsc(List<string> listToCheck)
        {
            var l = new List<string>(listToCheck);
            l.Sort();
            return l.SequenceEqual(listToCheck);
        }

    }
}

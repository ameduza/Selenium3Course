using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lesson5
{
    public class Product
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string PriceRegular { get; set; }
        public string PriceRegularColor { get; set; }
        public string PriceRegularStyle { get; set; }
        public float PriceRegularSize { get; set; }
        public string PriceCampaign { get; set; }
        public string PriceCampaignColor { get; set; }
        public string PriceCampaignStyle { get; set; }
        public float PriceCampaignSize { get; set; }

    }
}

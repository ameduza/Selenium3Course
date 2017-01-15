using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lesson7
{
    public class Product
    {
        // General
        public string Status { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string[] Categories { get; set; }  // array?
        public string DefaultCategory { get; set; }
        public string[] ProductGroups { get; set; }   // array?
        public int Quantity { get; set; }
        public string SoldOutStatus { get; set; }
        public string[] ImagesUrls { get; set; }
        public string DateValidFrom { get; set; }
        public string DateValidTo { get; set; }
        // Information 
        public string Manufacturer { get; set; }
        public string Keywords { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string HeadTitle { get; set; }
        public string MetaDescription { get; set; }
        // Prices
        public float PurchasePriceValue { get; set; }
        public string PurchasePriceCurrency { get; set; }
        public float PriceUsd { get; set; }
        //public float PriceUsdGross { get; set; }
        public float PriceEur { get; set; }
        //public float PriceEurGross { get; set; }
        
        // For Store page:
        public string PriceRegularStyle { get; set; }
        public float PriceRegularSize { get; set; }
        public string PriceCampaign { get; set; }
        public string PriceCampaignColor { get; set; }
        public string PriceCampaignStyle { get; set; }
        public float PriceCampaignSize { get; set; }
        public string PriceRegular { get; set; }
        public string PriceRegularColor { get; set; }


    }
}

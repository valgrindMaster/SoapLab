using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace HaynesSoapLab
{
    /// <summary>
    /// Summary description for ProductService
    /// </summary>
    [WebService(Namespace = "http://haynes.soaplab.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to  be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ProductService : System.Web.Services.WebService
    {
        private Dictionary<string, double> products;

        public ProductService()
        {
            products = new Dictionary<string, double>();

            products.Add("Apples", 3.99);
            products.Add("Peaches", 4.05);
            products.Add("Pumpkin", 13.99);
            products.Add("Pie", 8.00);
        }

        private string firstCharToUpper(string input)
        {
            switch (input)
            {
                case null: return "Inappropriate argument provided.";
                case "": return "Inappropriate argument provided.";
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        [WebMethod]
        public string[] GetMethods()
        {
            return new string[] { "GetPrice", "GetProducts", "GetCheapest", "GetCostliest" };
        }

        [WebMethod]
        public double GetPrice(string productName)
        {
            string formattedStr = firstCharToUpper(productName);
            try
            {
                return products[formattedStr];
            } catch(KeyNotFoundException e)
            {
                return -1;
            }
        }

        [WebMethod]
        public string[] GetProducts()
        {
            return products.Keys.ToArray();
        }

        [WebMethod]
        public string GetCheapest()
        {
            //Default to none found.
            string productName = "No products found.";

            //Used to keep track of the cheapest product.
            double temp = -1;

            foreach(KeyValuePair<string, double> product in products)
            {
                if (temp == -1)
                {
                    productName = product.Key;
                    temp = product.Value;
                } else if (temp >= 0 && product.Value < temp)
                {
                    productName = product.Key;
                    temp = product.Value;
                }
            }

            return productName;
        }

        [WebMethod]
        public string GetCostliest()
        {
            //Default to none found.
            string productName = "No products found.";

            //Used to keep track of the costliest product.
            double temp = -1;

            foreach (KeyValuePair<string, double> product in products)
            {
                if (temp == -1)
                {
                    productName = product.Key;
                    temp = product.Value;
                }
                else if (temp >= 0 && product.Value > temp)
                {
                    productName = product.Key;
                    temp = product.Value;
                }
            }

            return productName;
        }
    }
}
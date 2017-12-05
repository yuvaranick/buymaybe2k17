using SampleAuth1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using UrlGenaration;

namespace SampleAuth1.DBService
{
    public class SeedMyDb
    {

        public void FillDB(ApplicationDbContext context)
        {
            string[] Catgry = new string[] { "1000", "493964", "2619526011", "165797011", "165795011" };

            //define column of data table
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ASIN", typeof(string)));
            dt.Columns.Add(new DataColumn("DetailPageURL", typeof(string)));
            dt.Columns.Add(new DataColumn("LargeImage", typeof(string)));
            dt.Columns.Add(new DataColumn("Title", typeof(string)));
            dt.Columns.Add(new DataColumn("Brand", typeof(string)));
            dt.Columns.Add(new DataColumn("Category", typeof(int)));
            dt.Columns.Add(new DataColumn("OfferListingId", typeof(string)));
            dt.Columns.Add(new DataColumn("OtherInfo", typeof(string)));



            //setup browseNode and ItemPage
            GenerateURL url = new GenerateURL();

            foreach (string category in Catgry)
            {
                dt.Clear();
                url.BrowseNode = category;
                Int64 bs = Convert.ToInt64(url.BrowseNode);
                for (int i = 1; i < 11; i++)
                {
                    string XmlResponse;
                    XmlResponse = string.Empty;
                    url.ItemPage = i.ToString();
                    DbClient c = new DbClient() { EndPoint = url.GetURL() };
                    //Get Response
                    XmlResponse = c.MakeRequest();

                    //process Response if not null
                    if (!string.IsNullOrEmpty(XmlResponse))
                    {
                        XElement ItemSearchResponse = XElement.Parse(XmlResponse);
                        XNamespace ns = "http://webservices.amazon.com/AWSECommerceService/2011-08-01";
                        IEnumerable<XElement> Items = from node in ItemSearchResponse.Descendants(ns + "Item") select node;


                        //set data table column

                        foreach (var item in Items)
                        {
                            Product p = new Product();
                            p.ASIN = item.Element(ns + "ASIN").Value;
                            p.DetailPageURL = item.Element(ns + "DetailPageURL").Value;
                            p.LargeImage = item.Element(ns + "LargeImage")?.Element(ns + "URL")?.Value;
                            p.CategoryId = bs;
                            p.OfferListingId = item.Descendants(ns + "OfferListingId")?.FirstOrDefault()?.Value;
                            p.Title = item.Descendants(ns + "Title")?.FirstOrDefault()?.Value;
                            //put switch for category
                            p.OtherInfo = "Author: " + item.Descendants(ns + "Author")?.FirstOrDefault()?.Value; ;
                            // dt.Rows.Add(p.DetailPageURL, p.ASIN, p.LargeImage, p.Title, p.Brand, p.Category, p.OfferListingId, p.OtherInfo);
                            if (p.ASIN != null && p.DetailPageURL != null && p.LargeImage != null && p.Title != null)
                            {
                                context.Products.Add(p);
                            }
                            
                        }
                    }
                }//end of category

                //connection string
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
               

                // SleepTimer 10 seconds for Amazon Request
                System.Threading.Thread.Sleep(10000);
            }
        }
    }
}
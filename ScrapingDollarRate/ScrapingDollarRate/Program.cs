using System;
using HtmlAgilityPack;
using CsvHelper;
using System.Globalization;


namespace ScrapingDollarRate
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://www.banki.ru/products/currency/usd/";

            var web = new HtmlWeb();
            // downloading to the target page
            // and parsing its HTML content
            var document = web.Load(url);
            var nodes = document.DocumentNode.SelectNodes("//div[contains(@class, 'Flexbox__sc-wtbhrg-0 fEdnEg')]");

            List<Episode> episodes = new List<Episode>();

            // /html/body/div[1]/div[2]/div[1]/div/div/div/div[2]/div/div/div[2]/div/div[1]/section/div/div/div/div[2]

            // Panel__sc-1g68tnu-0 cieTjP
            // //div[contains(@class, 'Flexbox__sc-wtbhrg-0 fEdnEg')]

            // Flexbox__sc-wtbhrg-0 fEdnEg

            if (nodes == null)
            {
                Console.WriteLine("Null");
            }
            else
            {
                foreach (var node in nodes)
                {
                    // add a new Episode instance to
                    // to the list of scraped data
                    //Console.WriteLine("Null!");

                    episodes.Add(new Episode()
                    {
                        OverallNumber = HtmlEntity.DeEntitize(node.SelectSingleNode("//div[contains(@class, 'Text__sc-j452t5-0 hDxmZl')]").InnerText),
                        Title = HtmlEntity.DeEntitize(node.SelectSingleNode("//div[contains(@class, 'Text__sc-j452t5-0 bCCQWi')]").InnerText)
                    });
                }

                using (var writer = new StreamWriter("output.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    // populating the CSV file
                    csv.WriteRecords(episodes);
                }
            }
        }
    }
}
using Catalog.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Catalog // Note: actual namespace depends on the project name.
{
    public class Program
    {
        private static readonly JsonSerializerOptions _options =
         new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        public static async Task Main(string[] args)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                Delimiter=";"
            };
            try
            {
                var products = GetProducts("data/Products.csv", config);
                var categories = GetCategories("data/Categories.csv", config);

                var results = (from c in categories
                              
                              select new CatalogModel
                              { Id=c.Id, Name=c.Name, Description = c.Description, Products= GetCategoryProducts(products,c.Id) }).ToList();

                var jsonString = JsonSerializer.Serialize(results, _options);
                File.WriteAllText("../../../data/catalog_1.json", jsonString);
                // writing xml
                using (var stringwriter = new System.IO.StringWriter())
                {
                    var serializer = new XmlSerializer(results.GetType());
                    serializer.Serialize(stringwriter, results);
                    File.WriteAllText("../../../data/catalog_1.xml", stringwriter.ToString());
                    
                }
                Console.WriteLine("Json & XML files are generated sucuessfully! Press any key to close");
                Console.ReadLine();
                //return;
            }
            catch (Exception ex)
            {

                throw;
            }
            

        }


        private static List<Product> GetCategoryProducts(List<Product> products, int categoryId)
        {
            return products.Where(p => p.CategoryId == categoryId).ToList();
        }
        private static List<Product> GetProducts(string path, CsvConfiguration config)
        {
            var products = new List<Product>();
            using (var reader = new StreamReader(path))

            using (var csv = new CsvReader(reader, config))
            {
                products = csv.GetRecords<Product>().ToList();
            }
            return products;
        }

       private static List<Category> GetCategories(string path, CsvConfiguration config)
        {
            var categories = new List<Category>();
            using (var reader = new StreamReader(path))

            using (var csv = new CsvReader(reader, config))
            {
                categories = csv.GetRecords<Category>().ToList();
            }
            return categories;
        }
    }
}






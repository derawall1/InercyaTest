using CsvHelper;
using CsvHelper.Configuration;
using Customers.Models;
using EFCore.BulkExtensions;
using System.Globalization;

namespace Customers.Services
{
    public class CustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context)
        {
            _context = context;
        }
        public  List<Customer> GetCustomersFromCsv(string path)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                Delimiter = ";"
            };
            var customers = new List<Customer>();
            using (var reader = new StreamReader(path))

            using (var csv = new CsvReader(reader, config))
            {
                customers = csv.GetRecords<Customer>().ToList();
            }
            return customers;
        }

        public async Task SaveCustomers(List<Customer> custumers)
        {
            try
            {
                await _context.BulkInsertOrUpdateAsync(custumers);
                Console.WriteLine("Data has been saved successfully into the database!\n\n");
            }
            catch (Exception)
            {
                throw;

                
            }

          
            
        }
    }
}

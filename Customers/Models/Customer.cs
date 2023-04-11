using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Models;

[Table(name: "Customer")]
public class Customer
{
    [Key]
    public string Id { get; set; } = default;
    public string Name { get; set; } = default;
    public string Address { get; set; } = default;
    public string City { get; set; } = default;
    public string Country { get; set; } = default;
    public string PostalCode { get; set; } = default;
    public string Phone { get; set; } = default;
}

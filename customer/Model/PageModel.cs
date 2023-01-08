using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace customer.Model
{
    public class PageModel
    {
        public int CustomerCount { get; set; }
        public string Email { get; set; }
        public string Names { get; set; }
        public string ProductStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TransactionValue { get; set; }
        public string ShipmentDelivery { get; set; }
        public bool LocationStatus { get; set; }
        //Product List
        public List<Product> products { get; set; }
        public int cat { get; set; }

    }
}

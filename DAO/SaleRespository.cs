using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class SaleRespository
    {

        private readonly cargageEntities db;
        public SaleRespository()
        {
            db = new cargageEntities();
        }

        public List<Sales> Get()
        {
            return db.Sales.ToList();
        }
        public List<Sales> GetByUser(int usr)
        {
            return db.Sales.ToList().FindAll(x => x.users == usr);
        }
        public Sales Get(int id)
        {
            return db.Sales.FirstOrDefault(x => x.id == id);
        }

        public Sales Get(DateTime saledate)
        {
            return db.Sales.FirstOrDefault(x => x.sale_date == saledate);
        }

        public Sales Add(Sales sales)
        {
            if (sales == null)
                throw new ArgumentNullException(nameof(sales));

            sales = db.Sales.Add(sales);
            db.SaveChanges();

            return sales;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ProductRepository
    {
        private readonly cargageEntities db;
        public ProductRepository()
        {
            db = new cargageEntities();
        }
        public List<Product> Get()
        {
            return db.Product.ToList();
        }
        public Product GetByCategory(int category)
        {
            return db.Product.FirstOrDefault(x => x.category == category);
        }
        
        public Product Get(int id)
        {
            return db.Product.FirstOrDefault(x => x.id == id);
        }

        public Product Get(string nom)
        {
            return db.Product.FirstOrDefault(x => x.nom == nom);
        }

        public Product Get(string nom, int category)
        {
            var Product = Get(nom);
            if (Product?.category == category)
                return Product;
            return null;
        }

        public Product Add(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var u = Get(product.nom);
            if (u != null)
                throw new DuplicateWaitObjectException($"Product {product.nom} already exist !");

            product = db.Product.Add(product);
            db.SaveChanges();

            return product;
        }

        public Product Set(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var currentDb = new cargageEntities();
            var oldProduct = currentDb.Product.Find(product.id);

            if (oldProduct == null)
                throw new KeyNotFoundException($"Product not found !");

            var u = currentDb.Product.FirstOrDefault(x => x.nom == product.nom);
            if (u != null && u.id != oldProduct.id)
                throw new DuplicateWaitObjectException($"Product with Name : {product.nom} already exist !");

            db.Entry(product).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            return product;
        }
    }
}

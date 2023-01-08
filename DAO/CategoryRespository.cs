using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class CategoryRespository
    {

        private readonly cargageEntities db;
        public CategoryRespository()
        {
            db = new cargageEntities();
        }

        public List<Category> Get()
        {
            return db.Category.ToList();
        }
        public Category Get(int id)
        {
            return db.Category.FirstOrDefault(x => x.id == id);
        }

        public Category Get(string nom)
        {
            return db.Category.FirstOrDefault(x => x.nom == nom);
        }

        public Category Add(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            var u = Get(category.nom);
            if (u != null)
                throw new DuplicateWaitObjectException($"Category {category.nom} already exist !");

            category = db.Category.Add(category);
            db.SaveChanges();

            return category;
        }

        public Category Set(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            var currentDb = new cargageEntities();
            var oldCategory = currentDb.Category.Find(category.id);

            if (oldCategory == null)
                throw new KeyNotFoundException($"Category not found !");

            var u = currentDb.Category.FirstOrDefault(x => x.nom == category.nom);
            if (u != null && u.id != oldCategory.id)
                throw new DuplicateWaitObjectException($"Category with Name : {category.nom} already exist !");

            db.Entry(category).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            return category;
        }
    }
}

using Nerxeas.DataAccess.Repository.IRepository;
using Nerxeas.Models;

namespace Nerxeas.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj) // Here comes the custom Update method.
        {
            var objFromDb = _db.Products.FirstOrDefault( u => u.Id == obj.Id );
            if ( objFromDb != null )
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Author = obj.Author;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price = obj.Price;
                objFromDb.ListPrice = obj.ListPrice;

                if ( objFromDb.ImageUrl != null )
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }

            }
        }
    }
}

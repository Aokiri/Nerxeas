using System.Linq.Expressions;

namespace Nerxeas.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T => Category, so I have to implement these methods to work with Categories (CRUD)

        // 20.10.23 >>> Added an includeProperties to add if any, to populate that with EFCore.
        // Like Categories and CoverTypes, anything provided by the user.
        IEnumerable<T> GetAll(string? includeProperties = null);

        // This parameter is like this to work with LINQ Expressions.
        // We need to pass a boolean like that when we work with LINQ.
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);

        void Add(T entity);

        // The reason that there's no "Update" action in the generic repository
        // Is because it could have different logic between the types Category and Product.
        // E.g. When I'll want to update a Category it could save changes. But with products maybe I don't want to edit
        // The entire product at all. With EFCore we have the .Update() method to get it easier.

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
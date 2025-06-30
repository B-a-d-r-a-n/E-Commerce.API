
namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext context) : IUnitOfWork
    {
        private readonly Dictionary<String, object> _repositories = [];

        public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if(_repositories.ContainsKey(typeName)) return
                    (IGenericRepository<TEntity, TKey>) _repositories[typeName];

            var repo = new GenericRepository<TEntity, TKey>(context);
            _repositories.Add(typeName, repo);
            return repo;
        }

        public IGenericRepository<TEntity, int> GetRepository<TEntity>() where TEntity : BaseEntity<int>
        {
            var typeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(typeName)) return
                    (IGenericRepository<TEntity, int>)_repositories[typeName];

            var repo = new GenericRepository<TEntity, int>(context);
            _repositories.Add(typeName, repo);
            return repo;
        }
        // Container for the Created Repos [Dictionary]
        //Dictionary<TypeName , object>
        //GetRepository => Check if the Required Repo is already Created => Return Without Creating new Object
        // IF not WILL create new one and add it to the container and return the created repo
    }
}

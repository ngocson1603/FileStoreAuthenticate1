using Microsoft.EntityFrameworkCore;

namespace FileStore
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly Context Context;
        protected readonly DbSet<T> Dbset;   
        public Repository(Context context)
        {
            Context = context;  
            Dbset = Context.Set<T>();   
        }
        public void Create(T entity)
        {
            Context.Add(entity);
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
        }

        public T? Get(int id)
        {
            return Dbset.Find(id);
        }

        public List<T> GetAll()
        {
            return Dbset.ToList();
        }

        public void Update(T entity)
        {
            Context.Update(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}

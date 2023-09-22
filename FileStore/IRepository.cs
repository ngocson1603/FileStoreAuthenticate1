namespace FileStore
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T? Get(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

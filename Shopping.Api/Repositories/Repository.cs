using Microsoft.EntityFrameworkCore;

namespace Shopping.Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext Context;
        protected DbSet<T> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Deleted;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}

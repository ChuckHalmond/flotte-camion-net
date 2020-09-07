using System.Collections.Generic;

namespace TP3_NET.Dao
{
    public abstract class BaseDao<T>
    {
        protected readonly DatabaseContext DatabaseContext = DatabaseContext.GetInstance();
        public abstract T Create(T entity);
        public abstract T Update(int id, T entity);
        public abstract bool Remove(int id);
        public abstract List<T> GetList();
        public abstract T FindById(int id);
    }
}
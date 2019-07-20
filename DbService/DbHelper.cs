using DbInterface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DbService
{
    public class DbHelper : IDbInterface
    {
        private DbContext context;

        public DbHelper(DbContext context)
        {
            this.context = context;
        }


        public int Delete<T>(T t, bool shouldCommit = true) where T : class
        {
            if (t == null) throw new Exception("delete parameter is null");
            context.Set<T>().Attach(t);
            context.Set<T>().Remove(t);
            return Commit(shouldCommit);

        }

        public int DeleteWhere<T>(Expression<Func<T, bool>> expression, bool shouldCommit = true) where T : class
        {
            var items = Select<T>(expression);

            foreach (var item in items)
            {
                context.Set<T>().Remove(item);
            }

            return Commit(shouldCommit);
        }


        public int Insert<T>(T t, bool shouldCommit = true) where T : class
        {
            if (t == null) throw new Exception("Insert parameter is null");

            context.Set(typeof(T)).Add(t);
            return Commit(shouldCommit);
        }

        public IQueryable<T> Select<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return context.Set<T>().Where(expression);
        }

        public int Update<T>(T t, bool shouldCommit = true) where T : class
        {
            if (t == null) throw new Exception("update parameter is null");

            context.Set(typeof(T)).Attach(t);
            context.Entry(t).State = EntityState.Modified;
            return Commit(shouldCommit);
        }

        public int InsertRange<T>(IEnumerable<T> tList, bool shouldCommit = true) where T : class
        {
            context.Set<T>().AddRange(tList);
            return Commit(shouldCommit);
        }

        public IQueryable<T> SelectPage<T>(Expression<Func<T, bool>> expression, int pageSize, int pageNum, Expression<Func<T, bool>> orderExp = null, bool isAsc = true) where T : class
        {
            var list = Select(expression);

            if (orderExp != null)
            {
                if (isAsc)
                    list = list.OrderBy(orderExp);
                else
                    list = list.OrderByDescending(orderExp);
            }

            return list.Skip((pageNum - 1) * pageSize).Take(pageSize);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public T FindId<T>(int id) where T : class
        {
            PropertyInfo property = typeof(T).GetProperty("id");
            if (property == null) throw new Exception("类型不包含Id属性");
            if (property.PropertyType != typeof(int)) throw new Exception("Id属性不是int类型");

            return context.Set<T>().Find(id);
        }

        public int Delete<T>(int id, bool shouldCommit = true) where T : class
        {
            T t = FindId<T>(id);
            return Delete(t, shouldCommit);
        }

        public int DeleteRange<T>(IEnumerable<T> tList, bool shouldCommit = true) where T : class
        {
            context.Set<T>().RemoveRange(tList);
            return Commit(shouldCommit);
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        private int Commit(bool shouldCommit)
        {
            if (shouldCommit)
                return context.SaveChanges();
            else
                return 0;
        }

        public IQueryable<T> SelectAll<T>() where T : class
        {
            return context.Set<T>().AsQueryable();
        }
    }
}

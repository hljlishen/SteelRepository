using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbInterface
{
    public interface IDbInterface : IDisposable
    {
        int Insert<T>(T t, bool shouldCommit = true) where T : class;
        int InsertRange<T>(IEnumerable<T> tList, bool shouldCommit = true) where T : class;
        T FindId<T>(int id) where T : class;
        IQueryable<T> Select<T>(Expression<Func<T, bool>> expression) where T : class;
        IQueryable<T> SelectAll<T>() where T : class;
        IQueryable<T> SelectPage<T>(Expression<Func<T, bool>> expression, int pageSize, int pageNum, Expression<Func<T, bool>> orderExp = null, bool isAsc = true) where T : class;
        int Update<T>(T t, bool shouldCommit = true) where T : class;
        int Delete<T>(T t, bool shouldCommit = true) where T : class;
        int Delete<T>(int id, bool shouldCommit = true) where T : class;
        int DeleteRange<T>(IEnumerable<T> tList, bool shouldCommit = true) where T : class;
        int DeleteWhere<T>(Expression<Func<T, bool>> expression, bool shouldCommit = true) where T : class;
        int Commit();
    }

    public class DbInterfaceImplement : IDbInterface
    {
        public int Commit()
        {
            return 1;
        }

        public int Delete<T>(T t, bool shouldCommit = true) where T : class
        {
            return 1;
        }

        public int Delete<T>(int id, bool shouldCommit = true) where T : class
        {
            return id;
        }

        public int DeleteRange<T>(IEnumerable<T> tList, bool shouldCommit = true) where T : class
        {
            foreach(var s in tList)
                int.Parse(s.ToString());
            return 1;
        }

        public int DeleteWhere<T>(Expression<Func<T, bool>> expression, bool shouldCommit = true) where T : class
        {
            return int.Parse(expression.ToString());
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T FindId<T>(int id) where T : class
        {
            throw new NotImplementedException();
        }

        public int Insert<T>(T t, bool shouldCommit = true) where T : class
        {
            return int.Parse(t.ToString());
        }

        public int InsertRange<T>(IEnumerable<T> tList, bool shouldCommit = true) where T : class
        {
            return int.Parse(tList.ToString());
        }

        public IQueryable<T> Select<T>(Expression<Func<T, bool>> expression) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> SelectAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> SelectPage<T>(Expression<Func<T, bool>> expression, int pageSize, int pageNum, Expression<Func<T, bool>> orderExp = null, bool isAsc = true) where T : class
        {
            throw new NotImplementedException();
        }

        public int Update<T>(T t, bool shouldCommit = true) where T : class
        {
            return int.Parse(t.ToString());
        }
    }
}


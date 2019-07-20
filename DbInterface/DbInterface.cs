using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbInterface
{
    public interface IDbInterface : IDisposable
    {
        int Insert<T>(T t, bool commit = true) where T : class;
        int InsertRange<T>(IEnumerable<T> tList, bool commit = true) where T : class;
        T FindId<T>(int id) where T : class;
        IQueryable<T> Select<T>(Expression<Func<T, bool>> expression) where T : class;
        IQueryable<T> SelectAll<T>() where T : class;
        IQueryable<T> SelectPage<T, TValue>(Expression<Func<T, bool>> expression, int pageSize, int pageNum, Expression<Func<T, TValue>> orderExp = null, bool isAsc = true) where T : class;
        int Update<T>(T t, bool commit = true) where T : class;
        int Delete<T>(T t, bool commit = true) where T : class;
        int Delete<T>(int id, bool commit = true) where T : class;
        int DeleteRange<T>(IEnumerable<T> tList, bool commit = true) where T : class;
        int DeleteWhere<T>(Expression<Func<T, bool>> expression, bool commit = true) where T : class;
        int Commit();
    }
}
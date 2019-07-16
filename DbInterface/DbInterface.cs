using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbInterface
{
    interface DbInterface
    {
        int Insert<T>(T t);
        List<T> Select<T>(Expression<Func<T, bool>> expression);
        T FindId<T>(int id);
        int Update<T>(T t);
        int Delete<T>(int id);
        int DeleteWhere<T>(Expression<Func<T, bool>> expression);
    }
}
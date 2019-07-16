using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbInterface
{
    interface IDbInterface
    {
        int Insert<T>(T t);
        List<T> Select<T>(Expression<Func<T, bool>> expression);
        int Update<T>(T t);
        int DeleteWhere<T>(Expression<Func<T, bool>> expression);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.DataAccessLayer.Abstract
{
    interface IRepository<T>
    {
        List<T> List();

        List<T> List(Expression<Func<T, bool>> eresult);

        IQueryable<T> listQueryable();

        int Insert(T obj);
        int Update(T obj);
        int Delete(T obj);
        int Save();
        T Find(Expression<Func<T, bool>> eresult);

    }

    //public class Deneme<T> : IRepository<T>
    //{
    //    public int Delete(T obj)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public T Find(Expression<Func<T, bool>> eresult)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Insert(T obj)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public List<T> List()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public List<T> List(Expression<Func<T, bool>> eresult)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Save()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Update(T obj)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}

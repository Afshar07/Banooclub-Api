using BanooClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IBanooClubEFRepository<T>
    {
        T Insert(T item);
        Task InsertAsync(T item);
        Task<List<T>> GetAll();
        Task<T> Update(T item);
        void VoidUpdate(T item);
        Task Delete(T entity);
        void Erase(T entity);
        IQueryable<T> GetQuery();
        Task Save();
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IQueryable<T> SqlExecute(string executable);
        List<PostDTO> SqlObj(string executable);
        Task<object> DapperSqlQuery(string cmd);
        int GetCount();

    }
}

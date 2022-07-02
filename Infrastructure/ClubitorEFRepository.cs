using BanooClub.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure
{

    public class BanooClubEfRepository<T> : IBanooClubEFRepository<T> where T : BaseEntity
    {
        private readonly BanooClubDBContext dbContext;

        public BanooClubEfRepository(BanooClubDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<T>> GetAll()
        {
            return await this.dbContext.Set<T>().Where(z => z.IsDeleted == false).ToListAsync();
        }
        public T Insert(T item)
        {
            var ID = this.dbContext.Add<T>(item);
            this.dbContext.SaveChanges();
            return ID.Entity;
        }
        public virtual async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await this.dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }
        public async Task Save()
        {
            await this.dbContext.SaveChangesAsync();
        }
        public async Task<T> Update(T item)
        {
            dbContext.Update(item);
            await dbContext.SaveChangesAsync();
            return item;
        }
        public void VoidUpdate(T item)
        {
            dbContext.Update(item);
            dbContext.SaveChanges();
        }
        public IQueryable<T> GetQuery()
        {
            return dbContext.Set<T>().Where(z => z.IsDeleted == false).AsQueryable();
        }
        public async Task Delete(T entity)
        {
            entity.IsDeleted = true;
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
        }
        public void Erase(T entity)
        {
            this.dbContext.Remove(entity);
            this.dbContext.SaveChanges();
        }
        public IQueryable<T> FindAll()
        {
            return this.dbContext.Set<T>().Where(z => z.IsDeleted == false).AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.dbContext.Set<T>().Where(z => z.IsDeleted == false).Where(expression).AsNoTracking();
        }
        public IQueryable<T> SqlExecute(string executable)
        {
            var res = dbContext.Set<T>().FromSqlRaw($"{executable}");
            return res;
        }
        public List<PostDTO> SqlObj(string executable)
        {
            var result = this.dbContext.PostDTOs.FromSqlRaw(
             executable).ToList();
            return result;
        }
        public virtual async Task<object> DapperSqlQuery(string cmd)
        {
            try
            {
                using IDbConnection conn = new SqlConnection(dbContext.Database.GetConnectionString());
                conn.Open();

                var entities = await conn.QueryAsync<object>
                    (cmd);
                return entities;
            }
            catch
            {
                return null;
            }
        }
        public int GetCount()
        {
            return this.dbContext.Set<T>().Count();
        }

    }
}


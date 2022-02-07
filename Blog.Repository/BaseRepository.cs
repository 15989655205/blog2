using IRepository;
using Model;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class BaseRepository<TEntity> : SimpleClient<TEntity>,IBaseRepository<TEntity> where TEntity: class,new()
    {
        public BaseRepository(ISqlSugarClient context = null) :base(context)
        {
            base.Context = DbScoped.Sugar;
           // base.Context.DbMaintenance.CreateDatabase();
           // base.Context.CodeFirst.InitTables(
           // typeof(BlogNews),
           // typeof(TypeInfo),
           // typeof(WriterInfo)
           //);
        }
        public async Task<bool> CreateAsync(TEntity entity)
        {
            return await base.InsertAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await base.DeleteByIdAsync(id);
        }

        public async Task<bool> EditAsync(TEntity entity)
        {
            return await base.UpdateAsync(entity);
        }

        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }

        public virtual async Task<List<TEntity>> QueryAsync()
        {
            return await base.GetListAsync();
        }

        public virtual async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetListAsync(func);
        }

        public virtual async Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>().ToPageListAsync(page, size, total);
        }

        public virtual async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>().Where(func).ToPageListAsync(page, size, total);
        }

        Task<bool> IBaseRepository<TEntity>.CreateAsync(TEntity entity)
        {
            return   base.InsertAsync(entity);
        }

        Task<bool> IBaseRepository<TEntity>.DeleteAsync(int id)
        {
            return   base.DeleteByIdAsync(id);
        }

        Task<bool> IBaseRepository<TEntity>.EditAsync(TEntity entity)
        {
            return   base.UpdateAsync(entity);
        }

        Task<TEntity> IBaseRepository<TEntity>.FindAsync(int id)
        {
            return   base.GetByIdAsync(id);
        }
        Task<TEntity> IBaseRepository<TEntity>.FindAsync(Expression<Func<TEntity, bool>> func)
        {
            return base.GetSingleAsync(func);
        }
        Task<List<TEntity>> IBaseRepository<TEntity>.QueryAsync()
        {
            return   base.GetListAsync();
        }

        Task<List<TEntity>> IBaseRepository<TEntity>.QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            return   base.GetListAsync(func);
        }

        Task<List<TEntity>> IBaseRepository<TEntity>.QueryAsync(int page, int size, RefAsync<int> Total)
        {
            return  base.Context.Queryable<TEntity>().ToPageListAsync(page, size, Total);
        }

        Task<List<TEntity>> IBaseRepository<TEntity>.QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> Total)
        {
            return  base.Context.Queryable<TEntity>().Where(func).ToPageListAsync(page, size, Total);
        }
    }
}

using Blog.IRepository;
using Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class BlogNewsRepository:BaseRepository<BlogNews>,IBlogNewsRepository
    {
        public async override Task<List<BlogNews>> QueryAsync()
        {
            return await base.Context.Queryable<BlogNews>()
                .Mapper(x => x.TypeInfo, x => x.TypeId, x => x.TypeInfo.id)
                .Mapper(x => x.WriterInfo, x => x.WriterId, x => x.WriterInfo.id).ToListAsync();
                
        }

        public async override  Task<List<BlogNews>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<BlogNews>().ToPageListAsync(page, size, total);
        }
        public async override Task<List<BlogNews>> QueryAsync(Expression<Func<BlogNews,bool>> func) 
        {
            return await base.Context.Queryable<BlogNews>()
                .Mapper(x => x.TypeInfo, x => x.TypeInfo.id, x => x.TypeId)
                .Mapper(x => x.WriterInfo, x => x.WriterId, x => x.WriterInfo.id).ToListAsync();
        }
        public async override Task<List<BlogNews>> QueryAsync(Expression<Func<BlogNews, bool>> func,int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<BlogNews>()
                .Where(func)
                .Mapper(x => x.TypeInfo, x => x.TypeInfo.id, x => x.TypeId)
                .Mapper(x => x.WriterInfo, x => x.WriterId, x => x.WriterInfo.id)
                .ToPageListAsync(page, size, total);
        }
    }
}

using Blog.IRepository;
using Model;
using System;
using System.Collections.Generic;
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
    }
}

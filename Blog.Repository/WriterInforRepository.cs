using Blog.IRepository;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class WriterInforRepository : BaseRepository<WriterInfo>, IWriterInforRepository
    {
        //public async override Task<List<WriterInfo>> QueryAsync()
        //{
        //    base.Context.Queryable<BlogNews>()
        //        .Mapper<>
        //}
    }
}

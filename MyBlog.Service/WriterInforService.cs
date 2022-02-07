using Blog.IRepository;
using Model;
using MyBlog.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Service
{
    public class WriterInforService:BaseService<WriterInfo>,IWriterInforService
    {
        private readonly IWriterInforRepository _iWriterInfoRepository;
        public WriterInforService(IWriterInforRepository iWriterInfoRepository)
        {
            base._iBaseRepository = iWriterInfoRepository;
            _iWriterInfoRepository = iWriterInfoRepository;
        }
    }
}

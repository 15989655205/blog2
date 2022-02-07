using Blog.IRepository;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Repository
{
    public class BlogNewsRepository:BaseRepository<BlogNews>,IBlogNewsRepository
    {
    }
}

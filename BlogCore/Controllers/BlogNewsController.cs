using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Utility.ApiResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using MyBlog.IService;

namespace BlogCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService _iBlogNewsService;
        public BlogNewsController(IBlogNewsService iBlogNewsService)
        {
            this._iBlogNewsService = iBlogNewsService;
        }

        [HttpGet("BlogNews")]
        public async Task<ActionResult<ApiResult>> GetBlogNews()
        {
            var data= await _iBlogNewsService.QueryAsync();

            if (data == null)
            {
                return ApiResultHelper.Error("没有更多的值");
            }
            return ApiResultHelper.Success(data);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ApiResult>> Create(string  title,string content,int typeId)
        {
            BlogNews blog = new BlogNews
            {
                BrowseCount = 0,
                Content = content,
                LikeCount = 0,
                Time = DateTime.Now,
                Title = title,
                TypeId = typeId,
                WriterId = 1,
            };
            var data = await _iBlogNewsService.CreateAsync(blog);

            if (!data )
            {
                return ApiResultHelper.Error("添加失败");
            }
            return ApiResultHelper.Success(blog);
        }

        [HttpPost("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
             
            var data = await _iBlogNewsService.DeleteAsync(id);

            if (!data)
            {
                return ApiResultHelper.Error("删除失败");
            }
            return ApiResultHelper.Success(data);
        }

        [HttpPost("Edit")]
        public async Task<ActionResult<ApiResult>> Edit(int id,string title,string content,int typeId)
        {
            
            var data = await _iBlogNewsService.FindAsync(id);
            if(data==null) return ApiResultHelper.Error("找不到此文章");
            data.Title = title;
            data.Content = content;
            data.TypeId = typeId;

            var res= await _iBlogNewsService.EditAsync(data);
            if (!res)
            {
                return ApiResultHelper.Error("修改失败");
            }
            return ApiResultHelper.Success(res);
        }
    }
}

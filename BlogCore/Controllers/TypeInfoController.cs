using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.IRepository;
using BlogCore.Utility.ApiResult;
using Microsoft.AspNetCore.Mvc;
using Model;
using MyBlog.IService;

namespace BlogCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeInfoController : Controller
    {
        private readonly ITypeInfoService iTypeInfoService;
        public TypeInfoController(ITypeInfoService _iTypeInfoService)
        {
            iTypeInfoService = _iTypeInfoService;
        }

        [HttpGet("Types")]
        public async Task<ActionResult<ApiResult>> GetType()
        {
            var data = await iTypeInfoService.QueryAsync();

            if (data == null)
            {
                return ApiResultHelper.Error("没有更多的值");
            }
            return ApiResultHelper.Success(data);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ApiResult>> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return ApiResultHelper.Error("类型名称不能为空");

            TypeInfo blog = new TypeInfo
            {
                Name= name
            };
            var data = await iTypeInfoService.CreateAsync(blog);

            if (!data)
            {
                return ApiResultHelper.Error("添加失败");
            }
            return ApiResultHelper.Success(blog);
        }

        [HttpPost("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {

            var data = await iTypeInfoService.DeleteAsync(id);

            if (!data)
            {
                return ApiResultHelper.Error("删除失败");
            }
            return ApiResultHelper.Success(data);
        }
        [HttpPost("Edit")]
        public async Task<ActionResult<ApiResult>> Edit(int id, string name )
        {

            var data = await iTypeInfoService.FindAsync(id);
            if (data == null) return ApiResultHelper.Error("找不到此类别");
            data.Name = name;
             
            var res = await iTypeInfoService.EditAsync(data);
            if (!res)
            {
                return ApiResultHelper.Error("修改失败");
            }
            return ApiResultHelper.Success(res);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Utility._MD5;
using BlogCore.Utility.ApiResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using MyBlog.IService;

namespace BlogCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriterController : ControllerBase
    {
        private readonly IWriterInforService iWriterInforService;
        public WriterController(IWriterInforService _iWriterInforService)
        {
            iWriterInforService = _iWriterInforService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name,string username,string pwd)
        {
            if (string.IsNullOrWhiteSpace(name)) return ApiResultHelper.Error("名称不能为空");

            WriterInfo blog = new WriterInfo
            {
                Name=name,
                UserName=username,
                UserPwd= MD5Helper.Md5(pwd)
            };
         
            var oldModel= await iWriterInforService.FindAsync(x => x.Name == name);
            if (oldModel != null)
            {
                return ApiResultHelper.Error("账号已存在");
            }

            var data = await iWriterInforService.CreateAsync(blog);

            if (!data)
            {
                return ApiResultHelper.Error("添加失败");
            }
            return ApiResultHelper.Success(blog);
        }

        [HttpPost("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {

            var data = await iWriterInforService.DeleteAsync(id);

            if (!data)
            {
                return ApiResultHelper.Error("删除失败");
            }
            return ApiResultHelper.Success(data);
        }
        [HttpPost("Edit")]
        public async Task<ActionResult<ApiResult>> Edit(int id, string name)
        {

            var data = await iWriterInforService.FindAsync(id);
            if (data == null) return ApiResultHelper.Error("找不到此用户");
            data.Name = name;

            var res = await iWriterInforService.EditAsync(data);
            if (!res)
            {
                return ApiResultHelper.Error("修改失败");
            }
            return ApiResultHelper.Success(res);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogCore.Utility._MD5;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using JWT.Utility.ApiResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model;
using MyBlog.IService;

namespace MyBlog.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        //将"JWT"三个字母通过SHA256加密后得到
        private const string secret = "fc93cb07e1ad92898527100e58a1cf1d1e7f65e9a266a6f87f3c84feb541c7b3";
        private readonly IWriterInforService iWriterInforService;

        public AuthorizeController(IWriterInforService _writerInforService)
        {
            iWriterInforService = _writerInforService;
        }

        [HttpPost("Login")]
        public async Task<ApiResult> Login(string username, string userpwd)
        {
            var pwd = MD5Helper.Md5(userpwd);
            var model = await iWriterInforService.FindAsync(x => x.Name == username && x.UserPwd == pwd);

            if (model != null)
            {
                //定义payload中的数据  里面的数据可随意填，一般都是返回用户数据
                var payload = new Dictionary<string, object>
            {
                { "Name",model.Name },
                 { "UserName",model.UserName },
                { "Id", model.id }
            };
                var claims = new[]
                   {
                    new Claim("UserName",model.UserName),
                    new Claim ("Id",model.id.ToString() ),
                    new Claim(ClaimTypes.Name, model.Name )
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: "http://localhost:6060",
                    audience: "http://localhost:5000",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);
                var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
                return ApiResultHelper.Success(tokenStr);


            }
            else
            {
                return ApiResultHelper.Error("");
            }

            //WriterInfo
        }
    }
}

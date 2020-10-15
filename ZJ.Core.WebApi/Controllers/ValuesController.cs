using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ZJ.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;

        public IHttpContextAccessor HttpContextAccessor => _accessor;

        public ValuesController(HttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //简单 创建一个token令牌

            var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name,"LaoZhou"),
                    new Claim(JwtRegisteredClaimNames.Email,"jeremyZhou"),
                    new Claim(JwtRegisteredClaimNames.NameId,"1")
                };

            //生成密钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("LaoZhouLaoZhouLaoZhouLaoZhou")); //至少16位密钥

            //实例化token 对象
            var token = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5001",
                claims: claims,
                notBefore: DateTime.Now.AddHours(1),    //过期时间
                signingCredentials: new SigningCredentials(key: key, SecurityAlgorithms.HmacSha256) //加密方式及密钥
                );




            //生成 token
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new string[] { jwtToken };
        }

        [HttpGet]
        [Route("jwtstr")]
        public ActionResult<IEnumerable<string>> Get(string jwtstr)
        {
            //获取 token 内容的方式
            //1
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = jwtHandler.ReadJwtToken(jwtstr);

            //2
            var sub = User.FindFirst(d => d.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            //3

        }
    }
}
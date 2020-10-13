using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZJ.Core.WebApi.Controllers
{
    /// <summary>
    /// 严格遵循RestFull 风格     Get、Post、Put、Delete
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]     //First当成一个资源，对外提供增删改查的Api
    public class FirstController : ControllerBase
    {
        [Route("Get")]
        [HttpGet]
        public string Get()
        {
            return "Core测试";
        }

        /// <summary>
        /// Swagger测试
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        [Route("Info")]     //特性路由违反了RestFull风格
        [HttpGet]
        public string Info(int id, string name)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                Id = id,
                Name = name
            });
        }
    }
}
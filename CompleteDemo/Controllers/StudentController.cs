using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using CompleteDemo.DataBase;
using CompleteDemo.Model;
using CompleteDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CompleteDemo.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudentServices studentServices;
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// 无参构造函数
        /// </summary>
        /// <param name="_studentServices"></param>
        public StudentController(IStudentServices _studentServices, IMemoryCache _memoryCache)
        {
            studentServices = _studentServices;
            memoryCache = _memoryCache;
        }

        [HttpGet]
        public async Task<bool> SetCache()
        {
            memoryCache.Set("name", "wangxiaowei");

            return await Task.FromResult(true);
        }

        [HttpGet]
        public async Task<string> GetCache()
        {
            var name = memoryCache.Get("name").ToString();

            return await Task.FromResult(name);
        }

        /// <summary>
        /// 获取学生列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Student>> GetStudentList()
        {
            var cookie = GetCookies("test");
            return await studentServices.GetStudentList();
        }

        /// <summary>
        /// 获取学生信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Student> GetStudentInfoByID(int ID)
        {
            SetCookies("test", Guid.NewGuid().ToString());
            return await studentServices.GetStudentInfoByID(ID);
        }

        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> UpdateStudentInfoByID(int ID, string Name)
        {
            return await studentServices.UpdateStudentInfoByID(ID, Name);
        }

        /// <summary>
        /// 新增学生信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> InsertStudentInfoByID(int ID, string Name)
        {
            return await studentServices.InsertStudentInfoByID(ID, Name);
        }

        /// <summary>
        ///  Transaction事务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> Transaction()
        {
            return await studentServices.Transaction();
        }

        /// <summary>
        ///  Transaction事务提交
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> TransactionCommit()
        {
            return await studentServices.TransactionCommit();
        }

        /// <summary>
        ///  TransactionScope事务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> TransactionScope()
        {
            return await studentServices.TransactionScope();
        }

        [HttpGet]
        public async Task<bool> AutoMapperTest()
        {
            return await studentServices.AutoMapperTest();
        }

        /// <summary>
        /// 设置本地cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>  
        /// <param name="minutes">过期时长，单位：分钟</param>      
        protected void SetCookies(string key, string value, int minutes = 30)
        {
            HttpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(minutes)
            });
        }

        /// <summary>
        /// 删除指定的cookie
        /// </summary>
        /// <param name="key">键</param>
        protected void DeleteCookies(string key)
        {
            HttpContext.Response.Cookies.Delete(key);
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        protected string GetCookies(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }
    }
}
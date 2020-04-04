using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompleteDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompleteDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        TeacherServices teacherServices;

        /// <summary>
        /// 无参构造函数
        /// </summary>
        /// <param name="_teacherServices"></param>
        public TeacherController(TeacherServices _teacherServices)
        {
            teacherServices = _teacherServices;
        }

        /// <summary>
        /// 获取教师列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetTeacherList()
        {
            return await teacherServices.GetTeacherList();
        }
    }
}
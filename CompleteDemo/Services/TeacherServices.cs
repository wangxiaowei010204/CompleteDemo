using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteDemo.Services
{
    public class TeacherServices : ITeacherServices
    {
        /// <summary>
        /// 获取教师列表
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetTeacherList()
        {
            return await Task.FromResult("GetTeacherListTest");
        }
    }
}

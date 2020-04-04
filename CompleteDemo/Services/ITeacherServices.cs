using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteDemo.Services
{
    public interface ITeacherServices
    {
        Task<string> GetTeacherList();
    }
}

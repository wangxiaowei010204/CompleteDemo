using CompleteDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteDemo.Services
{
    public interface IStudentServices
    {
        Task<List<Student>> GetStudentList();

        Task<Student> GetStudentInfoByID(int ID);

        Task<bool> UpdateStudentInfoByID(int ID, string Name);

        Task<bool> InsertStudentInfoByID(int ID, string Name);

        Task<bool> Transaction();

        Task<bool> TransactionCommit();

        Task<bool> TransactionScope();

        Task<bool> AutoMapperTest();
    }
}

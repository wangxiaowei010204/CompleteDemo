using Autofac.Extras.DynamicProxy;
using AutoMapper;
using CompleteDemo.DataBase;
using CompleteDemo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace CompleteDemo.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly DemoContext _context;
        private readonly MySQLDataBaseConfig _mySQLDataBaseConfig;
        private static Dictionary<string, IDbContextTransaction> dic = new Dictionary<string, IDbContextTransaction>();
        private readonly IMapper _mapper;

        public StudentServices(MySQLDataBaseConfig _mySQLDataBaseConfig, IMapper mapper)
        {
            _context = _mySQLDataBaseConfig.CreateContext();
            this._mapper = mapper;
        }

        /// <summary>
        /// 获取学生列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Student>> GetStudentList()
        {
            List<Student> allUsers = new List<Student>();
            allUsers = _context.student.Skip(5).Take(2).OrderBy(x => x.ID).ToList();

            return await Task.FromResult(allUsers);
        }

        /// <summary>
        /// 获取学生信息
        /// </summary>
        /// <returns></returns>
        public async Task<Student> GetStudentInfoByID(int ID)
        {
            var student = _context.student.FirstOrDefault(x => x.ID == ID);

            return await Task.FromResult(student);
        }

        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateStudentInfoByID(int ID, string Name)
        {
            var student = _context.student.FirstOrDefault(x => x.ID == ID);

            student.Name = Name;
            student.CreateTime = DateTime.Now;

            // 异步保存
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        /// <summary>
        /// 新增学生信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InsertStudentInfoByID(int ID, string Name)
        {
            var student = new Student();

            student.ID = ID;
            student.Name = Name;
            student.CreateTime = DateTime.Now;

            _context.student.Add(student);

            // 异步保存
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Transaction事物
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Transaction()
        {
            var tran = _context.Database.BeginTransaction();  //开启事务

            dic.Add("test", tran);
            try
            {
                var item = _context.student.FirstOrDefault(x => x.ID == 1);
                item.CreateTime = DateTime.Now;
                await _context.SaveChangesAsync();

                var item2 = _context.student.FirstOrDefault(x => x.ID == 2);
                item2.CreateTime = DateTime.Now;
                await _context.SaveChangesAsync();

                tran.Commit();  //必须调用Commit()，不然数据不会保存

                return await Task.FromResult(true).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                tran.Rollback(); //出错就回滚

                return await Task.FromResult(false);
            }
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        /// <returns></returns>
        public async Task<bool> TransactionCommit()
        {
            var tran = dic["test"];
            tran.Commit();

            return await Task.FromResult(true);
        }

        /// <summary>
        /// TransactionScope事务
        /// </summary>
        /// <returns></returns>
        public async Task<bool> TransactionScope()
        {
            using (var tran = new TransactionScope())   //开启事务
            {
                try
                {
                    var student = _context.student.FirstOrDefault(s => s.ID == 3);
                    student.CreateTime = DateTime.Now;
                    _context.SaveChanges();

                    var student2 = _context.student.FirstOrDefault(s => s.ID == 4);
                    student2.CreateTime = DateTime.Now;
                    _context.SaveChanges();

                    tran.Complete();  //必须调用.Complete()，不然数据不会保存

                    return await Task.FromResult(true);
                }
                catch (Exception ex)
                {
                    return await Task.FromResult(false);
                }
            }
        }

        /// <summary>
        /// automapper测试
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AutoMapperTest()
        {
            StudentDto studentDto = new StudentDto()
            {
                ID = 1,
                Name = "fan",
                CreateTime = DateTime.Now
            };

            Student student = _mapper.Map<Student>(studentDto);

            return await Task.FromResult(true);
        }
    }
}

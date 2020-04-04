using AutoMapper;
using CompleteDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteDemo.Common
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //CreateMap<StudentDto, Student>().ForMember(d => d.Name, u => u.MapFrom(s => s.Name))    //属性名称映射
            //                        .ForMember(d => d.CreateTime, u => u.MapFrom(s => s.CreateTime))  //属性名称映射
            //                        //.ForMember(d => d.age, u => u.Condition(s => s.age >= 0 && s.age <= 120)) //对一些属性做映射判断
            //                       // .BeforeMap((dto, ent) => ent.fullname = dto.firstname + "_" + dto.lastname)   //对一些属性做映射前处理

            CreateMap<StudentDto, Student>();

        }
    }
}

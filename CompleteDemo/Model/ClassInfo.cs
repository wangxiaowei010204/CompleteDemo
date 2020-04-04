using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteDemo.Model
{
    [Table("classinfo")]
    public class ClassInfo
    {
        public int Id { get; set; }

        [StringLength(15)]
        public string ClassName { get; set; }

        [StringLength(15)]
        public string ClassExplain{ get; set; }
    }
}

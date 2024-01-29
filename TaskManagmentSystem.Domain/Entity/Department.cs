using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentSystem.Domain.Entity
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }
        public string Name { get; set; }


        //связь один к одному
        public int DirectorId { get; set; }
        public UserEntity Director { get; set; }
        

        //cвязь один ко многим
        public ICollection<UserEntity> Users { get; set;}
    }
}

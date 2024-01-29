using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.Domain.Enums;

namespace TaskManagmentSystem.Domain.Entity
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserEntityId { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }

        //две связи один ко многим
        public List<TaskEntity>? taskEntitiesInitiator { get; set; }
        public List<TaskEntity>? taskEntitiesRecipient { get; set; }

        // связь один к одному
        [ForeignKey(nameof(UserEntityProfile))]
        public int UserEntityProfileId { get; set; }
        public UserEntityProfile UserEntityProfile { get; set; }

        //связь многие к одному
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
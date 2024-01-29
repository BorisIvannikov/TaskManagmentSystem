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
    public class TaskEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskEntityId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime BornDateTime { get; set; }
        public bool IsDone { get; set; }

        //две связи многие к одному
        public int? InitiatorId { get; set; }
        public UserEntity? Initiator { get; set; }

        public int? RecipientId { get; set; }
        public UserEntity? Recipient { get; set; }

    }
}


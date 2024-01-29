using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.Domain.Enums;

namespace TaskManagmentSystem.Domain.Entity
{
    public class UserEntityProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserEntityProfileId { get; set; }

        [NotMapped]
        private int _userEntityProfileId;

        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Position Position {  get; set; }

        //связь один к одному
        public int UserEntityId { get; set; }
        public UserEntity UserEntity { get; set; }
    }
}

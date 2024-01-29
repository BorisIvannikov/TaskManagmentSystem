using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.Enums;

namespace TaskManagmentSystem.Domain.ViewModels
{
    public class UserEntityProfileViewModel
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Position Position { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.Domain.Enums;

namespace TaskManagmentSystem.Domain.ViewModels
{
    public class UserEntityViewModel
    {
        /// <summary>
        /// айди
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Занимаемая должность
        /// </summary>
        public Position Position { get; set; }
    }
}

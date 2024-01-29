using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentSystem.Domain.Enums
{
    /// <summary>
    /// описывает должности пользователей
    /// </summary>
    public enum Position
    {
        None = 0,
        /// <summary>
        /// Администратор приложения
        /// </summary>
        [Display(Name = "Администратор приложения")]
        Admin = 1,
        /// <summary>
        /// Глава компании
        /// </summary>
        [Display(Name = "Глава компании")]
        Boss = 2,
        /// <summary>
        /// Руководитель отдела
        /// </summary>
        [Display(Name = "Руководитель отдела")]
        Director = 3,
        /// <summary>
        /// Работник
        /// </summary>
        [Display(Name = "Работник")]
        Worker = 4,
    }
}

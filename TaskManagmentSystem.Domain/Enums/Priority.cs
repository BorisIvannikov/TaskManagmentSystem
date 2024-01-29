using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentSystem.Domain.Enums
{
    /// <summary>
    /// описывает приоритет задачи
    /// </summary>
    public enum Priority
    {
        None = 0,
        [Display(Name = "Высокий")]
        High = 1,
        [Display(Name = "Средний")]
        Medium = 2,
        [Display(Name = "Низкий")]
        Low = 3,
    }
}

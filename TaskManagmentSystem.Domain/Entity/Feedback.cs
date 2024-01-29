using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaskManagmentSystem.Domain.Entity
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Имя отправителя
        /// </summary>
        [Display(Name = "Введите Имя")]
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string Name { get; set; }

        /// <summary>
        /// Сообщение от человека
        /// </summary>
        [Display(Name = "Введите Сообщение")]
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string Message { get; set; }

        /// <summary>
        /// Время рождения
        /// </summary>
        public DateTime BornTime { get; set; }
    }
}

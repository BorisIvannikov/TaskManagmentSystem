using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.Enums;

namespace TaskManagmentSystem.Domain.ViewModels
{
    public class TaskEntityViewModel
    {
		///// <summary>
		///// айди
		///// </summary>
		//public int UserEntityId { get; set; }

		/// <summary>
		/// Тема задачи
		/// </summary>
		public string Subject { get; set; }

		/// <summary>
		/// Описание задачи
		/// </summary>
		public string Description { get; set; }

        /// <summary>
        /// Приоритет задачи
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// пользователь создавший задачу
        /// </summary>
        public UserEntity? Initiator { get; set; }

        
        /// <summary>
        /// id пользователя создавший задачу
        /// </summary>
        public int InitiatorID { get; set; }   
        

        /// <summary>
        /// Пользователь которому задача назначена
        /// </summary>
        public UserEntity? Recipient { get; set; }

        
        /// <summary>
        /// id пользователя которому задача назначена
        /// </summary>
        public int RecipientID { get; set; }
        

        /// <summary>
        /// Время появления задачи в системе
        /// </summary>
        public DateTime BornDateTime { get; set; }

        
    }
}

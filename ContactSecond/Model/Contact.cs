using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ContactSecond.Model
{
    /// <summary>
    /// Представляет модель контакта с именем, номером телефона и электронной почтой.
    /// Содержит проверки для корректности вводимых данных.
    /// </summary>
    public class Contact
    {
        private string _name;
        private string _number;
        private string _email;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Contact"/> с указанными данными.
        /// </summary>
        /// <param name="name">Имя контакта. Не может быть пустым или состоять только из пробелов.</param>
        /// <param name="number">Номер телефона контакта. Не может быть пустым или состоять только из пробелов.</param>
        /// <param name="email">Email контакта. Не может быть пустым или состоять только из пробелов.</param>
        public Contact(string name, string number, string email)
        {
            Name = name;
            Number = number;
            Email = email;
        }
        public string Name {  get; set; }
        public string Number { get; set; }  
        public string Email { get; set; }
    }
}

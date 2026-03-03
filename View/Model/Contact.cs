using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace View.Model
{
    /// <summary>
    /// Представляет модель контакта с именем, номером телефона и электронной почтой.
    /// Содержит проверки для корректности вводимых данных.
    /// </summary>
    class Contact
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

        /// <summary>
        /// Возвращает или задает имя контакта.
        /// </summary>
        /// <value>Имя контакта. Не может быть <c>null</c>, пустой строкой или строкой из пробелов.</value>
        /// <exception cref="MessageBox">Выбрасывается, если присваиваемое значение — <c>null</c>, пустая строка или строка из пробелов.</exception>
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("Имя введено неправильно. Имя не может быть пустым или состоять только из пробелов.");
                }
                _name = value;
            }
        }

        /// <summary>
        /// Возвращает или задает номер телефона контакта.
        /// </summary>
        /// <value>Номер телефона. Не может быть <c>null</c>, пустой строкой или строкой из пробелов.</value>
        /// <exception cref="MessageBox">Выбрасывается, если присваиваемое значение — <c>null</c>, пустая строка или строка из пробелов.</exception>
        public string Number
        {
            get { return _number; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("Номер введен неверно. Номер не может быть пустым или состоять только из пробелов.");
                }
                _number = value;
            }
        }

        /// <summary>
        /// Возвращает или задает email контакта.
        /// </summary>
        /// <value>Email контакта. Не может быть <c>null</c>, пустой строкой или строкой из пробелов.</value>
        /// <exception cref="MessageBox">Выбрасывается, если присваиваемое значение — <c>null</c>, пустая строка или строка из пробелов.</exception>
        /// <remarks>
        /// В текущей реализации проверяется только заполненность поля.
        /// Для полноценной проверки формата email рекомендуется использовать регулярные выражения.
        /// </remarks>
        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("Введен некорректный email. Email не может быть пустым или состоять только из пробелов.");
                }
                _email = value;
            }
        }
    }
}
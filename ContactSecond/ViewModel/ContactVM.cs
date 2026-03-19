using ContactSecond.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ContactSecond.ViewModel
{
    public class ContactVM : INotifyPropertyChanged
    {
        private Contact _contact;
        private string _name;
        private string _phoneNumber;
        private string _email;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ContactVM"/> на основе модели контакта.
        /// </summary>
        /// <param name="contact">Модель контакта, которая не может быть null.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="contact"/> равен null.</exception>
        public ContactVM(Contact contact)
        {
            _contact = contact ?? throw new ArgumentNullException(nameof(contact));
            _name = _contact.Name;
            _phoneNumber = _contact.Number;
            _email = _contact.Email;
        }

        /// <summary>
        /// Получает или задаёт имя контакта.
        /// При изменении значения обновляет модель <see cref="Contact"/> и вызывает событие <see cref="PropertyChanged"/>.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    _contact.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Получает или задаёт номер телефона контакта.
        /// При изменении значения обновляет модель <see cref="Contact"/> и вызывает событие <see cref="PropertyChanged"/>.
        /// </summary>
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    _contact.Number = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Получает или задаёт email контакта.
        /// При изменении значения обновляет модель <see cref="Contact"/> и вызывает событие <see cref="PropertyChanged"/>.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    _contact.Email = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Получает или задаёт модель контакта.
        /// При установке нового значения обновляет все свойства ViewModel и вызывает соответствующие события.
        /// </summary>
        /// <exception cref="ArgumentNullException">Выбрасывается при попытке установить значение null.</exception>
        public Contact Contact
        {
            get => _contact;
            set
            {
                if (_contact == value) return;

                _contact = value ?? throw new ArgumentNullException(nameof(value));

                // Обновляем локальные поля из новой модели
                _name = _contact.Name;
                _phoneNumber = _contact.Number;
                _email = _contact.Email;

                // Уведомляем об изменении всех свойств
                OnPropertyChanged(nameof(Contact));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(PhoneNumber));
                OnPropertyChanged(nameof(Email));
            }
        }

        /// <summary>
        /// Событие, возникающее при изменении значения свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызывает событие <see cref="PropertyChanged"/> для указанного свойства.
        /// </summary>
        /// <param name="propertyName">
        /// Имя свойства, для которого вызывается событие. 
        /// Если не указано, используется имя вызывающего члена.
        /// </param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

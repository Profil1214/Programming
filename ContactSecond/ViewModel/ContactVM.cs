using ContactSecond.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactSecond.ViewModel
{
    public class ContactVM : INotifyPropertyChanged, IDataErrorInfo
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
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Name):
                        if (Name.Length > 100)
                        {
                            return "Имя должно быть не длиннее 100 символов";
                        }
                        if (String.IsNullOrWhiteSpace(Name))
                        {
                            return "Имя не может быть пустым";
                        }
                        break;

                    case nameof(PhoneNumber):
                        if (PhoneNumber.Length > 100)
                        {
                            return "Номер должен быть не длиннее 100 символов";
                        }
                        if (!Regex.IsMatch(PhoneNumber, @"^[\d\+\-\(\)\s]+$"))
                            return "Телефон может содержать только цифры, +, -, ()";
                        if (String.IsNullOrWhiteSpace(PhoneNumber))
                            return "Телефон не может быть пустым";
                        break;
                    case nameof(Email):
                        if (Email.Length > 100)
                            return "Почтовый адрес не может быть длиннее 100 символов";
                        if (!Email.Contains("@"))
                            return "Почтовый адрес должен содержать символ @";
                        if (String.IsNullOrWhiteSpace(Email))
                            return "Почтовый адрес не может быть пустым";
                        break;

                }
                return null;
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

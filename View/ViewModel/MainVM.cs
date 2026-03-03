using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using View.Model;
using View.Model.Services;

namespace View.ViewModel
{
    class MainVM : INotifyPropertyChanged
    {
        private Contact _contact;
        private ContactSerializer _serializer;
        private string _name;
        private string _phoneNumber;
        private string _email;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MainVM"/>.
        /// Устанавливает начальные значения для контакта, создает сериализатор и команды сохранения/загрузки.
        /// </summary>
        public MainVM()
        {
            _name = "Иван";
            _phoneNumber = "12345678901";
            _email = "ivat@mail.com";
            _contact = new Contact(_name, _phoneNumber, _email);
            _serializer = new ContactSerializer();
            SaveCommand = new SaveCommand(this);
            LoadCommand = new LoadCommand(this);
        }

        /// <summary>
        /// Возвращает или задает имя контакта.
        /// При изменении значения автоматически обновляет связанный объект <see cref="Contact"/>.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    UpdateContact();
                }
            }
        }

        /// <summary>
        /// Возвращает команду сохранения контакта в файл.
        /// </summary>
        public ICommand SaveCommand { get; private set; }

        /// <summary>
        /// Возвращает команду загрузки контакта из файла.
        /// </summary>
        public ICommand LoadCommand { get; private set; }

        /// <summary>
        /// Возвращает или задает номер телефона контакта.
        /// При изменении значения автоматически обновляет связанный объект <see cref="Contact"/>.
        /// </summary>
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    UpdateContact();
                }
            }
        }

        /// <summary>
        /// Возвращает или задает email контакта.
        /// При изменении значения автоматически обновляет связанный объект <see cref="Contact"/>.
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    UpdateContact();
                }
            }
        }

        /// <summary>
        /// Возвращает или задает объект контакта <see cref="Contact"/>.
        /// Доступен только для установки внутри класса.
        /// При изменении вызывает событие <see cref="PropertyChanged"/>.
        /// </summary>
        public Contact Contact
        {
            get { return _contact; }
            private set
            {
                _contact = value;
                OnPropertyChanged(nameof(Contact));
            }
        }

        /// <summary>
        /// Обновляет свойства объекта <see cref="Contact"/> текущими значениями из полей ввода.
        /// Вызывается после изменения любого из свойств Name, PhoneNumber или Email.
        /// </summary>
        private void UpdateContact()
        {
            _contact.Name = _name;
            _contact.Number = _phoneNumber;
            _contact.Email = _email;
            OnPropertyChanged(nameof(Contact));
        }

        /// <summary>
        /// Событие, возникающее при изменении значения свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызывает событие <see cref="PropertyChanged"/> для указанного свойства.
        /// </summary>
        /// <param name="propertyName">
        /// Имя свойства, которое изменилось.
        /// Если параметр не указан, используется имя вызывающего метода.
        /// </param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Сохраняет текущий контакт в файл с помощью <see cref="ContactSerializer"/>.
        /// В случае ошибки выводит сообщение в отладочную консоль.
        /// </summary>
        public void SaveContact()
        {
            try
            {
                _serializer.SaveContact(_contact);
                System.Diagnostics.Debug.WriteLine("Контакт сохранен");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сохранения: {ex.Message}");
            }
        }

        /// <summary>
        /// Загружает контакт из файла с помощью <see cref="ContactSerializer"/>.
        /// При успешной загрузке обновляет все свойства и объект контакта.
        /// В случае ошибки выводит сообщение в отладочную консоль.
        /// </summary>
        public void LoadContact()
        {
            try
            {
                Contact loadedContact = _serializer.LoadContact();
                if (loadedContact != null)
                {
                    Name = loadedContact.Name;
                    PhoneNumber = loadedContact.Number;
                    Email = loadedContact.Email;

                    UpdateContact();
                    System.Diagnostics.Debug.WriteLine("Контакт загружен");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки: {ex.Message}");
            }
        }
    }
}


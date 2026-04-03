using ContactSecond.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ContactSecond.Model.Services;

namespace ContactSecond.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        private ContactSerializer _contactSerializer;
        private ContactVM _originalContactBeforeEdit;
        private ContactVM _tempContact;
        private ObservableCollection<ContactVM> _contacts;
        private ContactVM _selectedContacts;
        private ContactVM _editContactCopy;
        private bool _isAddingNew = false;
        private bool _isEdit = false;

        /// <summary>
        /// Инициализирует новый экземпляр класса MainVM.
        /// Создает экземпляр ContactSerializer, инициализирует коллекцию контактов,
        /// создает команды и загружает сохраненные контакты.
        /// </summary>
        public MainVM()
        {
            _contactSerializer = new ContactSerializer();
            _contacts = new ObservableCollection<ContactVM>();
            AddCommand = new RelayCommand(ExecuteAdd, CanAdd);
            ApplyCommand = new RelayCommand(ExecuteApply, CanApply);
            EditCommand = new RelayCommand(ExecuteEdit, CanEdit);
            RemoveCommand = new RelayCommand(ExecuteRemove, CanRemove);
            LoadCommand = new RelayCommand(ExecuteLoad, CanLoad);
            SaveCommand = new RelayCommand(ExecuteSave, CanSave);
            ExecuteLoad(null);
        }

        /// <summary>
        /// Получает коллекцию контактов, представленных в виде ContactVM.
        /// </summary>
        public ObservableCollection<ContactVM> Contacts
        {
            get { return _contacts; }
        }

        /// <summary>
        /// Получает или задает выбранный контакт в коллекции.
        /// При изменении значения отменяет текущее редактирование при необходимости
        /// и вызывает обновление команд.
        /// </summary>
        public ContactVM SelectedContact
        {
            get { return _selectedContacts; }
            set
            {
                if (_selectedContacts != value)
                {
                    CancelEditingIfNeeded();
                    _selectedContacts = value;
                    OnPropertyChanged(nameof(SelectedContact));
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        /// <summary>
        /// Отменяет текущий режим редактирования или добавления контакта,
        /// сбрасывая соответствующие флаги и временные объекты.
        /// </summary>
        private void CancelEditingIfNeeded()
        {
            if (IsEditNew == true || IsAddingNew == true)
            {

                IsAddingNew = false;
                IsEditNew = false;
                _tempContact = null;
                _editContactCopy = null;
                _originalContactBeforeEdit = null;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        /// <summary>
        /// Получает значение, указывающее, выполняется ли добавление нового контакта.
        /// </summary>
        public bool IsAddingNew
        {
            get { return _isAddingNew; }
            private set
            {
                _isAddingNew = value;
                OnPropertyChanged(nameof(IsAddingNew));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        /// <summary>
        /// Получает значение, указывающее, выполняется ли редактирование существующего контакта.
        /// </summary>
        public bool IsEditNew
        {
            get { return _isEdit; }
            private set
            {
                _isEdit = value;
                OnPropertyChanged(nameof(IsEditNew));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        /// <summary>
        /// Получает команду загрузки контактов.
        /// </summary>
        public ICommand LoadCommand { get; }

        /// <summary>
        /// Получает команду сохранения контактов.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Получает команду добавления нового контакта.
        /// </summary>
        public ICommand AddCommand { get; }

        /// <summary>
        /// Получает команду применения изменений при добавлении или редактировании контакта.
        /// </summary>
        public ICommand ApplyCommand { get; }

        /// <summary>
        /// Получает команду редактирования выбранного контакта.
        /// </summary>
        public ICommand EditCommand { get; }

        /// <summary>
        /// Получает команду удаления выбранного контакта.
        /// </summary>
        public ICommand RemoveCommand { get; }

        /// <summary>
        /// Определяет, может ли быть выполнена команда сохранения.
        /// </summary>
        /// <param name="param">Параметр команды.</param>
        /// <returns>Всегда возвращает true.</returns>
        public bool CanSave(object param) => true;

        /// <summary>
        /// Выполняет сохранение всех контактов в файл.
        /// </summary>
        /// <param name="param">Параметр команды.</param>
        private void ExecuteSave(object param)
        {
            List<Contact> contactsToSave = new List<Contact>();

            foreach (ContactVM contactVM in _contacts)
            {
                Contact newContact = new Contact(
                    contactVM.Name,
                    contactVM.PhoneNumber,
                    contactVM.Email
                );
                contactsToSave.Add(newContact);
            }
            _contactSerializer.SaveContacts(contactsToSave);
        }

        /// <summary>
        /// Определяет, может ли быть выполнена команда загрузки.
        /// </summary>
        /// <param name="param">Параметр команды.</param>
        /// <returns>Всегда возвращает true.</returns>
        private bool CanLoad(object param) => true;

        /// <summary>
        /// Выполняет загрузку контактов из файла.
        /// Очищает текущую коллекцию и добавляет загруженные контакты.
        /// </summary>
        /// <param name="param">Параметр команды.</param>
        private void ExecuteLoad(object param)
        {
            var loadedContacts = _contactSerializer.LoadContacts();

            _contacts.Clear();
            foreach (var contact in loadedContacts)
            {
                _contacts.Add(new ContactVM(contact));
            }
        }

        /// <summary>
        /// Определяет, может ли быть выполнена команда добавления контакта.
        /// </summary>
        /// <param name="param">Параметр команды.</param>
        /// <returns>Возвращает true, если не выполняется добавление; иначе false.</returns>
        private bool CanAdd(object param) => !IsAddingNew;

        /// <summary>
        /// Выполняет добавление нового контакта.
        /// Создает временный контакт и переключает режим добавления.
        /// </summary>
        /// <param name="param">Параметр команды.</param>
        private void ExecuteAdd(object param)
        {
            SelectedContact = null;
            _tempContact = new ContactVM(new Contact("", "", ""));
            SelectedContact = _tempContact;
            IsAddingNew = true;
            IsEditNew = false;

        }

        /// <summary>
        /// Определяет, может ли быть выполнена команда применения изменений.
        /// </summary>
        /// <param name="param">Параметр команды.</param>
        /// <returns>Возвращает true, если выполняется добавление; иначе false.</returns>
        private bool CanApply(object param)
        {
            if (!IsAddingNew) return false;
            if (SelectedContact == null) return false;

            // Проверяем наличие ошибок валидации
            bool hasNameError = !string.IsNullOrEmpty(SelectedContact[nameof(ContactVM.Name)]);
            bool hasPhoneError = !string.IsNullOrEmpty(SelectedContact[nameof(ContactVM.PhoneNumber)]);
            bool hasEmailError = !string.IsNullOrEmpty(SelectedContact[nameof(ContactVM.Email)]);

            return !hasNameError && !hasPhoneError && !hasEmailError;
        }

        /// <summary>
        /// Выполняет применение изменений при добавлении нового контакта
        /// или сохранении изменений при редактировании существующего.
        /// </summary>
        /// <param name="param">Параметр команды.</param>
        private void ExecuteApply(object param)
        {
            if (IsAddingNew == true && IsEditNew == false)
            {
                var newContact = new ContactVM(new Contact(SelectedContact.Name ?? "без имени", SelectedContact.PhoneNumber ?? "", SelectedContact.Email ?? ""));
                Contacts.Add(newContact);
                IsAddingNew = false;

                SelectedContact = newContact;
                _tempContact = null;

            }

            if (IsAddingNew == true && IsEditNew == true)
            {
                _originalContactBeforeEdit.Name = _editContactCopy.Name;
                _originalContactBeforeEdit.PhoneNumber = _editContactCopy.PhoneNumber;
                _originalContactBeforeEdit.Email = _editContactCopy.Email;
                SelectedContact = _originalContactBeforeEdit;
                IsEditNew = false;
                IsAddingNew = false;
                _editContactCopy = null;
                _originalContactBeforeEdit = null;

            }

        }

        /// <summary>
        /// Определяет, может ли быть выполнена команда редактирования контакта.
        /// </summary>
        /// <param name="param">Параметр команды.</param>
        /// <returns>Возвращает true, если выбран контакт и не выполняется добавление или редактирование; иначе false.</returns>
        private bool CanEdit(object param) => IsAddingNew != true && IsEditNew != true && SelectedContact != null;

        /// <summary>
        /// Выполняет редактирование выбранного контакта.
        /// Создает копию контакта для редактирования и переключает режим редактирования.
        /// </summary>
        /// <param name="param">Параметр команды.</param>
        private void ExecuteEdit(object param)
        {
            _originalContactBeforeEdit = _selectedContacts;
            _editContactCopy = new ContactVM(new Contact(_selectedContacts.Name, _selectedContacts.PhoneNumber, _selectedContacts.Email));
            SelectedContact = _editContactCopy;
            IsEditNew = true;
            IsAddingNew = true;
        }

        /// <summary>
        /// Определяет, может ли быть выполнена команда удаления контакта.
        /// </summary>
        /// <param name="param">Параметр команды.</param>
        /// <returns>Возвращает true, если выбран контакт и не выполняется добавление или редактирование; иначе false.</returns>
        private bool CanRemove(object param) => IsAddingNew != true && IsEditNew != true && SelectedContact != null;

        /// <summary>
        /// Выполняет удаление выбранного контакта из коллекции.
        /// </summary>
        /// <param name="param">Параметр команды.</param>
        private void ExecuteRemove(object param)
        {
            _contacts.Remove(SelectedContact);
        }

        /// <summary>
        /// Событие, возникающее при изменении значения свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызывает событие PropertyChanged для указанного свойства.
        /// </summary>
        /// <param name="propertyName">Имя измененного свойства. Если не указано, используется имя вызывающего члена.</param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

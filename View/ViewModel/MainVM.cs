using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
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
        public ICommand SaveCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }
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
        public Contact Contact
        {
            get { return _contact; }
            private set
            {
                _contact = value;
                OnPropertyChanged(nameof(Contact));
            }
        }
        private void UpdateContact()
        {
            _contact.Name = _name;
            _contact.Number = _phoneNumber;
            _contact.Email = _email;
            OnPropertyChanged(nameof(Contact));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

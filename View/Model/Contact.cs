using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Model
{
    class Contact
    {
        private string _name;
        private string _number;
        private string _email;
        public Contact(string name, string number, string email)
        {
            Name = name;
            Number = number;
            Email = email;
        }
        public string Name 
        {
            get {  return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Имя введенно неправильно");
                }
                _name = value;
            } 
        }
        public string Number
        {
            get { return _number; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Номер введен неверно");
                }
                _number = value;
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Введен некорректный email");
                }
                _email = value;
            }
        }
    }
}

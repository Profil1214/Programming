using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Model.Services
{
    class ContactSerializer
    {
        public string FilePath { get; set; }
        public ContactSerializer()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string contactsFolder = Path.Combine(documentsPath, "Contacts");
            FilePath = Path.Combine(contactsFolder, "contacts.json");
        }
        public void SaveContact(Contact contact)
        {
            string directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string json = JsonConvert.SerializeObject(contact, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
        public Contact LoadContact()
        {
            if (!File.Exists(FilePath))
            {
                return null;
            }
            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<Contact>(json);
        }
    }
}

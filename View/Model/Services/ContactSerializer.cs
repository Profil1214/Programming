using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Model.Services
{
    /// <summary>
    /// Представляет сервис для сериализации и десериализации контакта в формате JSON.
    /// Обеспечивает сохранение и загрузку контакта в файл.
    /// </summary>
    class ContactSerializer
    {
        /// <summary>
        /// Возвращает или задает полный путь к файлу, в котором сохраняется контакт.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ContactSerializer"/>.
        /// Устанавливает путь к файлу contacts.json в папке "Contacts" внутри каталога документов пользователя.
        /// </summary>
        public ContactSerializer()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string contactsFolder = Path.Combine(documentsPath, "Contacts");
            FilePath = Path.Combine(contactsFolder, "contacts.json");
        }

        /// <summary>
        /// Сохраняет переданный контакт в файл в формате JSON.
        /// При необходимости создает директорию для файла.
        /// </summary>
        /// <param name="contact">Контакт для сохранения. Не может быть <c>null</c>.</param>
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

        /// <summary>
        /// Загружает контакт из файла JSON.
        /// </summary>
        /// <returns>
        /// Десериализованный объект <see cref="Contact"/>, если файл существует; 
        /// в противном случае — <c>null</c>.
        /// </returns>
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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ContactSecond.Model.Services
{
    public class ContactSerializer
    {
        /// <summary>
        /// Возвращает полный путь к файлу, в котором сохраняются контакты.
        /// </summary>
        public string FilePath { get; private set; }

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
        /// Сохраняет список контактов в файл в формате JSON.
        /// </summary>
        /// <param name="contacts">Список контактов для сохранения.</param>
        public void SaveContacts(List<Contact> contacts)
        {
            // Создаем директорию, если её нет
            string directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Сериализуем и сохраняем
            string json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        /// <summary>
        /// Загружает список контактов из файла JSON.
        /// </summary>
        /// <returns>Список контактов или пустой список, если файл не существует.</returns>
        public List<Contact> LoadContacts()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Contact>();
            }

            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<Contact>>(json);
        }
    }
}
using ShinyApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShinyApp.Services
{
    public class LocalContactService : IContactService
    {
        private readonly string _storageKey = "contacts";
        private readonly IPreferences _preferences;

        public LocalContactService(IPreferences preferences)
        {
            _preferences = preferences;
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            var contactsJson = _preferences.Get(_storageKey, "[]");
            return JsonSerializer.Deserialize<List<Contact>>(contactsJson) ?? new List<Contact>();
        }

        public async Task<Contact> AddContact(Contact contact)
        {
            var contacts = (await GetContacts()).ToList();
            contacts.Add(contact);
            SaveContacts(contacts);
            return contact;
        }

        public async Task DeleteContact(Contact contact)
        {
            var contacts = (await GetContacts()).ToList();
            contacts.RemoveAll(c =>
                c.Email == contact.Email &&
                c.Name == contact.Name &&
                c.Phone == contact.Phone);
            SaveContacts(contacts);
        }

        private void SaveContacts(List<Contact> contacts)
        {
            var contactsJson = JsonSerializer.Serialize(contacts);
            _preferences.Set(_storageKey, contactsJson);
        }
    }
}

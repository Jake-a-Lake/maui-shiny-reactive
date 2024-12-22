using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShinyApp.Infrastructure;

namespace ShinyApp.Services
{
    public class SqliteContactService : IContactService
    {
        private readonly SQLiteAsyncConnection _database;

        public SqliteContactService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Contact>().Wait(); // Create table if it doesn't exist
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await _database.Table<Contact>().ToListAsync();
        }

        public async Task<Contact> AddContact(Contact contact)
        {
            await _database.InsertAsync(contact);
            return contact;
        }

        public async Task DeleteContact(Contact contact)
        {
            await _database.DeleteAsync(contact);
        }
    }
}

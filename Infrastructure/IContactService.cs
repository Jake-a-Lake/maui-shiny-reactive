using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShinyApp.Infrastructure
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetContacts();
        Task<Contact> AddContact(Contact contact);
        Task DeleteContact(Contact contact);
    }
}

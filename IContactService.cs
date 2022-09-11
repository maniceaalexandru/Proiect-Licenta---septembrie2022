using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.Models.Repositores
{
    public interface IContactService
    {
		public Task AddContactAsync(Contact contact);
		public Task DeleteContactAsync(Contact contact);
		public Task UpdateContactAsync(Contact contact);
		public IEnumerable<Contact> ListAllContact();
		public Task<Contact> GetContactByIdAsync(int id);
	}
}

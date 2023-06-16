using System.Collections.Generic;
using System.Linq;

namespace zd2_Shaidullin.types
{
    public class PhoneBook
    {
        private Queue<Contact> contacts = new Queue<Contact>();
        
        public void AddContact(Contact contact)
        {
            contacts.Enqueue(contact); 
        }
        
        public void RemoveContact(Contact contact)
        {
            contacts = new Queue<Contact>(contacts.Where(c => c != contact)); 
        }
        
        public List<Contact> SearchContacts(string name)
        {
            return contacts.Where(c => c.Name.Contains(name)).ToList();
        }
        
        public List<Contact> GetContacts()
        {
            return contacts.ToList();
        }
    }
}
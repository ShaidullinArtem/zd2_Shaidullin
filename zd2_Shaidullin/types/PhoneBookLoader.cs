using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace zd2_Shaidullin.types
{
    public static class PhoneBoookLoader
    {
        public static void Load(PhoneBook phoneBook, string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ';' }, 2);
                if (parts.Length == 2)
                {
                    phoneBook.AddContact(new Contact { Name = parts[0], Phone = parts[1] });
                }
            }
        }

        public static void Save(PhoneBook phoneBook, string fileName)
        {
            var lines = phoneBook.GetContacts().Select(c => $"{c.Name + ','} {c.Phone}");
            File.WriteAllLines(fileName, lines);
        }
    }
}
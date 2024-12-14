using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uprajnenie4
{
    public class Contact
    {
        public string name { get; set; }
        public int totalMessageSent {  get; set; } 
        public Contact(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                this.name = name;
            }
            else
            {
                //От задача 1: В класа Contact добавете свойство за името на потребителя, което има своите get и set
               // методи.В случай на опит да се запише null в него, да се генерира автоматично
                //произволно име. (Например userXXXXX, където XXXXX е произволно число.)
                //не се генерира произволно число, а 16 bytes стринг
                Guid guid = Guid.NewGuid();
                this.name = $"user{guid}";
            }
        }
    }
}
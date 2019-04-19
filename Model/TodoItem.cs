using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.API.Model
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DateTime { get; set; }
    }
}

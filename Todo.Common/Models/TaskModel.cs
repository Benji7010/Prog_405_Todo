using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common.Models
{
    public class TaskModel
    {
        public TaskModel() 
        {
            this.Key = String.Empty;
        }
        public string Key { get; set; }
    }
}

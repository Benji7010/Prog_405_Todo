using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;

namespace Todo.Common.Extensions
{
    //TODO: 
    public static class TaskModulesExtensions
    {
        public static string ToFileName(this TaskModel model) =>
            $"{model.Key}.json";
        public static string ToFileName(string key) =>
            $"{key}.json";
    }
}

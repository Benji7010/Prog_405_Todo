using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;
using Todo.Common.Requests;
using System.Text.Json;
using Todo.Common.Extensions;

namespace Todo.Common.Services
{
    public interface ITaskService
    {
        Task CreateTaskAsync(CreateTaskRequest request);
    }

    public interface IDataServiceIO<T, TKey>
    {
        Task SaveAsync(T obj);
        Task<T> GetAsync(TKey id);
    }

    public class FileDataServiceIO : IDataServiceIO<TaskModel?, string>
    {
        private readonly string path;
        public FileDataServiceIO(string path) 
        {
            this.path = path;
        }
        
        public async Task<TaskModel?> GetAsync(string id)
        {
            try
            {
                string fileName = TaskModulesExtensions.ToFileName(id);
                string combinePath = Path.Combine(path, fileName);
                if (File.Exists(combinePath))
                {
                    Console.Write($"File Does Not Exist At Path: {combinePath}");
                    return null;
                }

                using StreamReader sr = new StreamReader(this.path);
                string text = await sr.ReadToEndAsync();
                if (string.IsNullOrWhiteSpace(text))
                {
                    Console.Write($"Empty File At Path: {combinePath}");
                    return null;
                }

                return JsonSerializer.Deserialize<TaskModel>(text);
            }
            catch (IOException)
            {
                Console.WriteLine("Get File Failed For {id}");
                throw;
            }
            catch (JsonException)
            {
                Console.WriteLine($"Getting File Failed For: {id}");
                throw;
            }
            catch (Exception) 
            {
                Console.WriteLine($"Whoops. U+1F480");
                throw;
            }
        }

        public async Task SaveAsync(TaskModel? obj)
        {
            if(obj is null)
            {
                return;
            }
            //TODO: Is file overiding silent.
            try
            {
                string fileName = obj.ToFileName();
                string combinePath = Path.Combine (this.path, fileName);
                using StreamWriter sw = new StreamWriter(this.path);
                string text = JsonSerializer.Serialize(obj);
                await sw.WriteAsync(text);
            }
            catch (IOException)
            {
                Console.WriteLine("Failed Writing File For Task {obj.Key}");
                throw;
            }
            catch (JsonException)
            {
                Console.WriteLine($"Serializing File Failed");
                throw;
            }
            catch (Exception)
            {
                Console.WriteLine($"Whoops. U+1F480");
                throw;
            }
        }
    }

    public class TaskService : ITaskService
    {
        public async Task CreateTaskAsync(CreateTaskRequest request)
        {
            await Task.CompletedTask;
        }
    }
}

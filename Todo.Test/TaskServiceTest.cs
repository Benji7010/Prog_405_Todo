using Todo.Common;
using Todo.Common.Models;
using Todo.Common.Services;

namespace Todo.Test
{
    public class ClassServiceTest
    {
        private IFileDataServiceIO service;
        public ClassServiceTest()
        {
            this.service = new DummyFileDataServiceIO();
        }

        [Fact]
        public void CreateTaskSucceeds()
        {
            var taskService = new TaskService(this.service);

        }
    }

    internal class DummyFileDataServiceIO : IFileDataServiceIO
    {
        private readonly Dictionary<string, TaskModel> data = new Dictionary<string, TaskModel>();

        public void Seed(TaskModel taskModel)
        {
            this.data.Add(taskModel.Key, taskModel);
        }

        public void Seed(IEnumerable<TaskModel> taskModels)
        {
            foreach (var t in taskModels) 
            {
                this.data.Add(t.Key, t);
            }
        }
        
        public async Task<TaskModel?> GetAsync(string key)
        {
            await Task.CompletedTask;

            if (data.ContainsKey(key))
            {
                return data[key];
            }
            else { return null; }
        }

        public async Task SaveAsync(TaskModel? obj)
        {
            await Task.CompletedTask;
            if (obj == null)
            {
                return;
            }
            if (data.ContainsKey(obj.Key))
            {
                data.Remove(obj.Key);
            }
            this.data.Add(obj.Key, obj);
        }
    }
}

using Todo.Common;
using Todo.Common.Models;
using Todo.Common.Requests;
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
        public async void CreateTaskSucceeds()
        {
            var taskService = new TaskService(this.service);
            var happyRequest = new CreateTaskRequest("Test", "Dumb Desc", DateTime.UtcNow.AddDays(1));
            var createTaskResult = await taskService.CreateTaskAsync(happyRequest);
            Assert.True(createTaskResult.IsOk());
        }

        [Fact]
        public async void UpdateTaskSucceeds()
        {
            var taskService = new TaskService(this.service);
            var happyRequest = new CreateTaskRequest("Test", "Dumb Desc", DateTime.UtcNow.AddDays(1));
            var createTaskResult = await taskService.CreateTaskAsync(happyRequest);
            if (createTaskResult.IsOk())
            {
                return;
            }
            //Get task key
            string? key = createTaskResult.GetVal();
            //Use key to find task and update the data.
            happyRequest = new CreateTaskRequest("UpdateTest", "Dumber Desc", DateTime.UtcNow.AddDays(1));
            createTaskResult = await taskService.UpdateTaskAsync(key, happyRequest);

            Assert.True(createTaskResult.IsOk());
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

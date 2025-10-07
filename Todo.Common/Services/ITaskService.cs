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
        Task<Result> CreateTaskAsync(CreateTaskRequest request);
    }
    public class TaskService : ITaskService
    {
        private readonly IFileDataServiceIO fileDataService;

        public TaskService(IFileDataServiceIO fileDataService)
        {
            this.fileDataService = fileDataService;
        }
        
        public async Task<Result> CreateTaskAsync(CreateTaskRequest request)
        {
            var modelResult = TaskModel.CreateTask(request);
            if (modelResult.IsErr())
            {
                return Result.Err(modelResult.GetErr());
            }
            await this.fileDataService.SaveAsync(modelResult.GetVal());
            return Result.Ok();
        }
    }
    
}

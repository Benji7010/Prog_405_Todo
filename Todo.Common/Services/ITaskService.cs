using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Todo.Common.Extensions;
using Todo.Common.Models;
using Todo.Common.Requests;

namespace Todo.Common.Services
{
    public interface ITaskService
    {
        Task<Result<string>> CreateTaskAsync(CreateTaskRequest request);
    }
    public class TaskService : ITaskService
    {
        private readonly IFileDataServiceIO fileDataService;
        public string Key { get; private set; }

        public TaskService(IFileDataServiceIO fDS)
        {
            fileDataService = fDS;
        }
        
        public async Task<Result<string>> CreateTaskAsync(CreateTaskRequest request)
        {
            var modelResult = TaskModel.CreateTask(request);
            if (modelResult.IsErr())
            {
                return Result<string>.Err(modelResult.GetErr());
            }
            var model = modelResult.GetVal();
            if (model == null)
            {
                return Result<string>.Err("No Model");
            }
            await fileDataService.SaveAsync(modelResult.GetVal());
            return Result<string>.Ok(model.Key);
        }

        public async Task<Result<string>> UpdateTaskAsync(string key, CreateTaskRequest request)
        {
            var modelResult = TaskModel.UpdateTask(key, request);
            if (modelResult.IsErr())
            {
                return Result<string>.Err(modelResult.GetErr());
            }
            var model = modelResult.GetVal();
            if (model == null)
            {
                return Result<string>.Err("No Model");
            }
            await fileDataService.SaveAsync(modelResult.GetVal());
            return Result<string>.Ok(model.Key);
        }
    }
    
}

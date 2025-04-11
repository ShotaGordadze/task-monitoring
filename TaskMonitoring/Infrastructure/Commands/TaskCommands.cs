using Infrastructure.Models;

namespace Infrastructure.Commands.TaskCommands;

public interface ITaskCommands
{
    public Task InsertTaskAsync(TodoTask task);
    public Task<List<TodoTask>> LoadTasksAsync();
    public Task<List<TodoTask>> LoadTasksAsync(int id);
    public Task<List<TodoTask>> LoadTasksAsync(string username);
    public Task EditTaskAsync(TodoTask task);
    public Task DeleteTaskAsync(int id);
    
}

public class TaskCommands : ITaskCommands
{
    public async Task InsertTaskAsync(TodoTask task)
    {
        var supabaseService = await SupabaseService.CreateAsync();
        var client = supabaseService.Client;
        
        await client
            .From<TodoTask>()
            .Insert(task);
    }

    public async Task<List<TodoTask>> LoadTasksAsync()
    {
        var supabaseService = await SupabaseService.CreateAsync();
        var client = supabaseService.Client;

        var response = await client
            .From<TodoTask>()
            .Get();

        return response.Models.ToList();
    }

    public async Task<List<TodoTask>> LoadTasksAsync(int id)
    {
        var supabaseService = await SupabaseService.CreateAsync();
        var client = supabaseService.Client;

        var response = await client
            .From<TodoTask>()
            .Where(x => x.UserId == id)
            .Get();

        return response.Models.ToList();;
    }  
    
    public async Task<List<TodoTask>> LoadTasksAsync(string username)
    {
        var supabaseService = await SupabaseService.CreateAsync();
        var client = supabaseService.Client;

        var user = await client
            .From<User>()
            .Where(x => x.UserName == username)
            .Get();
        
        if (user.Model == null)
            throw new ApplicationException("ასეთი მომხმარებელი არ არსებობს");

        var response = await client
            .From<TodoTask>()
            .Where(x => x.UserId == user.Model.Id)
            .Get();

        return response.Models.ToList();;
    }

    public async Task EditTaskAsync(TodoTask task)
    {
        var supabaseService = await SupabaseService.CreateAsync();
        var client = supabaseService.Client;
        
        await client
            .From<TodoTask>()
            .Where(x => x.Id == task.Id)
            .Update(task);
    }

    public async Task DeleteTaskAsync(int id)
    {
        var supabaseService = await SupabaseService.CreateAsync();
        var client = supabaseService.Client;

        await client
            .From<TodoTask>()
            .Where(x => x.Id == id)
            .Delete();
    }
}
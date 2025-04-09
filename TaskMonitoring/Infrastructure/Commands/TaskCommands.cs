using Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;
using Supabase;

namespace Infrastructure.Commands.TaskCommands;

public interface ITaskCommands
{
    public Task InsertTaskAsync(TodoTask task);
    public Task<List<TodoTask>> LoadTasksAsync();
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
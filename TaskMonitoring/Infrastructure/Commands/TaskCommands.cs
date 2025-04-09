using Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;
using Supabase;

namespace Infrastructure.Commands.TaskCommands;

public interface ITaskCommands
{
    public Task InsertTaskAsync();
    public Task<List<TodoTask>> LoadTasksAsync();
}

public class TaskCommands : ITaskCommands
{
    public async Task InsertTaskAsync()
    {
        var supabaseService = await SupabaseService.CreateAsync();
        var client = supabaseService.Client;
        
        var task = new TodoTask
        {
            Initiator = "Shota",
            Project = "Bugsy",
            Content = "Create Supabase insert test",
            StartDate = DateTime.UtcNow.Date,
            EndDate = DateTime.UtcNow.Date.AddDays(3),
            Status = "Pending",
            Comment = "Auto-inserted test"
        };
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
}
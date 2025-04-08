using Infrastructure.Models;
using Supabase;

namespace Infrastructure.Commands.TaskCommands;

public class TaskCommands
{
    private readonly SupabaseService _supabaseService;
    
    public TaskCommands()
    {
        _supabaseService = App.Services.GetRequiredService<SupabaseService>();
        _client = App
    }
    
    
    public async Task InsertTask()
    {
        var _client = _supabaseService.Client;

        var task = new TodoTask()
        {
            Description = "TESTING TASK INSERT FUNC"
        };

        await _client
            .From<TodoTask>()
            .Insert(task);
    }

    public async Task<List<TodoTask>> LoadTasks()
    {
        var client = _supabaseService.Client;

        var response = await client
            .From<TodoTask>()
            .Get();

        return response.Models.ToList();
    }
}
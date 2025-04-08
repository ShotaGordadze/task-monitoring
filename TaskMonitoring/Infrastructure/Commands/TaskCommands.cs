using Infrastructure.Models;
using Supabase;

namespace Infrastructure.Commands.TaskCommands;

public class TaskCommands
{
    private static readonly SupabaseService _supabaseService = new();

    public async Task InsertTask()
    {
        await _supabaseService.InitializeAsync();
        
        var _client = _supabaseService.Client;

        var task = new TodoTask()
        {
            Description = "TESTING TASK INSERT FUNC"
        };

        await _client
            .From<TodoTask>()
            .Insert(task);
    }
}
using Infrastructure.Models;

namespace Infrastructure.Commands.TaskCommands;

public interface IUserCommands
{
    public Task<ICollection<User>> GetAllUsersAsync();
}

public class UserCommands : IUserCommands
{
    public async Task<ICollection<User>> GetAllUsersAsync()
    {
        var supabaseService = await SupabaseService.CreateAsync();
        var client = supabaseService.Client;

        var users = await client
            .From<User>()
            .Where(x => x.RoleId == 2)
            .Get();
        
        return users.Models.ToHashSet();
    }
}
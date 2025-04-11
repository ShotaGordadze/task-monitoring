using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Infrastructure.Models;

namespace Infrastructure.Commands.TaskCommands;

public interface IAuthCommands
{
    public Task<User> LogIn(string username, string userPassword);

    public Task LogOut();

    public Task<User> SignIn(User user);

    public Task<User> GetUserAsync(int id);

    public string EncryptPassword(string password);
}

public class AuthCommands : IAuthCommands
{
    private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "userInfo.json");

    public async Task<User> LogIn(string username, string userPassword)
    {
        var supabaseService = await SupabaseService.CreateAsync();
        var client = supabaseService.Client;

        userPassword = EncryptPassword(userPassword);

        var response = await client.From<User>()
            .Where(x => x.UserName == username && x.Password == userPassword)
            .Get();

        if (response.Model != null)
        {
            var userRole = response.Model.RoleId switch
            {
                1 => Roles.Moderator.ToString(),
                2 => Roles.User.ToString(),
                _ => "N/A"
            };

            var jsonData = new
            {
                id = response.Model.Id,
                name = response.Model.Name,
                last_name = response.Model.LastName,
                username = response.Model.UserName,
                password = userPassword,
                role = userRole,
                is_logged_in = "1"
            };

            var json = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions { WriteIndented = true });

            await File.WriteAllTextAsync(_path, json);
            
            return response.Model;
        }

        throw new Exception("მომხმარებელი ან პაროლი არასწორია!");
    }

    public async Task LogOut()
    {
        var jsonData = new
        {
            name = string.Empty,
            last_name = string.Empty,
            username = string.Empty,
            password = string.Empty,
            role = string.Empty,
            is_logged_in = "0"
        };

        var json = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions { WriteIndented = true });

        await File.WriteAllTextAsync(_path, json);
    }

    public async Task<User> SignIn(User user)
    {
        var supabaseService = await SupabaseService.CreateAsync();
        var client = supabaseService.Client;

        var response  = await client.From<User>()
            .Where(x => x.UserName == user.UserName)
            .Get();

        if (response.Model != null)
        {
            throw new Exception($"ასეთი იუზერი უკვე არსებობს {response.Model.UserName}");
        }

        var insertedUser = await client.From<User>()
            .Insert(user);

        var userRole = user.RoleId switch
        {
            1 => Roles.Moderator.ToString(),
            2 => Roles.User.ToString(),
            _ => "N/A"
        };

        var jsonData = new
        {
            id = insertedUser.Model.Id,
            name = user.Name,
            last_name = user.LastName,
            username = user.UserName,
            password = user.Password,
            role = userRole,
            is_logged_in = "1"
        };

        var json = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions { WriteIndented = true });

        await File.WriteAllTextAsync(_path, json);

        return insertedUser.Model;
    }

    public async Task<User> GetUserAsync(int id)
    {
        var supabaseService = await SupabaseService.CreateAsync();
        var client = supabaseService.Client;

        var response = await client.From<User>()
            .Where(x => x.Id == id)
            .Get();

        return response.Model ??
               throw new Exception($"მომხმარებელი Id-ით: {id} არ არსებობს");
    } 
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        var supabaseService = await SupabaseService.CreateAsync();
        var client = supabaseService.Client;

        var response = await client.From<User>()
            .Where(x => x.UserName == username)
            .Get();

        return response.Model ??
               throw new Exception($"მომხმარებელი {username} არ არსებობს");
    }

    public string EncryptPassword(string password)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hash);
    }
}
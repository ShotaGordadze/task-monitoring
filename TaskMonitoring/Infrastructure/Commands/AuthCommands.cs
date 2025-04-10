using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Infrastructure.Models;

namespace Infrastructure.Commands.TaskCommands;

public interface IAuthCommands
{
    public Task LogIn(string username, string password);

    public Task LogOut();

    public Task SignIn(User user);

    public string EncryptPassword(string password);
}

public class AuthCommands : IAuthCommands
{
    private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "userInfo.json");

    public async Task LogIn(string username, string password)
    {
        var supabaseService = await SupabaseService.CreateAsync();
        var client = supabaseService.Client;

        password = EncryptPassword(password);

        var response = await client.From<User>()
            .Where(x => x.UserName == username && x.Password == password)
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
                role = userRole,
                is_logged_in = "1"
            };

            var json = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions { WriteIndented = true });

            await File.WriteAllTextAsync(_path, json);
        }
        else
        {
            throw new Exception("მომხმარებელი ან პაროლი არასწორია!");
        }
    }

    public async Task LogOut()
    {
        var jsonData = new
        {
            name = string.Empty,
            last_name = string.Empty,
            username = string.Empty,
            role = string.Empty,
            is_logged_in = "0"
        };

        var json = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions { WriteIndented = true });

        await File.WriteAllTextAsync(_path, json);
        //ToDo
    }

    public async Task SignIn(User user)
    {
        var supabaseService = await SupabaseService.CreateAsync();
        var client = supabaseService.Client;

        var response = await client.From<User>()
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
            role = userRole,
            is_logged_in = "1"
        };

        var json = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions { WriteIndented = true });

        await File.WriteAllTextAsync(_path, json);
    }

    public string EncryptPassword(string password)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hash);
    }
}
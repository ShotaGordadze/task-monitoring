using System.Text.Json;
using Supabase;

namespace Infrastructure;

public class SupabaseService
{
    private string _supabaseUrl;
    private string _supabaseKey;
    private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json");

    private Client _client;

    public SupabaseService()
    {
        Load(_path);
        InitializeAsync();
    }

     private async Task InitializeAsync()
    {
        var options = new SupabaseOptions
        {
            AutoConnectRealtime = false
        };

        _client = new Client(
            _supabaseUrl,
            _supabaseKey,
            options);

        await _client.InitializeAsync();
    }

    private void Load(string path)
    {
        var json = File.ReadAllText(path);
        var config = JsonSerializer.Deserialize<SupabaseConfig>(json);

        if (config == null)
        {
            throw new ApplicationException("Invalid configuration"); //ToDo
        }

        _supabaseUrl = config.SupabaseUrl;
        _supabaseKey = config.SupabaseKey;
    }
}

internal class SupabaseConfig
{
    internal string SupabaseUrl { get; init; } = null!;
    internal string SupabaseKey { get; init; } = null!;
}
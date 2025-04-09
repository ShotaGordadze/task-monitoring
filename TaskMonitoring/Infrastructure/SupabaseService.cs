using System.Text.Json;
using System.Text.Json.Serialization;
using Supabase;

namespace Infrastructure;

public interface ISupabaseService
{
    Client Client { get; }
}

public class SupabaseService : ISupabaseService
{
    private string _supabaseUrl;
    private string _supabaseKey;
    private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json");

    public Client Client { get; private set; }

    private SupabaseService() { }

    public static async Task<ISupabaseService> CreateAsync()
    {
        var service = new SupabaseService();
        service.LoadConfig(service._path);

        var options = new SupabaseOptions
        {
            AutoConnectRealtime = false
        };

        service.Client = new Client(service._supabaseUrl, service._supabaseKey, options);
        await service.Client.InitializeAsync();

        return service;
    }

    private void LoadConfig(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"Config file not found at {path}");

        var json = File.ReadAllText(path);
        var config = JsonSerializer.Deserialize<SupabaseConfig>(json)
                     ?? throw new ApplicationException("Invalid configuration");

        _supabaseUrl = config.SupabaseUrl ?? throw new ArgumentNullException(nameof(config.SupabaseUrl));
        _supabaseKey = config.SupabaseKey ?? throw new ArgumentNullException(nameof(config.SupabaseKey));
    }
}

internal class SupabaseConfig
{
    [JsonPropertyName("supabaseUrl")]
    public string SupabaseUrl { get; init; }

    [JsonPropertyName("supabaseKey")]
    public string SupabaseKey { get; init; }
}
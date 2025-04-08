using System.Text.Json;
using System.Text.Json.Serialization;
using Supabase;

namespace Infrastructure;

public class SupabaseService
{
    private string _supabaseUrl;
    private string _supabaseKey;
    private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json");

    private Client _client;
    private bool _initialized = false;

    public Client Client => _client;

    public SupabaseService()
    {
        Load(_path);
    }

    public async Task InitializeAsync()
    {
        if (_initialized)
            return;

        var options = new SupabaseOptions
        {
            AutoConnectRealtime = false
        };

        _client = new Client(_supabaseUrl, _supabaseKey, options);
        await _client.InitializeAsync();

        _initialized = true;
    }


    private void Load(string path)
    {
        var json = File.ReadAllText(path);
        var config = JsonSerializer.Deserialize<SupabaseConfig>(json)
                     ?? throw new ApplicationException("Invalid configuration");

        _supabaseUrl = config.SupabaseUrl;
        _supabaseKey = config.SupabaseKey;
    }
}

internal class SupabaseConfig
{
    [JsonPropertyName("supabaseUrl")]
    public string SupabaseUrl { get; init; }
    
    [JsonPropertyName("supabaseKey")]
    public string SupabaseKey { get; init; }
}
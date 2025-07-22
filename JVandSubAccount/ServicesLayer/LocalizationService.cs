using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Text.Json;

namespace JVandSubAccount.ServicesLayer
{
    public class LocalizationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LocalizationService> _logger;
        private readonly Dictionary<string, Dictionary<string, JsonElement>> _loadedResources = new();

        public LocalizationService(HttpClient httpClient, ILogger<LocalizationService> logger, NavigationManager nav)
        {
            httpClient.BaseAddress = new Uri(nav.BaseUri);
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task LoadStringsAsync(string culture, string module)
        {
            try
            {
                if (!_loadedResources.ContainsKey(culture))
                    _loadedResources[culture] = new Dictionary<string, JsonElement>();

                if (_loadedResources[culture].ContainsKey(module))
                    return;

                var response = await _httpClient.GetAsync($"/locales/{culture}/{module}.json");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var parsed = JsonSerializer.Deserialize<JsonElement>(json);

                _loadedResources[culture][module] = parsed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load localization for {Culture}/{Module}", culture, module);

                if (culture != "en")
                    await LoadStringsAsync("en", module);
            }
        }

        public string GetString(string key)
        {
            var culture = CultureInfo.CurrentCulture.Name;

            var parts = key.Split(':');
            if (parts.Length < 2)
                return $"[{key}]";

            string module = parts[0];
            string[] path = parts.Skip(1).ToArray();

            return TryGetLocalizedString(culture, module, path) ??
                   TryGetLocalizedString(culture[..2], module, path) ??
                   TryGetLocalizedString("en", module, path) ??
                   $"[{key}]";
        }

        private string? TryGetLocalizedString(string culture, string module, string[] path)
        {
            if (_loadedResources.TryGetValue(culture, out var moduleDict) &&
                moduleDict.TryGetValue(module, out var json))
            {
                var current = json;
                foreach (var segment in path)
                {
                    if (!current.TryGetProperty(segment, out current))
                        return null;
                }

                return current.GetString();
            }

            return null;
        }

        public string this[string key] => GetString(key);
        public bool IsRTL => CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;
    }
}


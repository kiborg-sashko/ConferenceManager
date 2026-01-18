using System.Net.Http.Json;
using System.Diagnostics;
using ConferenceManager.Core.Entities; // Додайте посилання на проект Core

namespace ConferenceManager.Client.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        // Використовуємо тернарний оператор безпосередньо при створенні HttpClient
        var baseUrl = DeviceInfo.Platform == DevicePlatform.Android 
                      ? "http://10.0.2.2:5000" 
                      : "http://localhost:5000";

        _httpClient = new HttpClient { 
            BaseAddress = new Uri(baseUrl),
            Timeout = TimeSpan.FromSeconds(10) // Додайте таймаут, щоб додаток не вис на вічно
        };
    }

    public async Task<List<Conference>> GetConferencesAsync()
    {
        try 
        {
            // Додаємо '/' перед api, щоб шлях будувався коректно від кореня BaseAddress
            var response = await _httpClient.GetFromJsonAsync<List<Conference>>("api/v1/Conferences");
            return response ?? new List<Conference>();
        }
        catch (Exception ex)
        {
            // Це допоможе побачити реальну помилку в дебаг-консолі
            Debug.WriteLine(@"ERROR {0}", ex.Message);
            return new List<Conference>();
        }
    }
}
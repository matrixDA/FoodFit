using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using CommunityToolkit.Mvvm.ComponentModel;
using FoodFit.Models;
using Newtonsoft.Json.Linq;

public partial class SearchViewModel : ObservableObject
{
    [ObservableProperty]
    private string searchText;

    [ObservableProperty]
    private ObservableCollection<FoodItem> filteredCategories = new();

    private readonly HttpClient _httpClient;

    public SearchViewModel()
    {
        _httpClient = new HttpClient();
    }

    partial void OnSearchTextChanged(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            _ = SearchFoodsAsync(value);
        }
    }

    private async Task SearchFoodsAsync(string query)
    {
        try
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.nal.usda.gov/fdc/v1/foods/search?query={Uri.EscapeDataString(query)}&pageSize=10"),
                Headers =
            {
                { "X-Api-Key", "tSR3ifnJ3RKqXw4Ucx7L2kCmdLa3CPjXkbD6puJ5" }
            }
            };

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine("======= USDA API RESPONSE =======");
            Console.WriteLine(body);

            var json = JObject.Parse(body);
            var foods = json["foods"];

            if (foods == null)
            {
                Console.WriteLine("⚠️ No 'foods' field in response.");
                return;
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                FilteredCategories.Clear();
                foreach (var food in foods)
                {
                    FilteredCategories.Add(new FoodItem
                    {
                        Name = food["description"]?.ToString(),
                        Calories = food["foodNutrients"]?.FirstOrDefault(n => n["nutrientName"]?.ToString() == "Energy")?["value"]?.ToObject<float>() ?? 0,
                        Carbs = food["foodNutrients"]?.FirstOrDefault(n => n["nutrientName"]?.ToString() == "Carbohydrate, by difference")?["value"]?.ToObject<float>() ?? 0,
                        Protein = food["foodNutrients"]?.FirstOrDefault(n => n["nutrientName"]?.ToString() == "Protein")?["value"]?.ToObject<float>() ?? 0,
                        Fat = food["foodNutrients"]?.FirstOrDefault(n => n["nutrientName"]?.ToString() == "Total lipid (fat)")?["value"]?.ToObject<float>() ?? 0
                    });
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API error: {ex.Message}");
        }
    }
}
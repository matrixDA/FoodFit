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
                RequestUri = new Uri($"https://macronutrient-search.p.rapidapi.com/search?search={Uri.EscapeDataString(query)}&pageNumber=1&pageSize=10&apiKey=<USDA_API_KEY>"),
                Headers =
                {
                    { "x-rapidapi-key", "da26e47021msh387af52d2366d4dp197248jsn0789a156b97e" },
                    { "x-rapidapi-host", "macronutrient-search.p.rapidapi.com" },
                    { "Accept", "application/json" },
                }
            };

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(body);

            var results = json["results"];

            FilteredCategories.Clear();
            foreach (var item in results)
            {
                FilteredCategories.Add(new FoodItem
                {
                    Name = item["name"]?.ToString(),
                    Calories = item["calories"]?.ToObject<float>() ?? 0,
                    Carbs = item["carbohydrates"]?.ToObject<float>() ?? 0,
                    Protein = item["protein"]?.ToObject<float>() ?? 0,
                    Fat = item["fat"]?.ToObject<float>() ?? 0
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API error: {ex.Message}");
        }
    }
}

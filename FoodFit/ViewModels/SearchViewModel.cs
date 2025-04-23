//using System.Collections.ObjectModel;
//using System.Net.Http;
//using CommunityToolkit.Mvvm.ComponentModel;
//using FoodFit.Models;
//using Newtonsoft.Json.Linq;

//public partial class SearchViewModel : ObservableObject
//{
//    [ObservableProperty]
//    private string searchText;

//    [ObservableProperty]
//    private ObservableCollection<FoodItem> filteredCategories = new();

//    private readonly HttpClient _httpClient;

//    public SearchViewModel()
//    {
//        _httpClient = new HttpClient();
//    }



//    partial void OnSearchTextChanged(string value)
//    {
//        if (!string.IsNullOrWhiteSpace(value))
//        {
//            _ = SearchFoodsAsync(value);
//        }
//    }

//    private async Task SearchFoodsAsync(string query)
//    {
//        try
//        {
//            var request = new HttpRequestMessage
//            {
//                Method = HttpMethod.Get,
//                RequestUri = new Uri($"https://api.nal.usda.gov/fdc/v1/foods/search?query={Uri.EscapeDataString(query)}&pageSize=10&api_key=tSR3ifnJ3RKqXw4Ucx7L2kCmdLa3CPjXkbD6puJ5")
//            };

//            using var response = await _httpClient.SendAsync(request);
//            response.EnsureSuccessStatusCode();

//            var body = await response.Content.ReadAsStringAsync();
//            var json = JObject.Parse(body);
//            var foods = json["foods"];

//            if (foods == null)
//                return;

//            MainThread.BeginInvokeOnMainThread(() =>
//            {
//                FilteredCategories.Clear();
//                foreach (var food in foods)
//                {
//                    FilteredCategories.Add(new FoodItem
//                    {
//                        Name = food["description"]?.ToString(),
//                        Calories = food["foodNutrients"]?.FirstOrDefault(n => n["nutrientName"]?.ToString() == "Energy")?["value"]?.ToObject<float>() ?? 0,
//                        Carbs = food["foodNutrients"]?.FirstOrDefault(n => n["nutrientName"]?.ToString().Contains("Carbohydrate") == true)?["value"]?.ToObject<float>() ?? 0,
//                        Protein = food["foodNutrients"]?.FirstOrDefault(n => n["nutrientName"]?.ToString().Contains("Protein") == true)?["value"]?.ToObject<float>() ?? 0,
//                        Fat = food["foodNutrients"]?.FirstOrDefault(n => n["nutrientName"]?.ToString().Contains("Total lipid") == true)?["value"]?.ToObject<float>() ?? 0,
//                        FdcId = food["fdcId"]?.ToObject<int>() ?? 0
//                    });
//                }
//            });
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"API error: {ex.Message}");
//        }
//    }

//    public async Task<FoodItem?> GetFoodDetailsAsync(int fdcId)
//    {
//        try
//        {
//            string apiKey = "tSR3ifnJ3RKqXw4Ucx7L2kCmdLa3CPjXkbD6puJ5";
//            string url = $"https://api.nal.usda.gov/fdc/v1/food/{fdcId}?format=full&api_key={apiKey}";

//            var request = new HttpRequestMessage
//            {
//                Method = HttpMethod.Get,
//                RequestUri = new Uri(url)
//            };

//            var response = await _httpClient.SendAsync(request);
//            response.EnsureSuccessStatusCode();

//            var content = await response.Content.ReadAsStringAsync();
//            var json = JObject.Parse(content);

//            var nutrients = json["foodNutrients"];

//            float GetNutrientValue(string nutrientName)
//            {
//                var nutrient = nutrients?.FirstOrDefault(n => n["nutrientName"]?.ToString().Contains(nutrientName) == true);
//                return nutrient?["value"]?.ToObject<float>() ?? 0;
//            }

//            return new FoodItem
//            {
//                Name = json["description"]?.ToString(),
//                Calories = GetNutrientValue("Energy"),
//                Protein = GetNutrientValue("Protein"),
//                Carbs = GetNutrientValue("Carbohydrate"),
//                Fat = GetNutrientValue("Total lipid"),
//                FdcId = fdcId
//            };
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error fetching food details: {ex.Message}");
//            return null;
//        }
//    }
//}

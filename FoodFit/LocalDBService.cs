using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodFit.Models;
using SQLite;

namespace FoodFit
{
    public class LocalDBService
    {
        private const string DB_NAME = "foodfit_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDBService()
        {
            // Creating the tables
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<User>();
            _connection.CreateTableAsync<DailyNutrition>();
            _connection.CreateTableAsync<FoodLog>();
            _connection.CreateTableAsync<StepCount>();


            _connection.CreateTableAsync<Foods>();

            // Enable foreign key constraints
            _connection.ExecuteAsync("PRAGMA foreign_keys = ON");

            // Indexing the foreign keys
            _connection.CreateIndexAsync("IX_DailyNutrition_UserId_EntryDate", "DailyNutrition", new[] { "user_id, entry_date" });
            _connection.CreateIndexAsync("IX_FoodLog_UserId_EntryDate", "FoodLog", new[] { "user_id, entry_date" });
            _connection.CreateIndexAsync("IX_Users_Username", "Users", "username", unique: true);
            _connection.CreateIndexAsync("IX_Users_Email", "Users", "email", unique: true);
            _connection.CreateIndexAsync("IX_StepCount_UserId_EntryDate", "StepCount", new[] { "user_id, entry_date" });
        }


        // Helper Services for User
        public async Task<User> GetUserById(int id)
        {
            return await _connection.Table<User>().Where(u => u.UserId == id).FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByUserName(string username, string password)
        {
            return await _connection.Table<User>().Where(u => u.UserName == username && u.PasswordHash == password).FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _connection.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
        }
        public async Task CreateUser(User user)
        {
            await _connection.InsertAsync(user);
        }
        // need to add more helper services to change weight goals and current weight

        public async Task DeleteUser(User user)
        {
            await _connection.DeleteAsync(user);
        }
     
      

        public async Task UpdateUser(int id, double weightGoal)
        {
            var user = await GetUserById(id);
            user.GoalWeight = weightGoal;
            await _connection.UpdateAsync(user);
        }

        public async Task UpdateUserWeight(int id, double currentWeight)
        {
            var user = await GetUserById(id);
            user.CurrentWeight = currentWeight;
            await _connection.UpdateAsync(user);
        }

        // Helper Services for Daily Nutrition
        public async Task<DailyNutrition> GetDailyNutrition(int userId, DateTime date)
        {
            return await _connection.Table<DailyNutrition>().Where(dn => dn.UserId == userId && dn.EntryDate == date.Date).FirstOrDefaultAsync();
        }

        public async Task UpdateDailyNutrition(int userId, DateTime date, double totalCal, double totalProtein, double totalCarbs, double totalFat)
        {
            var existingEntry = await GetDailyNutrition(userId, date);
            existingEntry.TotalCarbs += totalCarbs;
            existingEntry.TotalProtein += totalProtein;
            existingEntry.TotalCalories += totalCal;
            existingEntry.TotalFats += totalFat;
            await _connection.UpdateAsync(existingEntry);

        }

        public async Task CreateDailyNutrition(DailyNutrition dailyNutrition)
        {
            await _connection.InsertAsync(dailyNutrition);
        }

        // More helper code needed to change specific total nutrition data



        // Helper Services for Daily Food Log
        public async Task<FoodLog> GetFoodLogEntry(int logId)
        {
            return await _connection.Table<FoodLog>().Where(u => u.LogId == logId).FirstOrDefaultAsync();
        }
        public async Task<List<FoodLog>> GetFoodLogsForDateAndMeal(int userId, DateTime date)
        {
            return await _connection.Table<FoodLog>().Where(fl => fl.UserId == userId && fl.EntryDate == date.Date).ToListAsync();
        }
        public async Task<List<FoodLog>> GetFoodLogsForDMT(int userId, DateTime date, MealType mealType)
        {
            return await _connection.Table<FoodLog>().Where(fl => fl.UserId == userId && fl.EntryDate == date.Date && fl.MealType == mealType).ToListAsync();
        }
        public async Task CreateFoodLogEntry(FoodLog foodLog)
        {
            await _connection.InsertAsync(foodLog);
        }
        public async Task DeleteFoodLogEntry(FoodLog foodLog)
        {
            await _connection.DeleteAsync(foodLog);
        }

        // Helper Service for Step Count
        public async Task<StepCount> GetStepCount(int userId, DateTime date)
        {
            return await _connection.Table<StepCount>().Where(u => u.UserId == userId && u.EntryDate == date.Date).FirstOrDefaultAsync();
        }
        public async Task CreateStepLogEntry(StepCount stepCount)
        {
            await _connection.InsertAsync(stepCount);
        }
        public async Task UpdateStepLogEntry(int userId, DateTime date, double caloriesBurned, double steps )
        {
            var existingEntry = await GetStepCount(userId, date);
            existingEntry.Steps = steps;
            existingEntry.CaloriesBurned = caloriesBurned;
            await _connection.UpdateAsync(existingEntry);
        }

        public async Task AddFoodItem(Foods food)
        {
            await _connection.InsertAsync(food);
        }

        public async Task<List<Foods>> GetAllFoods()
        {
            return await _connection.Table<Foods>().ToListAsync();
        }
        public async Task<List<Foods>> GetFoodsByName(string foodName)
        {
            foodName.Trim();
            return await _connection.Table<Foods>().Where(f => f.FoodName.ToLower().Contains(foodName.ToLower())).ToListAsync();
        }
    }
}

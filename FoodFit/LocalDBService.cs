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

            // Enable foreign key constraints
            _connection.ExecuteAsync("PRAGMA foreign_keys = ON");

            // Indexing the foreign keys
            _connection.CreateIndexAsync("IX_DailyNutrition_UserId_EntryDate", "DailyNutrition", new[] { "user_id, entry_date" }, unique: true);
            _connection.CreateIndexAsync("IX_FoodLog_UserId_EntryDate", "FoodLog", new[] { "user_id, entry_date" });
            _connection.CreateIndexAsync("IX_Users_Username", "Users", "username", unique: true);
            _connection.CreateIndexAsync("IX_Users_Email", "Users", "email", unique: true);
        }


        // Helper Services for User
        public async Task<User> GetUserById(int id)
        {
            return await _connection.Table<User>().Where(u => u.UserId == id).FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByUserName(string username)
        {
            return await _connection.Table<User>().Where(u => u.UserName == username).FirstOrDefaultAsync();
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

        // Helper Services for Daily Nutrition
        public async Task<DailyNutrition> GetDailyNutrition(int userId, DateTime date)
        {
            return await _connection.Table<DailyNutrition>().Where(dn => dn.UserId == userId && dn.EntryDate.Date == date.Date).FirstOrDefaultAsync();
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
        public async Task CreateFoodLogEntry(FoodLog foodLog)
        {
            await _connection.InsertAsync(foodLog);
        }
        public async Task DeleteFoodLogEntry(FoodLog foodLog)
        {
            await _connection.DeleteAsync(foodLog);
        }
    }
}

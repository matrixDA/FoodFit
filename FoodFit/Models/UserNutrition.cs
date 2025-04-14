using System;
using System.Collections.Generic;
using SQLite;

namespace FoodFit.Models
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        [Column("user_id")]
        public int UserId { get; set; }
        [Unique, NotNull]
        [Column("username")]
        public string UserName { get; set; }
        [NotNull]
        [Column("password_hash")]
        public string PasswordHash { get; set; }
        [Unique, NotNull]
        [Column("email")]
        public string Email { get; set; }
        [NotNull]
        [Column("current_weight")]
        public double CurrentWeight { get; set; }
        [NotNull]
        [Column("goal_weight")]
        public double GoalWeight { get; set; }
        [NotNull]
        [Column("height")]
        public double height { get; set; }

    }

    [Table("DailyNutrition")]
    public class DailyNutrition
    {
        [PrimaryKey, AutoIncrement]
        [Column("entry_id")]
        public int EntryId { get; set; }
        [Column("user_id")]
        public int UserId { get; set;}          // foreign key
        [Column("total_calories")]
        public double TotalCalories { get; set; }
        [Column("total_protein")]
        public double TotalProtein { get; set; }
        [Column("total_carbs")]
        public double TotalCarbs { get; set; }
        [Column("total_fats")]
        public double TotalFats { get; set; }
        [NotNull]
        [Column("entry_date")]
        public DateTime EntryDate { get; set; }
    }

    [Table("FoodLog")]
    public class FoodLog
    {
        [PrimaryKey, AutoIncrement]
        [Column("log_id")]
        public int LogId { get; set; }
        [Column ("user_id")]    
        public int UserId { get; set; }             // foreign key
        [NotNull]
        [Column("food_name")]
        public string FoodName { get; set; }
        [NotNull]
        [Column("quantity")]
        public double Quantity { get; set; }
        [Column("unit")]
        public string Unit { get; set; }
        [Column("calories")]
        public double Caloroies { get; set; }
        [Column("protein")]
        public double Protein { get; set; }
        [Column("carbs")]
        public double Carbs { get; set; }
        [Column("fat")]
        public double Fat { get; set; }
        [NotNull]
        [Column("meal_type")]
        public MealType MealType { get; set; }
        [NotNull]
        [Column("entry_date")]
        public DateTime EntryDate { get; set; }
    }

    public enum MealType
    {
        Breakfast,
        Lunch,
        Dinner,
        Snack
    }
}

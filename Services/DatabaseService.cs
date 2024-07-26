using System;
using Caisse_Leoni_gm7.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Caisse_Leoni_gm7.Services
{
    public class DatabaseService
    {
        private static SQLiteAsyncConnection _database;
        private static string _dbPath;

        public DatabaseService(string dbPath)
        {
            _dbPath = dbPath;
            _database = new SQLiteAsyncConnection(dbPath);
            Console.WriteLine($"Database path: {_dbPath}");
            // Removed Wait(), we'll call CreateTablesAsync separately
        }

        public async Task CreateTablesAsync()
        {
            try
            {
                await _database.CreateTableAsync<User>();
                Console.WriteLine("User table created.");

                await _database.CreateTableAsync<TwoFactorAuth>();
                Console.WriteLine("TwoFactorAuth table created.");

                await _database.CreateTableAsync<Log>();
                Console.WriteLine("Log table created.");

                await _database.CreateTableAsync<Depense>();
                Console.WriteLine("Depense table created.");

                await _database.CreateTableAsync<Alimentation>();
                Console.WriteLine("Alimentation table created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create table: {ex.Message}");
            }
        }

        // Method to retrieve all users
        public async Task<List<User>> GetUsersAsync()
        {
            return await _database.Table<User>().ToListAsync();
        }

        // Method to save User
        public Task<int> SaveUserAsync(User user)
        {
            if (user.Id != 0)
            {
                return _database.UpdateAsync(user);
            }
            else
            {
                return _database.InsertAsync(user);
            }
        }

        // Method to save TwoFactorAuth
        public Task<int> SaveTwoFactorAuthAsync(TwoFactorAuth twoFactorAuth)
        {
            if (twoFactorAuth.Id != 0)
            {
                return _database.UpdateAsync(twoFactorAuth);
            }
            else
            {
                return _database.InsertAsync(twoFactorAuth);
            }
        }

        // Method to save Log
        public Task<int> SaveLogAsync(Log log)
        {
            if (log.Id != 0)
            {
                return _database.UpdateAsync(log);
            }
            else
            {
                return _database.InsertAsync(log);
            }
        }

        // Method to save Depense
        public Task<int> SaveDepenseAsync(Depense depense)
        {
            if (depense.Id != 0)
            {
                return _database.UpdateAsync(depense);
            }
            else
            {
                return _database.InsertAsync(depense);
            }
        }

        // Method to save Alimentation
        public Task<int> SaveAlimentationAsync(Alimentation alimentation)
        {
            if (alimentation.Id != 0)
            {
                return _database.UpdateAsync(alimentation);
            }
            else
            {
                return _database.InsertAsync(alimentation);
            }
        }
    }
}

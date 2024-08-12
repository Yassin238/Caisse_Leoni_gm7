using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Caisse_Leoni_gm7.Models;

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
        }

        public async Task CreateTablesAsync()
        {
            try
            {
                await _database.CreateTableAsync<User>();
                Console.WriteLine("User table created.");

                await _database.CreateTableAsync<Log>();
                Console.WriteLine("Log table created.");

                await _database.CreateTableAsync<Depense>();
                Console.WriteLine("Depense table created.");

                await _database.CreateTableAsync<Alimentation>();
                Console.WriteLine("Alimentation table created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Table creation failed: {ex.Message}");
            }
        }

        public async Task InsertTestDataAsync()
        {
            try
            {
                var testUser = new User { Username = "test", Password = "test", IsAdmin = false, IsFirstLogin = false };
                var testAdmin = new User { Username = "admin", Password = "gm7", IsAdmin = true, IsFirstLogin = false };

                await SaveUserAsync(testUser);
                await SaveUserAsync(testAdmin);
                Console.WriteLine("Test data inserted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test data insertion failed: {ex.Message}");
            }
        }

        // User
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

        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }


        // Log
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

        public async Task<List<Log>> GetLogsAsync()
        {
            return await _database.Table<Log>().ToListAsync();
        }

        // Depense
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

        public Task<int> DeleteDepenseAsync(Depense depense)
        {
            return _database.DeleteAsync(depense);
        }

        public Task<List<Depense>> GetDepensesAsync()
        {
            return _database.Table<Depense>().ToListAsync();
        }

        // Alimentation
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

        public Task<int> DeleteAlimentationAsync(Alimentation alimentation)
        {
            return _database.DeleteAsync(alimentation);
        }

        public Task<List<Alimentation>> GetAlimentationsAsync()
        {
            return _database.Table<Alimentation>().ToListAsync();
        }


    }
}

using Caisse_Leoni_gm7.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Caisse_Leoni_gm7
{
    public partial class App : Application
    {
        static DatabaseService databaseService;

        public static DatabaseService DatabaseService
        {
            get
            {
                if (databaseService == null)
                {
                    InitializeDatabaseAsync().Wait();
                }
                return databaseService;
            }
        }

        public static async Task InitializeDatabaseAsync()
        {
            if (databaseService == null)
            {
                try
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CaisseLeoniGM7.db3");
                    Console.WriteLine($"Database path: {dbPath}");
                    databaseService = new DatabaseService(dbPath);
                    Console.WriteLine("Database initialized successfully.");
                    await databaseService.CreateTablesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database initialization failed: {ex.Message}");
                }
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }
    }
}

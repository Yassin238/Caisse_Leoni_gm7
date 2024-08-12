using Caisse_Leoni_gm7.Services;
using Caisse_Leoni_gm7.Views;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Caisse_Leoni_gm7
{
    public partial class App : Application
    {
        static DatabaseService databaseService;

        public static DatabaseService DatabaseService => databaseService;

        public static async Task InitializeDatabaseAsync()
        {
            if (databaseService == null)
            {
                try
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CaisseLeoniGM7.db3");
                    Console.WriteLine($"Database path: {dbPath}");
                    databaseService = new DatabaseService(dbPath);
                    await databaseService.CreateTablesAsync();
                    Console.WriteLine("Tables created successfully.");
                    //await databaseService.InsertTestDataAsync();
                    //Console.WriteLine("Test data inserted successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database initialization failed: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Database already created.");
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoadingPage());
        }
    }
}

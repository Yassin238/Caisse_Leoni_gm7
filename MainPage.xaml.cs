using Caisse_Leoni_gm7.Services;
using System;

namespace Caisse_Leoni_gm7
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            LoadUserCount();
        }

        private async void LoadUserCount()
        {
            try
            {
                await App.InitializeDatabaseAsync();
                var users = await App.DatabaseService.GetUsersAsync();
                Console.WriteLine($"Number of users retrieved: {users.Count}");
                UserCountLabel.Text = $"Number of users: {users.Count}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load user count: {ex.Message}");
                UserCountLabel.Text = "Failed to load user count";
            }
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}

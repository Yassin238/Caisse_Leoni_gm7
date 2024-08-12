using System;
using System.Linq;
using Caisse_Leoni_gm7.Views;
using Caisse_Leoni_gm7.Services;

namespace Caisse_Leoni_gm7
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
        }


        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both username and password", "OK");
                return;
            }

            var users = await App.DatabaseService.GetUsersAsync();
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                Console.WriteLine($"User {username} logged in successfully.");
                await DisplayAlert("Success", "Login successful", "OK");

                if (user.IsAdmin)
                {
                    if (user.IsFirstLogin)
                    {
                        await Navigation.PushAsync(new FirstLogin(App.DatabaseService));
                    }
                    else
                    {
                        await Navigation.PushAsync(new AdminView());
                    }
                }
                else
                {
                    if (user.IsFirstLogin)
                    {
                        await Navigation.PushAsync(new FirstLogin(App.DatabaseService));
                    }
                    else
                    {
                        await Navigation.PushAsync(new UserView());
                    }
                }
            }
            else
            {
                Console.WriteLine($"Login failed for user {username}.");
                await DisplayAlert("Error", "Invalid username or password", "OK");
            }
        }
    }
}

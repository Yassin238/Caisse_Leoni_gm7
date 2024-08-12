using Caisse_Leoni_gm7.Models;
using Microsoft.Maui.Controls;
using System;

namespace Caisse_Leoni_gm7.Views
{
    public partial class AddUserPage : ContentPage
    {
        public AddUserPage()
        {
            InitializeComponent();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = UsernameEntry.Text,
                PhoneNumber = PhoneNumberEntry.Text,
                Password = PasswordEntry.Text,
                IsAdmin = IsAdminSwitch.IsToggled,
                IsFirstLogin = true
            };

            await App.DatabaseService.SaveUserAsync(user);

       

            await Navigation.PopAsync();
        }
    }
}

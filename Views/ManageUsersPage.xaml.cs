using Caisse_Leoni_gm7.Models;
using Microsoft.Maui.Controls;
using System;

namespace Caisse_Leoni_gm7.Views
{
    public partial class ManageUsersPage : ContentPage
    {
        private User _selectedUser;

        public ManageUsersPage()
        {
            InitializeComponent();
        }

        private async void LoadUsers()
        {
            var users = await App.DatabaseService.GetUsersAsync();
            UsersListView.ItemsSource = users;
        }

        private async void OnAddUserClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddUserPage());
        }

        private void OnUserSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _selectedUser = e.SelectedItem as User;
        }

        private async void OnDeleteUserClicked(object sender, EventArgs e)
        {
            if (_selectedUser != null)
            {
                bool confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {_selectedUser.Username}?", "Yes", "No");
                if (confirm)
                {
                    await App.DatabaseService.DeleteUserAsync(_selectedUser);
                    LoadUsers();
                }
            }
            else
            {
                await DisplayAlert("Error", "No user selected", "OK");
            }
        }

        private async void OnEditProfileClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditProfile());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadUsers();
        }
    }
}

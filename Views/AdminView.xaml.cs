using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Caisse_Leoni_gm7.Models;
using Caisse_Leoni_gm7.Services;

namespace Caisse_Leoni_gm7.Views
{
    public partial class AdminView : ContentPage
    {
        private List<Depense> _depenses;
        private List<Alimentation> _alimentations;

        public AdminView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }

        private void OnMenuButtonClicked(object sender, EventArgs e)
        {
            MenuStack.IsVisible = !MenuStack.IsVisible;
        }

        private async void OnAddAlimentationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAlimentationPage());
        }

        private async void OnAddDepenseClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddDepensePage());
        }

        private async void OnManageUserClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageUsersPage());
        }

        private async Task LoadData()
        {
            _depenses = await App.DatabaseService.GetDepensesAsync();
            _alimentations = await App.DatabaseService.GetAlimentationsAsync();

            DepenseListView.ItemsSource = _depenses.OrderByDescending(d => d.Date).ToList();
            AlimentationListView.ItemsSource = _alimentations.OrderByDescending(a => a.Date).ToList();
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = e.NewTextValue.ToLower();

            DepenseListView.ItemsSource = _depenses
                .Where(d => d.Motif.ToLower().Contains(filter) || d.Id.ToString().Contains(filter))
                .OrderByDescending(d => d.Date)
                .ToList();

            AlimentationListView.ItemsSource = _alimentations
                .Where(a => a.Nom.ToLower().Contains(filter) || a.Id.ToString().Contains(filter))
                .OrderByDescending(a => a.Date)
                .ToList();
        }

        private async void OnDepenseSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedDepense = (Depense)e.SelectedItem;
            await DisplayAlert("Depense Details", $"Motif: {selectedDepense.Motif}\nMontant: {selectedDepense.Montant}\nDate: {selectedDepense.Date}", "OK");
            ((ListView)sender).SelectedItem = null;
        }

        private async void OnAlimentationSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedAlimentation = (Alimentation)e.SelectedItem;
            await DisplayAlert("Alimentation Details", $"Nom: {selectedAlimentation.Nom}\nDescription: {selectedAlimentation.Description}\nMontant: {selectedAlimentation.Montant}\nDate: {selectedAlimentation.Date}", "OK");
            ((ListView)sender).SelectedItem = null;
        }
    }
}

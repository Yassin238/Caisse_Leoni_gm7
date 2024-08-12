using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caisse_Leoni_gm7.Models;
using Microsoft.Maui.Controls;

namespace Caisse_Leoni_gm7.Views
{
    public partial class UserView : ContentPage
    {
        private List<Depense> _depenses;
        private List<Alimentation> _alimentations;

        public UserView()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
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
            await DisplayAlert("Depense Details", $"Motif: {selectedDepense.Motif}", "OK");
            ((ListView)sender).SelectedItem = null;
        }

        private async void OnAlimentationSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedAlimentation = (Alimentation)e.SelectedItem;
            await DisplayAlert("Alimentation Details", $"Nom: {selectedAlimentation.Nom}\nDescription: {selectedAlimentation.Description}", "OK");
            ((ListView)sender).SelectedItem = null;
        }
    }
}

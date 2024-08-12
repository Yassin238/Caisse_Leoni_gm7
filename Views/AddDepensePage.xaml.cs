using Caisse_Leoni_gm7.Models;
using System;

namespace Caisse_Leoni_gm7.Views
{
    public partial class AddDepensePage : ContentPage
    {
        public AddDepensePage()
        {
            InitializeComponent();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var depense = new Depense
            {
                Motif = MotifEntry.Text,
                Montant = double.Parse(MontantEntry.Text),
                Date = DatePicker.Date
            };

            await App.DatabaseService.SaveDepenseAsync(depense);
            await DisplayAlert("Success", "Depense saved", "OK");
            await Navigation.PopAsync();
        }
    }
}

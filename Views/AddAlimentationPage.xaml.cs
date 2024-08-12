using Caisse_Leoni_gm7.Models;
using System;

namespace Caisse_Leoni_gm7.Views
{
    public partial class AddAlimentationPage : ContentPage
    {
        public AddAlimentationPage()
        {
            InitializeComponent();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var alimentation = new Alimentation
            {
                Nom = NomEntry.Text,
                Description = DescriptionEntry.Text,
                Montant = double.Parse(AmountEntry.Text),
                Date = DatePicker.Date
            };

            await App.DatabaseService.SaveAlimentationAsync(alimentation);
            await DisplayAlert("Success", "Alimentation saved", "OK");
            await Navigation.PopAsync();
        }
    }
}

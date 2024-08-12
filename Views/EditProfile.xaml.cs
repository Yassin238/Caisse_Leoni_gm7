using Caisse_Leoni_gm7.Data;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Caisse_Leoni_gm7.Views
{
    public partial class EditProfile : ContentPage
    {
        public EditProfile()
        {
            InitializeComponent();
            InitializeProfileDataAsync();
        }

        private async Task InitializeProfileDataAsync()
        {
            try
            {
                InitializeGouvernoratPickers();
            }
            catch (Exception ex)
            {
                // Handle exceptions or log them
                Console.WriteLine($"Error initializing gouvernorat pickers: {ex.Message}");
                await DisplayAlert("Error", "Failed to load data for pickers. Please try again.", "OK");
            }
        }

        private void InitializeGouvernoratPickers()
        {
            GouvernoratResidencePicker.ItemsSource = TunisiaData.DelegationsByGouvernorat.Keys.ToList();
            GouvernoratParentsPicker.ItemsSource = TunisiaData.DelegationsByGouvernorat.Keys.ToList();
        }

        private void OnGouvernoratResidenceSelected(object sender, EventArgs e)
        {
            string selectedGouvernorat = GouvernoratResidencePicker.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedGouvernorat) && TunisiaData.DelegationsByGouvernorat.ContainsKey(selectedGouvernorat))
            {
                DelegationResidencePicker.ItemsSource = TunisiaData.DelegationsByGouvernorat[selectedGouvernorat];
                DelegationResidencePicker.SelectedIndex = -1;
                MunicipaliteResidencePicker.ItemsSource = null;
            }
        }

        private void OnDelegationResidenceSelected(object sender, EventArgs e)
        {
            string selectedDelegation = DelegationResidencePicker.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedDelegation) && TunisiaData.MunicipalitesByDelegation.ContainsKey(selectedDelegation))
            {
                MunicipaliteResidencePicker.ItemsSource = TunisiaData.MunicipalitesByDelegation[selectedDelegation];
                MunicipaliteResidencePicker.SelectedIndex = -1;
            }
        }

        private void OnGouvernoratParentsSelected(object sender, EventArgs e)
        {
            string selectedGouvernorat = GouvernoratParentsPicker.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedGouvernorat) && TunisiaData.DelegationsByGouvernorat.ContainsKey(selectedGouvernorat))
            {
                DelegationParentsPicker.ItemsSource = TunisiaData.DelegationsByGouvernorat[selectedGouvernorat];
                DelegationParentsPicker.SelectedIndex = -1;
                MunicipaliteParentsPicker.ItemsSource = null;
            }
        }

        private void OnDelegationParentsSelected(object sender, EventArgs e)
        {
            string selectedDelegation = DelegationParentsPicker.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedDelegation) && TunisiaData.MunicipalitesByDelegation.ContainsKey(selectedDelegation))
            {
                MunicipaliteParentsPicker.ItemsSource = TunisiaData.MunicipalitesByDelegation[selectedDelegation];
                MunicipaliteParentsPicker.SelectedIndex = -1;
            }
        }

        private async void OnChangeImageClicked(object sender, EventArgs e)
        {
            // Implement logic to change the profile image
        }

        private async void OnUpdateClicked(object sender, EventArgs e)
        {
            // Implement logic to update the user profile
            // Validate inputs and save changes
        }
    }
}

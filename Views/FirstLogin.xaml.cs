using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Caisse_Leoni_gm7.Models;
using Caisse_Leoni_gm7.Services;

namespace Caisse_Leoni_gm7.Views
{
    public partial class FirstLogin : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private static readonly Dictionary<string, string> VerificationCodes = new();

        public FirstLogin(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void OnSendVerificationCodeClicked(object sender, EventArgs e)
        {
            string phoneNumber = PhoneNumberEntry.Text;

            if (string.IsNullOrEmpty(phoneNumber))
            {
                await DisplayAlert("Error", "Please enter your phone number.", "OK");
                return;
            }

            // Check if the phone number matches the user's phone number in the database
            bool isPhoneNumberValid = await CheckPhoneNumberAsync(phoneNumber);

            if (isPhoneNumberValid)
            {
                // Send the verification code via SMS using an API
                await SendVerificationCodeAsync(phoneNumber);
            }
            else
            {
                await DisplayAlert("Error", "Phone number does not match.", "OK");
            }
        }

        private async void OnVerifyClicked(object sender, EventArgs e)
        {
            string verificationCode = VerificationCodeEntry.Text;

            if (string.IsNullOrEmpty(verificationCode))
            {
                await DisplayAlert("Error", "Please enter the verification code.", "OK");
                return;
            }

            // Verify the code
            bool isCodeValid = await VerifyCodeAsync(verificationCode);

            if (isCodeValid)
            {
                await DisplayAlert("Success", "Verification successful.", "OK");
                // Navigate to the next page
                await Navigation.PushAsync(new UserView());
            }
            else
            {
                await DisplayAlert("Error", "Invalid verification code.", "OK");
            }
        }

        public async Task<bool> CheckPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                var users = await _databaseService.GetUsersAsync();
                var user = users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
                return user != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking phone number: {ex.Message}");
                return false;
            }
        }

        private Task SendVerificationCodeAsync(string phoneNumber)
        {
            var smsService = new SmsService();
            string verificationCode = GenerateVerificationCode();
            string message = $"Your verification code is: {verificationCode}";

            smsService.SendSms(phoneNumber, message);

            SaveVerificationCode(phoneNumber, verificationCode);

            return Task.CompletedTask;
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Generates a 6-digit code
        }

        private void SaveVerificationCode(string phoneNumber, string code)
        {
            // Store the verification code in memory
            if (VerificationCodes.ContainsKey(phoneNumber))
            {
                VerificationCodes[phoneNumber] = code;
            }
            else
            {
                VerificationCodes.Add(phoneNumber, code);
            }
        }

        private Task<bool> VerifyCodeAsync(string verificationCode)
        {
            string phoneNumber = PhoneNumberEntry.Text; // Assume this is the current user's phone number
            string storedCode = GetStoredVerificationCode(phoneNumber);

            bool isCodeValid = storedCode == verificationCode;

            // Optionally, you can remove the code after verification to prevent reuse
            if (isCodeValid)
            {
                VerificationCodes.Remove(phoneNumber);
            }

            return Task.FromResult(isCodeValid);
        }

        private string GetStoredVerificationCode(string phoneNumber)
        {
            // Retrieve the stored code for the given phone number
            return VerificationCodes.TryGetValue(phoneNumber, out var code) ? code : null;
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConferenceManager.Client.Services;
using System.Collections.ObjectModel;
using ConferenceManager.Core.Entities;

namespace ConferenceManager.Client.ViewModels
{
    // Partial клас потрібен для роботи CommunityToolkit (генерація кодів)
    public partial class MainViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private readonly Auth0Client _auth0Client;

        // Ця властивість автоматично створить IsBusy (з великої літери) для XAML
        [ObservableProperty]
        private bool isBusy; 

        public ObservableCollection<Conference> Conferences { get; } = new();

        public MainViewModel(ApiService apiService, Auth0Client auth0Client)
        {
            _apiService = apiService;
            _auth0Client = auth0Client;
        }

        [RelayCommand]
        public async Task LoginAsync()
        {
            if (IsBusy) return;

            IsBusy = true; // Пункт 3: Вмикаємо анімацію
            try
            {
                // Тут буде виклик методу логіну з вашого Auth0Client
                await Task.Delay(2000); // Тестова затримка, щоб побачити анімацію
                
                // Після логіну можна завантажити дані
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false; // Пункт 3: Вимикаємо анімацію
            }
        }

        private async Task LoadDataAsync()
        {
            var data = await _apiService.GetConferencesAsync();
            Conferences.Clear();
            foreach (var conf in data)
            {
                Conferences.Add(conf);
            }
        }
    }
}
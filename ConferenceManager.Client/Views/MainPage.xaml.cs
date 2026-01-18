using ConferenceManager.Client.ViewModels;

namespace ConferenceManager.Client.Views;

public partial class MainPage : ContentPage
{
    // Ми видалили лічильник count, бо логіка тепер буде у ViewModel
    
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        
        // Зв'язуємо сторінку з логікою (ViewModel)
        BindingContext = viewModel;
    }
}
using weather_app.Services;
using weather_app.ViewModels;

namespace weather_app.Views;

public partial class HomePage : ContentPage
{	
    public HomePage(IWeatherDataService weatherService)
    {
        InitializeComponent();
        BindingContext = new HomePageViewModel(weatherService);
    }
}
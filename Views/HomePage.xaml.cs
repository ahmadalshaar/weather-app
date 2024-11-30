using weather_app.Services;
using weather_app.ViewModels;

namespace weather_app.Views;

public partial class HomePage : ContentPage
{	
    public HomePage(IWeatherDataService weatherService,ILocationService locationService, IConnectivityService connectivityService, IToastMessageService toastMessageService)
    {
        InitializeComponent();
        BindingContext = new HomePageViewModel(weatherService, locationService, connectivityService,toastMessageService);
    }
}
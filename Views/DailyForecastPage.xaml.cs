using weather_app.Services;
using weather_app.ViewModels;

namespace weather_app.Views;

public partial class DailyForecastPage : ContentPage
{
	public DailyForecastPage(IConnectivityService connectivityService, IToastMessageService toastMessageService)
	{
		InitializeComponent();
		BindingContext = new DailyForecastPageViewModle(connectivityService, toastMessageService);
    }
}
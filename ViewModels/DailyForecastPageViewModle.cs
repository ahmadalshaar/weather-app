using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weather_app.Model;
using weather_app.Services;
using weather_app.ViewModels.Base;

namespace weather_app.ViewModels
{

    [QueryProperty(nameof(Forecast), nameof(Forecast))]
    public partial class DailyForecastPageViewModle : ViewModelBase
    {
        [ObservableProperty]
        private Forecast forecast;

        public DailyForecastPageViewModle(IConnectivityService connectivityService, IToastMessageService toastMessageService) : base(connectivityService, toastMessageService)
        {
        }

        [RelayCommand]
        private async Task GoBack()
        {
            var c = forecast;
            await Shell.Current.GoToAsync("..", true);
        }

    }
}
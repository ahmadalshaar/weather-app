using CommunityToolkit.Mvvm.ComponentModel;
using System.Runtime.InteropServices;
using weather_app.Services;

namespace weather_app.ViewModels.Base
{
    public partial class ViewModelBase : ObservableObject
    {
        private readonly IConnectivityService _connectivityService;
        private readonly IToastMessageService _toastMessageService;
        [ObservableProperty]
        private bool connectionStatus;
   

        public ViewModelBase(IConnectivityService connectivityService, IToastMessageService toastMessageService)
        {
            _toastMessageService = toastMessageService;
            _connectivityService = connectivityService;
            
            // Set the initial connection status
            UpdateConnectionStatus();
          
            // Subscribe to connectivity changes
            _connectivityService.ConnectivityChanged += (s, e) => UpdateConnectionStatus();
        }
     
        private void UpdateConnectionStatus()
        {
            ConnectionStatus =  _connectivityService.IsConnected;
            if(_toastMessageService!= null)
                _toastMessageService.ErrorToast(_connectivityService.IsConnected ? "Connected to the Internet" : "No Internet Connection");
        }    
    }
}

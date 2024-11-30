using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weather_app.Services
{
    public class ConnectivityService : IConnectivityService
    {
        public event EventHandler ConnectivityChanged;

        public ConnectivityService()
        {            
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        public bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            ConnectivityChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

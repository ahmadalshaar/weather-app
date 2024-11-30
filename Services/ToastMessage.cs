using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weather_app.Services
{
    public class ToastMessage : IToastMessageService   
    {      
        public void ErrorToast(string message)
        {
            Toast.Make(message, ToastDuration.Long, 16).Show();
        }
    }
}

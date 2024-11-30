using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weather_app.Services
{
    public interface IToastMessageService
    {
        void ErrorToast(string message);        
    }
}

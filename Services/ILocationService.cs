using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weather_app.Model;

namespace weather_app.Services
{
    public interface ILocationService
    {
        Task<Position> GetCurrentLocationAsync();        
    }
}

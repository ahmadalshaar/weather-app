using Microsoft.Maui.ApplicationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weather_app.Model;

namespace weather_app.Services
{
    public class LocationService : ILocationService
    {
        public event EventHandler<Position> LocationChanged;

        public async Task<Position> GetCurrentLocationAsync()
        {
            try
            {
                var permissionStatus = await CheckAndRequestLocationPermission();

                if (permissionStatus != PermissionStatus.Granted)
                    return null;

                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    return new Position { Latitude= location.Latitude,Longitude = location.Longitude };
                }

                // If no last known location, request current location
                location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
                return new Position { Latitude = location.Latitude, Longitude = location.Longitude };
            }
            catch (Exception ex)
            {       
                return null;
            }
        }


        private async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
                return status;

            if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
            {
                // Optionally show rationale to the user
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return status;
        }
    }
}

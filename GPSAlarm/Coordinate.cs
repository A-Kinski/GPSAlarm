using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace GPSAlarm
{
    class Coordinate
    {
        //TODO заменить IDIctionary на имзеняемую коллекцию
        IDictionary coordinate = new Dictionary<string, string>();
        Geolocator location = null;

        public Coordinate()
        {
            location = new Geolocator();
            location.MovementThreshold = 5;
            location.PositionChanged += Location_PositionChanged;

            coordinate.Add("latitude", "No data");
            coordinate.Add("longitude", "No data");

            //getLock();
            
        }

        private void Location_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            getLock();
        }

        async private void getLock()
        {
            try
            {
                Geoposition pos = await location.GetGeopositionAsync();//.ConfigureAwait(false);

                coordinate["latitude"] = pos.Coordinate.Point.Position.Latitude.ToString();
                //coordinate.Add("latitude", pos.Coordinate.Point.Position.Latitude.ToString());
                coordinate["longitude"] = pos.Coordinate.Point.Position.Longitude.ToString();
                //coordinate.Add("longitude", pos.Coordinate.Point.Position.Longitude.ToString());
            }
            catch (System.UnauthorizedAccessException)
            {
                coordinate.Add("latitude", "No data");
                coordinate.Add("longitude", "No data");
            }
            catch (TaskCanceledException)
            {
                coordinate.Add("latitude", "Canceled");
                coordinate.Add("longitude", "Canceled");
            }
        }

    }
}

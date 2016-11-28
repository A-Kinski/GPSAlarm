using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace GPSAlarm
{
    class Coordinate
    {
        //Коллекция с координатами - 0 - latitude широта, 1 - longitude долгота
        private ObservableCollection<Double> currentCoordinate = new ObservableCollection<Double>() { 1, 0.8};
        private ObservableCollection<Double> destinationCoordinate = new ObservableCollection<double>() { 0, 0 };
        private Geolocator location = null;
        private Int32 R = 6371210; //радиус Земли
        private Int16 alarmDistance = 500;

        public Coordinate()
        {
            location = new Geolocator();
            location.MovementThreshold = 5;
            location.PositionChanged += Location_PositionChanged;
        }

        private void Location_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            GetLock();

            //проверка на попадание в сферу
            checkCoordinate();
        }

        private void checkCoordinate()
        {
            //найдем расстояние между двумя точками на сфере
            Double distance = Math.Round(R * Math.Acos(Math.Sin(currentCoordinate[0]) * Math.Sin(destinationCoordinate[0]) + 
                Math.Cos(currentCoordinate[0]) * Math.Cos(destinationCoordinate[0]) * Math.Cos(destinationCoordinate[1] - currentCoordinate[1])));

            //проверка - если дистанция меньше определенного расстояния - делаем alarm
            if (distance < alarmDistance) alarmLetsGo();

        }

        private void alarmLetsGo()
        {
            string str = "";
            
        }

        public void setDestinationCoordinate(Double latitude, Double longitude)
        {
            //будем сохранять координаты в радианах - чтобы сразу использовать для расчета расстояния на сфере
            destinationCoordinate[0] = degreToRadian(latitude);
            destinationCoordinate[1] = degreToRadian(longitude);
        }

        private double degreToRadian(double coordinate)
        {
            return (coordinate * Math.PI) / 180;
        }

        async private void GetLock()
        {
            try
            {
                Geoposition pos = await location.GetGeopositionAsync();//.ConfigureAwait(false);

                currentCoordinate[0] = degreToRadian(pos.Coordinate.Point.Position.Latitude);
                //coordinate.Add("latitude", pos.Coordinate.Point.Position.Latitude.ToString());
                currentCoordinate[1] = degreToRadian(pos.Coordinate.Point.Position.Longitude);
                //coordinate.Add("longitude", pos.Coordinate.Point.Position.Longitude.ToString());
            }
            catch (System.UnauthorizedAccessException)
            {
                currentCoordinate[0] =  0;
                currentCoordinate[1] =  0;
            }
            catch (TaskCanceledException)
            {
                currentCoordinate[0] = 0;
                currentCoordinate[1] = 0;
            }
        }

        public List<Double> GetCoordinate()
        {
           return currentCoordinate.ToList();
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.Storage;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=391641

namespace GPSAlarm
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Coordinate coord;

        public MainPage()
        {
            this.InitializeComponent();
            
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           coord = new Coordinate();

           mapSeetings(coord.GetCoordinate());
        }

        private async void mapSeetings(List<Double> geoCoordinate)
        {
            //TODO сделать настроку карты в соответсвии с моей текущей позицией - как в примере на msdn
            Geolocator geolocator = new Geolocator();
            Geoposition pos = await geolocator.GetGeopositionAsync();
            Geopoint currentLocation = pos.Coordinate.Point;


            //BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = geoCoordinate[0], Longitude = geoCoordinate[1] };
            //Geopoint cityCenter = new Geopoint(cityPosition);

            mainMap.Center = currentLocation;
            mainMap.ZoomLevel = 12;
            mainMap.LandmarksVisible = true;
            mainMap.Heading = 0;
            mainMap.DesiredPitch = 0;
            mainMap.Style = MapStyle.AerialWithRoads;
            mainMap.ColorScheme = MapColorScheme.Light;
        }



        private void mainMap_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void mainMap_MapHolding(MapControl sender, MapInputEventArgs args)
        {

        }

        private void mainMap_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            BasicGeoposition tappedGeoposition = args.Location.Position;

            //сохраним полученные кординаты в в приложении
            ApplicationDataContainer destinationCoordinate = ApplicationData.Current.LocalSettings;
            destinationCoordinate.Values["destinationLatitude"] = tappedGeoposition.Latitude;
            destinationCoordinate.Values["destinationLongitude"] = tappedGeoposition.Longitude;

            coord.setDestinationCoordinate(tappedGeoposition.Latitude, tappedGeoposition.Longitude);
        }
    }
}

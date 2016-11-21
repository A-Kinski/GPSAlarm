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

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=391641

namespace GPSAlarm
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
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
           // Coordinate coord = new Coordinate();

            mapSeetings();
        }

        private void mapSeetings()
        {
            //mainMap.ZoomInteractionMode = MapInteractionMode.GestureAndControl;
            //mainMap.TiltInteractionMode = MapInteractionMode.GestureAndControl;

            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 47.604, Longitude = -122.32 };
            Geopoint cityCenter = new Geopoint(cityPosition);

            mainMap.Center = cityCenter;
            mainMap.ZoomLevel = 12;
            mainMap.LandmarksVisible = true;
            mainMap.Heading = 0;
            mainMap.DesiredPitch = 0;
            mainMap.Style = MapStyle.AerialWithRoads;
            mainMap.ColorScheme = MapColorScheme.Light;
        }

  

        private void mainMap_Loaded(object sender, RoutedEventArgs e)
        {
            string str = "";
        }
    }
}

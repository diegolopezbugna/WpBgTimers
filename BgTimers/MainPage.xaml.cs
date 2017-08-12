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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace BgTimers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // VERY ULGY!!!!

        public static List<string> COLORES = new List<string>() { "rojo", "azul", "verde", "amarillo", "blanco", "negro", "naranja", "violeta", "marrón" };

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            combo1.ItemsSource = COLORES;
            combo2.ItemsSource = COLORES;
            combo3.ItemsSource = COLORES;
            combo4.ItemsSource = COLORES;
            combo5.ItemsSource = COLORES;
            combo6.ItemsSource = COLORES;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var args = string.Join(",", combo1.SelectedValue, combo2.SelectedValue, combo3.SelectedValue, 
                combo4.SelectedValue, combo5.SelectedValue, combo6.SelectedValue);
            Frame.Navigate(typeof(Timers6), args);
        }
    }
}

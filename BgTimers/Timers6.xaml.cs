using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace BgTimers
{
    public class TimerUI
    {
        // VERY ULGY!!!!
        public Grid Grid;
        public TextBlock txtCur;
        public TextBlock txtTot;
        public DateTime CurrentTimeStart;
        public TimeSpan Total = new TimeSpan();
        public bool IsRunning = false;
    }
        

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Timers6 : Page
    {
        private List<TimerUI> timers = new List<TimerUI>();
        private Timer threadingTimer;
        private string[] colores;

        public Timers6()
        {
            this.InitializeComponent();

            // VERY ULGY!!!!
            timers.Add(new TimerUI());
            timers.Add(new TimerUI());
            timers.Add(new TimerUI());
            timers.Add(new TimerUI());
            timers.Add(new TimerUI());
            timers.Add(new TimerUI());

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var coloresStr = e.Parameter.ToString();
            colores = coloresStr.Split(',');

            SetupGrid(grid1, 0, txtCur1, txtTot1);
            SetupGrid(grid2, 1, txtCur2, txtTot2);
            SetupGrid(grid3, 2, txtCur3, txtTot3);
            SetupGrid(grid4, 3, txtCur4, txtTot4);
            SetupGrid(grid5, 4, txtCur5, txtTot5);
            SetupGrid(grid6, 5, txtCur6, txtTot6);
        }

        private void SetupGrid(Grid grid, int index, TextBlock txtCur, TextBlock txtTot)
        {
            grid.Tag = index;
            string color = colores[index];
            timers[index].Grid = grid;
            timers[index].txtCur = txtCur;
            timers[index].txtTot = txtTot;

            if (string.IsNullOrEmpty(color))
            {
                grid.IsTapEnabled = false;
                grid.Visibility = Visibility.Collapsed;
                return;
            }

            grid.Background = GetBgBrush(color);
            if (color == "blanco" || color == "amarillo")
            {
                txtCur.Foreground = new SolidColorBrush(Colors.Black);
                txtTot.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private Brush GetBgBrush(string color)
        {
            // VERY ULGY!!!!
            if (color == "rojo")
                return new SolidColorBrush(Colors.Red);
            if (color == "azul")
                return new SolidColorBrush(Colors.Blue);
            if (color == "verde")
                return new SolidColorBrush(Colors.Green);
            if (color == "amarillo")
                return new SolidColorBrush(Colors.Yellow);
            if (color == "blanco")
                return new SolidColorBrush(Colors.White);
            if (color == "negro")
                return new SolidColorBrush(Colors.Black);
            if (color == "naranja")
                return new SolidColorBrush(Colors.Orange);
            if (color == "marrón")
                return new SolidColorBrush(Colors.Brown);
            if (color == "violeta")
                return new SolidColorBrush(Colors.Violet);
            return null;
        }

        private void Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Grid grid = sender as Grid;
            int index = (int)grid.Tag;

            bool shouldStart = !timers[index].IsRunning;

            foreach (var t in timers)
                TimerStop(t);

            if (shouldStart)
                TimerStart(timers[index]);
        }

        private void TimerStart(TimerUI timer)
        {
            if (threadingTimer == null)
            {
                timer.CurrentTimeStart = DateTime.Now;
                timer.IsRunning = true;
                threadingTimer = new Timer(TimerCallback, null, 0, 83);
            }
        }

        private void TimerStop(TimerUI timer)
        {
            if (threadingTimer != null)
            {
                threadingTimer.Dispose();
                threadingTimer = null;
            }
            if (timer.IsRunning)
            {
                timer.IsRunning = false;
                timer.Total += (DateTime.Now - timer.CurrentTimeStart);
                timer.txtTot.Text = timer.Total.ToString(@"mm\:ss");
            }
        }

        private void TimerCallback(Object stateInfo)
        {
            for (int i = 0; i < timers.Count; i++)
            {
                var t = timers[i];
                if (t.IsRunning)
                {
                    Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
    () =>
    {
        var elapsed = DateTime.Now - t.CurrentTimeStart;
        t.txtCur.Text = elapsed.ToString(@"mm\:ss\.fff");
        t.txtTot.Text = (t.Total + elapsed).ToString(@"mm\:ss");
    });
                 }
            }
        }

    }
}

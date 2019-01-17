using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StopWatchWithInterface
{
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        Stopwatch lapTimeStopWatch = new Stopwatch();
        string currentTime = string.Empty;
        string currentTimeLap = string.Empty;
        string lapTime = string.Empty;
        int lapTimeCounter = 1;

        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        // Innitiating timespan
        void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                StopwatchTime.Text = currentTime;
            }

            if (lapTimeStopWatch.IsRunning)
            {
                TimeSpan tsl = lapTimeStopWatch.Elapsed;
                currentTimeLap = String.Format("{0:00}:{1:00}:{2:00}",
                tsl.Minutes, tsl.Seconds, tsl.Milliseconds / 10);
                StopwatchLapTime.Text = currentTimeLap;
            }
        }

        // Start button
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            stopWatch.Start();
            dispatcherTimer.Start();
            lapTimeStopWatch.Start();
        }

        // Stop button
        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
            }

            if (lapTimeStopWatch.IsRunning)
            {
                lapTimeStopWatch.Stop();
            }
        }

        // Reset button
        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            if (!stopWatch.IsRunning)
            {
                stopWatch.Reset();
                StopwatchTime.Text = "00:00:00";

                lapTimeStopWatch.Reset();
                StopwatchLapTime.Text = "00:00:00";

                TotalTimeBox.Text = string.Empty;
                LapTimeBoxLap.Text = string.Empty;
                lapTimeCounter = 1;
            }
           
        }

        // Lap time button
        private void ButtonLapTime_Click(object sender, RoutedEventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                // Writes lap time to lap time box
                LapTimeBoxLap.Text = LapTimeBoxLap.Text.Insert(0, $"Laptime {lapTimeCounter}:\t" + StopwatchLapTime.Text + "\n");

                // Restarts laptime counter when Lap button is pressed
                lapTimeStopWatch.Restart();
                StopwatchLapTime.Text = "00:00:00";

                // Writes total time to overall timebox
                TotalTimeBox.Text = TotalTimeBox.Text.Insert(0, $"Laptime {lapTimeCounter}:\t" + StopwatchTime.Text + "\n");

                // Adds +1 lap to for the lap counter
                lapTimeCounter++;
            }
        }
    }
}

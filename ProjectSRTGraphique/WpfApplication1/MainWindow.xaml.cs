using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private EncodeSub Subs;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick +=new EventHandler(timer_Tick);
               Subs = new EncodeSub(textBox2);
    }

        private void timer_Tick(object sender, EventArgs e)
        {
            slider_seek.Value = mediaElement.Position.TotalSeconds;
        }

        private void Play_OnClick(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
           // Subs.ReadStr().Wait();
        }

        private void Pause_OnClick(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void Stop_OnClick(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }

        private void Slider_seek_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Position = TimeSpan.FromSeconds(slider_seek.Value);
        }


        private void Slider_volume_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = (double) slider_volume.Value;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string filename = (string)((DataObject)e.Data).GetFileDropList()[0];
            mediaElement.Source = new Uri(filename);

            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.UnloadedBehavior = MediaState.Manual;
            mediaElement.Volume = (double) slider_volume.Value;
            mediaElement.Play();
          //Subs.PrintSub(Subs.ListOfStrs).Wait();
        }

        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = mediaElement.NaturalDuration.TimeSpan;
            slider_seek.Maximum = ts.TotalSeconds;
            timer.Start();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MyWindowsMediaPlayer.View
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _fullscreen = false;
        private DispatcherTimer _doubleClickTimer = new DispatcherTimer();
        private GridLength[] _columnsWidth = new GridLength[3];

        public MainWindow()
        {
            InitializeComponent();
            _doubleClickTimer.Interval = TimeSpan.FromMilliseconds(GetDoubleClickTime());
            _doubleClickTimer.Tick += (s, e) => _doubleClickTimer.Stop();
        }

        private void MediaPlayer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_doubleClickTimer.IsEnabled)
                _doubleClickTimer.Start();
            else
            {
                if (!_fullscreen)
                {
                    this.WindowStyle = WindowStyle.None;
                    this.WindowState = WindowState.Maximized;

                    this.ContentSplitter.Visibility = Visibility.Collapsed;
                    this.LeftContentFrame.Visibility = Visibility.Collapsed;
                    _columnsWidth[0] = this.Content.ColumnDefinitions[0].Width;
                    _columnsWidth[1] = this.Content.ColumnDefinitions[1].Width;
                    this.Content.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Auto);
                    this.Content.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Auto);
                }
                else
                {
                    this.WindowStyle = WindowStyle.SingleBorderWindow;
                    this.WindowState = WindowState.Normal;

                    this.Content.ColumnDefinitions[0].Width = _columnsWidth[0];
                    this.Content.ColumnDefinitions[1].Width = _columnsWidth[1];
                    this.ContentSplitter.Visibility = Visibility.Visible;
                    this.LeftContentFrame.Visibility = Visibility.Visible;
                }

                _fullscreen = !_fullscreen;
            }
        }

        [DllImport("user32.dll")]
        private static extern uint GetDoubleClickTime();
    }
}

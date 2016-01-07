﻿using MyWindowsMediaPlayer.Service;
using MyWindowsMediaPlayer.ViewModel;
using System;
using System.Collections.Generic;
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

namespace MyWindowsMediaPlayer.View
{
    /// <summary>
    /// Logique d'interaction pour VideoLibrary.xaml
    /// </summary>
    public partial class VideoLibrary : Page
    {
        public VideoLibrary()
        {
            InitializeComponent();

            this.DataContext = new VideoLibraryVM(new WindowService());
        }
    }
}

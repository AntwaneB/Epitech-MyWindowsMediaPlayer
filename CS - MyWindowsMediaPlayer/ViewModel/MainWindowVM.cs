using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyWindowsMediaPlayer.Model;
using MyWindowsMediaPlayer.Utils;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;
using System.IO;
using System.Windows.Media.Imaging;
using MyWindowsMediaPlayer.Service;

namespace MyWindowsMediaPlayer.ViewModel
{
    class MainWindowVM : ViewModelBase
    {
        #region Attributes
        private NavigationService _navigationService = null;

        private Media _currentMedia = null;
        private Playlist _currentPlaylist = null;
        private MediaElement _mediaElement = null;
        private DispatcherTimer _mediaTimer = new DispatcherTimer();
        private int _volume = 25;

        private DelegateCommand _loadCommand = null;
        private DelegateCommand _playCommand = null;
        private DelegateCommand _pauseCommand = null;
        private DelegateCommand _stopCommand = null;
        private DelegateCommand _navigateLibraryCommand = null;
        private DelegateCommand _navigatePlaylistsCommand = null;
        #endregion

        #region Properties
        public MediaElement MediaElementCtrl
        {
            get { return (_mediaElement); }
        }
        public Media CurrentMedia
        {
            get { return (_currentMedia); }
            set
            {
                _currentMedia = value;
                NotifyPropertyChanged("CurrentMedia");
            }
        }
        public Playlist CurrentPlaylist
        {
            get { return (_currentPlaylist); }
            set
            {
                _currentPlaylist = value;
                OnStop(null);
                CurrentMedia = null;
                NotifyPropertyChanged("CurrentPlaylist");
                _playCommand.RaiseCanExecuteChanged();
            }
        }
        public int Volume
        {
            get { return (_volume); }
            set
            {
                _volume = value;
                if (_mediaElement != null)
                    _mediaElement.Volume = _volume / 100.0;
            }
        }

        public ICommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                    _loadCommand = new DelegateCommand(OnLoad, CanLoad);

                return (_loadCommand);
            }
        }
        public ICommand PlayCommand
        {
            get
            {
                if (_playCommand == null)
                    _playCommand = new DelegateCommand(OnPlay, CanPlay);

                return (_playCommand);
            }
        }
        public ICommand PauseCommand
        {
            get
            {
                if (_pauseCommand == null)
                    _pauseCommand = new DelegateCommand(OnPause, CanPause);

                return (_pauseCommand);
            }
        }
        public ICommand StopCommand
        {
            get
            {
                if (_stopCommand == null)
                    _stopCommand = new DelegateCommand(OnStop, CanStop);

                return (_stopCommand);
            }
        }
        public ICommand NavigateLibraryCommand
        {
            get
            {
                if (_navigateLibraryCommand == null)
                    _navigateLibraryCommand = new DelegateCommand(OnNavigateLibrary, CanNavigateLibrary);

                return (_navigateLibraryCommand);
            }
        }
        public ICommand NavigatePlaylistsCommand
        {
            get
            {
                if (_navigatePlaylistsCommand == null)
                    _navigatePlaylistsCommand = new DelegateCommand(OnNavigatePlaylists, CanNavigatePlaylists);

                return (_navigatePlaylistsCommand);
            }
        }
        #endregion

        #region Delegate Commands
        public void OnLoad(object arg)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            var result = dialog.ShowDialog();

            if (!string.IsNullOrEmpty(dialog.FileName))
            {
                _currentMedia = Media.Factory.make(dialog.FileName);
                if (_currentMedia != null)
                    _currentMedia.State = MediaState.Stop;
                if (_mediaElement != null)
                    _mediaElement.Stop();

                if (_currentPlaylist != null)
                {
                    _currentPlaylist = null;
                    NotifyPropertyChanged("CurrentPlaylist");
                }
                _playCommand.RaiseCanExecuteChanged();
                _pauseCommand.RaiseCanExecuteChanged();
                _stopCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanLoad(object arg)
        {
            return (true);
        }

        public void OnPlay(object arg)
        {
            if (_currentPlaylist != null && _currentMedia == null)
            {
                _currentMedia = _currentPlaylist.Next();
            }

            if (_currentMedia != null)
            {
                _mediaElement.Source = _currentMedia.Path;
                _mediaElement.Volume = Volume / 100.0;
                _mediaElement.Play();
                _currentMedia.State = MediaState.Play;

                _playCommand.RaiseCanExecuteChanged();
                _pauseCommand.RaiseCanExecuteChanged();
                _stopCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanPlay(object arg)
        {
            return ((_currentMedia != null && _currentMedia.State != MediaState.Play)
                    || (_currentPlaylist != null && _currentMedia == null));
        }

        public void OnPause(object arg)
        {
            if (_currentMedia != null)
            {
                _mediaElement.Pause();
                _currentMedia.State = MediaState.Pause;

                _playCommand.RaiseCanExecuteChanged();
                _pauseCommand.RaiseCanExecuteChanged();
                _stopCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanPause(object arg)
        {
            return (_currentMedia != null && _currentMedia.State == MediaState.Play);
        }

        public void OnStop(object arg)
        {
            if (_currentMedia != null)
            {
                _mediaElement.Stop();
                _currentMedia.State = MediaState.Stop;

                _playCommand.RaiseCanExecuteChanged();
                _pauseCommand.RaiseCanExecuteChanged();
                _stopCommand.RaiseCanExecuteChanged();
            }
        }
        public bool CanStop(object arg)
        {
            return (_currentMedia != null && _currentMedia.State != MediaState.Stop);
        }

        public void OnNavigateLibrary(object arg)
        {
            string destination = (string)arg;

            switch (destination)
            {
                case "musics":
                    _navigationService.Navigate(new MusicLibraryVM(new WindowService()));
                    break;
                case "videos":
                    _navigationService.Navigate("View\\Videos.xaml", new PlayerService(this));
                    break;
                case "images":
                    _navigationService.Navigate("View\\Images.xaml", new PlayerService(this));
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("User tried to load an invalid page.");
                    break;
            }
        }

        public bool CanNavigateLibrary(object arg)
        {
            return (true);
        }
        public void OnNavigatePlaylists(object arg)
        {
            _navigationService.Navigate(new PlaylistListVM(new PlayerService(this)));
        }

        public bool CanNavigatePlaylists(object arg)
        {
            return (true);
        }
        #endregion

        public MainWindowVM(NavigationService navigationService)
        {
            _navigationService = navigationService;

            _mediaElement = new MediaElement();
            _mediaElement.LoadedBehavior = MediaState.Manual;
            _mediaElement.UnloadedBehavior = MediaState.Stop;
            _mediaElement.MediaOpened += new RoutedEventHandler(MediaOpened);
            _mediaElement.MediaEnded += new RoutedEventHandler(MediaEnded);

            _mediaTimer.Interval = TimeSpan.FromMilliseconds(100);
            _mediaTimer.Tick += new EventHandler(UpdateMediaPosition);
            _mediaTimer.Start();

            OnNavigatePlaylists(null);
        }

        #region Methods
        private void UpdateMediaPosition(Object sender, EventArgs e)
        {
            if (_currentMedia != null)
            {
                _currentMedia.Position = _mediaElement.Position;
                NotifyPropertyChanged("CurrentMedia");
            }
        }

        private void MediaOpened(object sender, RoutedEventArgs e)
        {
            if (_mediaElement.NaturalDuration.HasTimeSpan)
                _currentMedia.Duration = _mediaElement.NaturalDuration.TimeSpan;
            else
                _currentMedia.Duration = new TimeSpan(0);
        }

        private void MediaEnded(object sender, RoutedEventArgs e)
        {
            if (_currentMedia != null)
                _currentMedia.State = MediaState.Stop;
            if (_mediaElement != null)
                _mediaElement.Stop();

            _playCommand.RaiseCanExecuteChanged();
            _pauseCommand.RaiseCanExecuteChanged();
            _stopCommand.RaiseCanExecuteChanged();

            if (_currentPlaylist != null)
            {
                _currentMedia = null;
                OnPlay(null);
            }
        }
        #endregion
    }
}

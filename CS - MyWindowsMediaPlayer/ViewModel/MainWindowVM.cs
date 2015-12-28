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

namespace MyWindowsMediaPlayer.ViewModel
{
    class MainWindowVM : ViewModelBase
    {
        #region Attributes
        private Media _currentMedia = null;
        private MediaElement _mediaElement = null;
        private DispatcherTimer _mediaTimer = new DispatcherTimer();

        private DelegateCommand _loadCommand = null;
        private DelegateCommand _playCommand = null;
        private DelegateCommand _pauseCommand = null;
        private DelegateCommand _stopCommand = null;
        #endregion

        #region Properties
        public MediaElement MediaElementCtrl
        {
            get { return (_mediaElement); }
        }
        public Media CurrentMedia
        {
            get { return (_currentMedia); }
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
        #endregion

        #region Delegate Commands
        public void OnLoad(object arg)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            var result = dialog.ShowDialog();

            _currentMedia = Media.Factory.make(dialog.FileName);
            if (_currentMedia != null)
                _currentMedia.State = MediaState.Stop;
            if (_mediaElement != null)
                _mediaElement.Stop();

            _playCommand.RaiseCanExecuteChanged();
            _pauseCommand.RaiseCanExecuteChanged();
            _stopCommand.RaiseCanExecuteChanged();
        }

        public bool CanLoad(object arg)
        {
            return (true);
        }

        public void OnPlay(object arg)
        {
            if (_currentMedia != null)
            {
                _mediaElement.Source = _currentMedia.Path;
                _mediaElement.Play();
                _currentMedia.State = MediaState.Play;

                _playCommand.RaiseCanExecuteChanged();
                _pauseCommand.RaiseCanExecuteChanged();
                _stopCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanPlay(object arg)
        {
            return (_currentMedia != null && _currentMedia.State != MediaState.Play);
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
        #endregion

        public MainWindowVM()
        {
            _mediaElement = new MediaElement();
            _mediaElement.LoadedBehavior = MediaState.Manual;
            _mediaElement.UnloadedBehavior = MediaState.Stop;
            _mediaElement.MediaOpened += new RoutedEventHandler(MediaOpened);
            _mediaElement.MediaEnded += new RoutedEventHandler(MediaEnded);

            _mediaTimer.Interval = TimeSpan.FromMilliseconds(100);
            _mediaTimer.Tick += new EventHandler(UpdateMediaPosition);
            _mediaTimer.Start();
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
            _currentMedia.Duration = _mediaElement.NaturalDuration.TimeSpan;
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
        }
        #endregion
    }
}

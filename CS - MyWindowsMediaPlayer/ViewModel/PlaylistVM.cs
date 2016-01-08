using MyWindowsMediaPlayer.Model;
using MyWindowsMediaPlayer.Service;
using MyWindowsMediaPlayer.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace MyWindowsMediaPlayer.ViewModel
{
    class PlaylistVM : ViewModelBase
    {
        #region Attributes
        private Playlist _playlist = null;

        private IPlayerService _playerService = null;
        private ICommand _playPlaylistCommand = null;
        private ICommand _selectMediaCommand = null;
        private ICommand _removeMediaCommand = null;
        #endregion

        #region Properties
        public Playlist Playlist
        {
            get { return (_playlist); }
        }

        public ICollectionView Items
        {
            get
            {
                return (new ListCollectionView(_playlist));
            }
        }

        public ICommand PlayPlaylistCommand
        {
            get
            {
                if (_playPlaylistCommand == null)
                    _playPlaylistCommand = new DelegateCommand(OnPlayPlaylist, CanPlayPlaylist);

                return (_playPlaylistCommand);
            }
        }

        public ICommand SelectMediaCommand
        {
            get
            {
                if (_selectMediaCommand == null)
                    _selectMediaCommand = new DelegateCommand(OnSelectMedia, CanSelectMedia);

                return (_selectMediaCommand);
            }
        }

        public ICommand RemoveMediaCommand
        {
            get
            {
                if (_removeMediaCommand == null)
                    _removeMediaCommand = new DelegateCommand(OnRemoveMedia, CanRemoveMedia);

                return (_removeMediaCommand);
            }
        }
        #endregion

        #region Commands
        public void OnPlayPlaylist(object arg)
        {
            _playerService.SetPlaylist(_playlist);
            _playerService.Play();
        }

        public bool CanPlayPlaylist(object arg)
        {
            return (true);
        }

        public void OnSelectMedia(object arg)
        {
            System.Diagnostics.Debug.WriteLine("Media Name: " + (arg as Media).Name);
            _playerService.SetPlaylist(_playlist);
            _playerService.SetMedia(arg as Media);
            _playerService.Play();
        }

        public bool CanSelectMedia(object arg)
        {
            return (_playlist.Count > 0);
        }

        public void OnRemoveMedia(object arg)
        {
            _playlist.Remove(arg as Media);
            NotifyPropertyChanged("Playlist");
            NotifyPropertyChanged("Items");
        }

        public bool CanRemoveMedia(object arg)
        {
            return (true);
        }
        #endregion

        #region Ctor / Dtor
        public PlaylistVM(Playlist playlist, IPlayerService playerService)
        {
            _playlist = playlist;
            _playerService = playerService;
        }
        #endregion

    }
}

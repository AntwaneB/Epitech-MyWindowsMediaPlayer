using MyWindowsMediaPlayer.Model;
using MyWindowsMediaPlayer.Service;
using MyWindowsMediaPlayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyWindowsMediaPlayer.ViewModel
{
    class PlaylistVM
    {
        #region Attributes
        private Playlist _playlist = null;

        private IPlayerService _playerService = null;
        private ICommand _playPlaylistCommand = null;
        private ICommand _selectMediaCommand = null;
        #endregion

        #region Properties
        public Playlist Playlist
        {
            get { return (_playlist); }
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
            _playerService.SetMedia(arg as Media);
            _playerService.Play();
        }

        public bool CanSelectMedia(object arg)
        {
            return (_playlist.Count > 0);
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

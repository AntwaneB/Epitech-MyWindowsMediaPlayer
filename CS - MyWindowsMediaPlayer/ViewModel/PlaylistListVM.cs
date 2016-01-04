﻿using MyWindowsMediaPlayer.Model;
using MyWindowsMediaPlayer.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Input;

namespace MyWindowsMediaPlayer.ViewModel
{
    class PlaylistListVM : ViewModelBase
    {
        #region Attributes
        private ObservableCollection<Playlist> _playlists = new ObservableCollection<Playlist>();

        private string _newPlaylistName = "";

        private DelegateCommand _createPlaylistCommand = null;
        #endregion

        #region Properties
        public ObservableCollection<Playlist> Playlists
        {
            get { return (_playlists); }
            set { _playlists = value; }
        }

        public string NewPlaylistName
        {
            get { return (_newPlaylistName); }
            set
            {
                _newPlaylistName = value;
                NotifyPropertyChanged("NewPlaylistName");
            }
        }

        public ICommand CreatePlaylistCommand
        {
            get
            {
                if (_createPlaylistCommand == null)
                    _createPlaylistCommand = new DelegateCommand(OnCreatePlaylist, CanCreatePlaylist);

                return (_createPlaylistCommand);
            }
        }
        #endregion

        #region Delegate Commands
        public void OnCreatePlaylist(object arg)
        {
            if (string.IsNullOrEmpty(NewPlaylistName))
                return;

            _playlists.Add(new Playlist(NewPlaylistName));
            NotifyPropertyChanged("Playlists");

            NewPlaylistName = "";
        }

        public bool CanCreatePlaylist(object arg)
        {
            return (true);
        }
        #endregion

        #region Saving
        public void SavePlaylist()
        {

            System.IO.StreamWriter file = new System.IO.StreamWriter("h:\\C_sharp_WMP\\epitech-mywindowsmediaplayer\\Save\\playlist.xml");

            XElement save = new XElement("playlists");
            String media = "";
            String path = "";
            foreach (var item in _playlists)
            {
                XElement playlistName = new XElement("playlist");
                media = item.Name;
                playlistName.SetAttributeValue("name", media);
                foreach (var elem in item)
                {
                    XElement xml = new XElement("media");
                    path = elem.Path.LocalPath;
                    xml.SetAttributeValue("path", path);
                    playlistName.Add(xml);
                    System.Diagnostics.Debug.WriteLine(xml);
                }
                save.Add(playlistName);
            }
            System.Diagnostics.Debug.WriteLine(save);
            file.WriteLine(save);
            file.Close();
        }
        #endregion

        #region Ctor / Dtor
        public PlaylistListVM()
        {
            _playlists.Add(new Playlist("Musique"));
            _playlists.Add(new Playlist("Films"));
            _playlists.Add(new Playlist("Séries"));
            _playlists.Add(new Playlist("Photos de vacance"));

            _playlists[0].Add(new Music(@"E:\Projets\CS - MyWindowsMediaPlayer\Example Medias\Music1.mp3"));
            _playlists[0].Add(new Music(@"E:\Projets\CS - MyWindowsMediaPlayer\Example Medias\MusicInfos1.mp3"));

            _playlists[1].Add(new Video(@"E:\Projets\CS - MyWindowsMediaPlayer\Example Medias\Video2.mp4"));
            _playlists[1].Add(new Video(@"E:\Projets\CS - MyWindowsMediaPlayer\Example Medias\Video1.mp4"));

            _playlists[2].Add(new Video(@"E:\Projets\CS - MyWindowsMediaPlayer\Example Medias\Video3.mp4"));

            // Todo : A virer
            //System.Diagnostics.Debug.WriteLine("**********");
            SavePlaylist();
            //System.Diagnostics.Debug.WriteLine("**********");
            //
        }
        #endregion
    }
}

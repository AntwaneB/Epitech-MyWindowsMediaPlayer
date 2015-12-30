using MyWindowsMediaPlayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.ViewModel
{
    class PlaylistListVM
    {
        #region Attributes
        private List<Playlist> _playlists = new List<Playlist>();
        #endregion

        #region Properties
        public List<Playlist> Playlists
        {
            get { return (_playlists); }
            set { _playlists = value; }
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
        }
        #endregion
    }
}

using MyWindowsMediaPlayer.Model;
using MyWindowsMediaPlayer.Service;
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

        private IPlayerService _playerService = null;
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

            // Ajouter la nouvelle playlist au XML
        }

        public bool CanCreatePlaylist(object arg)
        {
            return (true);
        }
        #endregion

        #region Saving
        public void SavePlaylist()
        {

            System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\C# - MyWindowsMediaPlayer\Save\playlist.xml");

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
                }
                save.Add(playlistName);
            }
            file.WriteLine(save);
            file.Close();
        }
        #endregion

        #region Ctor / Dtor
        public PlaylistListVM(IPlayerService playerService)
        {
            _playerService = playerService;

            //Todo : A virer
            //SavePlaylist(); //Todo : Methode génération de la playlist dans un xml
            //Todo : Path de Playlist.xml à changer

            var xdoc = XDocument.Load(@"E:\C# - MyWindowsMediaPlayer\Save\playlist.xml");

            var names = from i in xdoc.Descendants("playlist")
                        select new
                        {
                            Path = (string)i.Attribute("name")
                        };

            var paths1 = xdoc.Descendants("playlist")
                            .SelectMany(x => x.Descendants("media"), (pl, media) => Tuple.Create(pl.Attribute("name").Value, media.Attribute("path").Value))
                            .GroupBy(x => x.Item1)
                            .ToList();

            int inc = 0;

            foreach (var name1 in names)
            {
                System.Diagnostics.Debug.WriteLine(name1.Path);
                System.Diagnostics.Debug.WriteLine(inc);
                _playlists.Add(new Playlist(name1.Path)); // ajout de la playlist
                foreach (var name in paths1.Where(x => x.Key == name1.Path))
                {
                    foreach (var tuple in name)
                    {
                        if (tuple.Item2.Length > 0)
                        {
                            System.Diagnostics.Debug.WriteLine(tuple.Item2);
                            _playlists[inc].Add(Media.Factory.make(tuple.Item2));//ajout des paths des fichiers
                        }
                    }
                }
                inc++;
            }

        }
        #endregion
    }
}
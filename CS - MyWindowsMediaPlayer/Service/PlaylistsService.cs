﻿using MyWindowsMediaPlayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyWindowsMediaPlayer.Service
{
    class PlaylistsService : List<Playlist>
    {
        #region Singleton
        static PlaylistsService _instance = new PlaylistsService();
        static List<string> _importedFiles = new List<string>();

        private PlaylistsService(){}

        static public PlaylistsService Instance
        {
            get { return (_instance); }
        }
        #endregion

        #region Methods
        public void ImportOnce(string file)
        {
            if (!_importedFiles.Contains(file))
            {
                _importedFiles.Add(file);
                this.Import(file);
            }
        }

        public void Import(string file)
        {
            var xdoc = XDocument.Load(file);

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
                //System.Diagnostics.Debug.WriteLine(name1.Path);
                //System.Diagnostics.Debug.WriteLine(inc);
                this.Add(new Playlist(name1.Path)); // ajout de la playlist
                foreach (var name in paths1.Where(x => x.Key == name1.Path))
                {
                    foreach (var tuple in name)
                    {
                        if (tuple.Item2.Length > 0)
                        {
                            //System.Diagnostics.Debug.WriteLine(tuple.Item2);
                            this[inc].Add(Media.Factory.make(tuple.Item2));//ajout des paths des fichiers
                        }
                    }
                }
                inc++;
            }
        }

        public void Export(string file)
        {
            System.IO.StreamWriter outFile = new System.IO.StreamWriter(file);

            XElement save = new XElement("playlists");
            String media = "";
            String path = "";
            foreach (var item in this)
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
            outFile.WriteLine(save);
            outFile.Close();
        }
        #endregion
    }
}

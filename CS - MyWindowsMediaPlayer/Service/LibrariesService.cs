using MyWindowsMediaPlayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyWindowsMediaPlayer.Service
{
    class LibrariesService : List<Library<Media>>
    {
        #region Singleton
        static LibrariesService _instance = new LibrariesService();

        private LibrariesService() { }

        static public LibrariesService Instance
        {
            get { return (_instance); }
        }
        #endregion

        #region Methods
        public void Import(string file)
        {
            var xdoc = XDocument.Load(file);

            var names = from i in xdoc.Descendants("libraries")
                        select new
                        {
                            Path = (string)i.Attribute("name")
                        };
        }

        public void Export(string file)
        {
            System.IO.StreamWriter outFile = new System.IO.StreamWriter(file);

            XElement save = new XElement("libraries");
            String dir = "";
            String path = "";
            foreach (var item in this)
            {
                XElement playlistName = new XElement("libraries");
                dir = item.Folders.ToString();
                System.Diagnostics.Debug.WriteLine(dir);
                //playlistName.SetAttributeValue("name", media);
                /*foreach (var elem in item)
                {
                    XElement xml = new XElement("media");
                    path = elem.Path.LocalPath;
                    xml.SetAttributeValue("path", path);
                    playlistName.Add(xml);
                }
                save.Add(playlistName);*/
            }
            //outFile.WriteLine(save);
            outFile.Close();
        }
        #endregion
    }
}

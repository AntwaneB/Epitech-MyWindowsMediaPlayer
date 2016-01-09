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

            var names = from i in xdoc.Descendants("library")
                        select new
                        {
                            Type = (string)i.Attribute("type")
                        };

            var paths1 = xdoc.Descendants("library")
                            .SelectMany(x => x.Descendants("media"), (pl, media) => Tuple.Create(pl.Attribute("type").Value, media.Attribute("path").Value))
                            .GroupBy(x => x.Item1)
                            .ToList();

            foreach (var libtype in names)
            {
                List<Uri> pathList = new List<Uri>();
                foreach (var name in paths1.Where(x => x.Key == libtype.Type))
                {
                    foreach (var tuple in name)
                    {
                        if (tuple.Item2.Length > 0)
                        {
                            Uri myUri = new Uri(tuple.Item2);
                            pathList.Add(myUri);
                        }
                    }
                }
                this.Add(new Library<Type.GetType(libtype) > (pathList));
            }
        }

        public void Export(string file)
        {
            System.IO.StreamWriter outFile = new System.IO.StreamWriter(file);

            XElement libSave = new XElement("libraries");
            String type = "";
            String dir = "";
            foreach (var item in this)
            {
                XElement libType = new XElement("library");
                type = item.MediaType.ToString();
                libSave.SetAttributeValue("type", type);
                foreach (var elem in item.Folders)
                {
                    XElement xml = new XElement("media");

                    dir = elem.ToString();
                    xml.SetAttributeValue("path", dir);
                    libType.Add(xml);
                }
            }
            outFile.WriteLine(libSave);
            outFile.Close();
        }
        #endregion
    }
}

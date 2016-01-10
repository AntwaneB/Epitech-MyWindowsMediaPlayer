using MyWindowsMediaPlayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyWindowsMediaPlayer.Service
{
    class LibrariesService : List<Library>
    {
        #region Singleton
        static LibrariesService _instance = new LibrariesService();
        static List<string> _importedFiles = new List<string>();

        private LibrariesService() { }

        static public LibrariesService Instance
        {
            get { return (_instance); }
        }
        #endregion

        #region Methods
        public Library FindByType(Type type)
        {
            var matches = this.Where(p => p.MediaType == type);

            return (matches.Count() > 0 ? matches.First() : null);
        }

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

            var names = from i in xdoc.Descendants("library")
                        select new
                        {
                            Type = (string)i.Attribute("type")
                        };

            var paths1 = xdoc.Descendants("library")
                            .SelectMany(x => x.Descendants("folder"), (pl, media) => Tuple.Create(pl.Attribute("type").Value, media.Attribute("path").Value))
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

                /*
                Type libraryType = typeof(Library<>);
                Type libraryCurrentType = Type.GetType("MyWindowsMediaPlayer.Model." + libtype.Type);
                Type libraryConstructed = libraryType.MakeGenericType(libraryCurrentType);
                object libraryInstance = Activator.CreateInstance(libraryConstructed);
                Library<Media> library = libraryInstance as Library<Media>;
                if (libraryInstance == null)
                    System.Diagnostics.Debug.WriteLine("CONNARD");
                if (library == null)
                    System.Diagnostics.Debug.WriteLine("CONNARD");
                library.Folders = pathList;
                this.Add(library);
                */

                Library library = new Library();
                library.Folders = pathList;
                library.MediaType = Type.GetType(libtype.Type);
                this.Add(library);
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
                libType.SetAttributeValue("type", type);
                foreach (var elem in item.Folders)
                {
                    XElement xml = new XElement("folder");
                    dir = elem.LocalPath;
                    xml.SetAttributeValue("path", dir);
                    libType.Add(xml);
                }
                libSave.Add(libType);
            }
            outFile.WriteLine(libSave);
            outFile.Close();
        }
        #endregion
    }
}

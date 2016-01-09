using MyWindowsMediaPlayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }

        public void Export(string file)
        {
            
        }
        #endregion
    }
}

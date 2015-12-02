using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.Model
{
    [Serializable]
    abstract class Media
    {
        #region Attributes
        private string _path;
        private string _title;
        private string _artist;
        #endregion

        #region Properties
        public string Path
        {
            get { return (_path); }
            set { _path = value; }
        }
        public string Title
        {
            get { return (_title); }
            set { _title = value; }
        }
        public string Artist
        {
            get { return (_artist); }
            set { _artist = value; }
        }
        #endregion

        #region Ctor / Dtor
        public Media()
        {
            _path = null;
            _title = null;
            _artist = null;
        }

        public Media(string path, string title = "", string artist = "")
        {
            _path = path;
            _title = title;
            _artist = artist;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.Model
{
    abstract class Media
    {
        #region Factory
        public static class Factory
        {
            private static Dictionary<string, Type> _extensions = new Dictionary<string, Type>
            {
                { "mp4", typeof(Video) },
                { "mpeg", typeof(Video) },
                { "avi", typeof(Video) },
                { "wmv", typeof(Video) },
                { "mp3", typeof(Music) },
                { "wav", typeof(Music) },
                { "jpg", typeof(Image) },
            };
        }
        #endregion

        #region Attributes
        private string _path;
        private string _name;
        #endregion

        #region Properties
        public string Path
        {
            get { return (_path); }
            set
            {
                _path = value;
                this.parseName();
            }
        }
        public string Name
        {
            get { return (_name); }
        }
        #endregion

        #region Ctor / Dtor
        public Media()
        {
            _path = null;
        }

        public Media(string path)
        {
            _path = path;

            this.parseName();
        }
        #endregion

        #region Methods
        private void parseName()
        {
            _name = System.IO.Path.GetFileNameWithoutExtension(_path);
        }
        #endregion
    }
}

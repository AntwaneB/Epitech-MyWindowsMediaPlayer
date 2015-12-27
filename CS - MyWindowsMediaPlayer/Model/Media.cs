using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyWindowsMediaPlayer.Model
{
    abstract class Media
    {
        #region Factory
        public static class Factory
        {
            private static Dictionary<string, Func<string, Media>> _extensions = new Dictionary<string, Func<string, Media>>
            {
                { "mp4", ((string path) => new Video(path)) },
                { "mpeg", ((string path) => new Video(path)) },
                { "avi", ((string path) => new Video(path)) },
                { "wmv", ((string path) => new Video(path)) },
                { "mp3", ((string path) => new Music(path)) },
                { "wav", ((string path) => new Music(path)) },
                { "jpg", ((string path) => new Image(path)) },
            };

            public static Media make(string path)
            {
                string extension = System.IO.Path.GetExtension(path).Substring(1);

                if (!_extensions.ContainsKey(extension))
                    return (null);

                return (_extensions[extension].Invoke(path));
            }
        }
        #endregion

        #region Attributes
        private Uri _path;
        private string _name;
        private MediaState _state;
        #endregion

        #region Properties
        public Uri Path
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
        public MediaState State
        {
            get { return (_state); }
            set { _state = value; }
        }
        #endregion

        #region Ctor / Dtor
        public Media()
        {
            _path = null;
        }

        public Media(string path)
        {
            _path = new Uri(path);
            _state = MediaState.Stop;

            this.parseName();
        }
        #endregion

        #region Methods
        private void parseName()
        {
            _name = System.IO.Path.GetFileNameWithoutExtension(_path.ToString());
        }
        #endregion
    }
}

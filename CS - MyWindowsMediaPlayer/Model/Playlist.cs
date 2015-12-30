using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MyWindowsMediaPlayer.Model
{
    [Serializable]
    class Playlist : List<Media>
    {
        #region Attributes
        private string _name;

        private int _currentIdx;
        #endregion

        #region Properties
        public string Name
        {
            get { return (_name); }
            set { _name = value; }
        }

        public BitmapImage Thumbnail
        {
            get
            {
                foreach (var media in this)
                {
                    if (media.Thumbnail != null)
                        return (media.Thumbnail);
                }
                return (null);
            }
        }
        #endregion

        #region Ctor / Dtor
        public Playlist()
        {
            _name = null;
            _currentIdx = 0;
        }

        public Playlist(string name)
        {
            _name = name;
            _currentIdx = 0;
        }
        #endregion

        public Media Next()
        {
            if (this.Count() > _currentIdx)
            {
                return (this.ElementAt(_currentIdx++));
            }
            else
            {
                _currentIdx = 0;
                return (null);
            }
        }

        public bool HasNext()
        {
            return (this.Count() > _currentIdx);
        }
    }
}

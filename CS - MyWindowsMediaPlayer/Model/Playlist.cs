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
        }

        public Playlist(string name)
        {
            _name = name;
        }
        #endregion
    }
}

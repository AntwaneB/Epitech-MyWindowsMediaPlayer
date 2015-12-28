using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MyWindowsMediaPlayer.Model
{
    [Serializable]
    class Video : Media
    {
        #region Attributes
        #endregion

        #region Properties
        public override BitmapImage Thumbnail
        {
            get { return (null); }
        }
        public override string Information
        {
            get { return (null); }
        }
        #endregion

        #region Ctor / Dtor
        public Video() : base()
        {
        }

        public Video(string path)
            : base(path)
        {
        }
        #endregion

        #region Methods
        #endregion
    }
}

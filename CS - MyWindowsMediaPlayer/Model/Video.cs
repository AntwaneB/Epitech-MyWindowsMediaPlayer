using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.Model
{
    class Video : Media
    {
        #region Attributes
        private TimeSpan _duration;
        #endregion

        #region Properties
        #endregion

        #region Ctor / Dtor
        public Video() : base()
        {
            _duration = default(TimeSpan);
        }

        public Video(string path, string title = "", string artist = "", TimeSpan duration = default(TimeSpan))
            : base(path, title, artist)
        {
            _duration = duration;
        }
        #endregion
    }
}

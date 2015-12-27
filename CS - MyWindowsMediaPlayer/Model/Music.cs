using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.Model
{
    [Serializable]
    class Music : Media
    {
        #region Attributes
        private TimeSpan _duration;
        #endregion

        #region Properties
        #endregion

        #region Ctor / Dtor
        public Music() : base()
        {
            _duration = default(TimeSpan);
        }

        public Music(string path, string title = "", string artist = "", TimeSpan duration = default(TimeSpan))
            : base(path)
        {
            _duration = duration;
        }
        #endregion
    }
}

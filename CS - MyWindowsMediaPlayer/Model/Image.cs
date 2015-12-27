using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.Model
{
    [Serializable]
    class Image : Media
    {
        #region Attributes
        #endregion

        #region Properties
        #endregion

        #region Ctor / Dtor
        public Image() : base()
        {
        }

        public Image(string path)
            : base(path)
        {
        }
        #endregion
    }
}

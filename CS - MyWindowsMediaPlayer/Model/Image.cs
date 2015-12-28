﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MyWindowsMediaPlayer.Model
{
    [Serializable]
    class Image : Media
    {
        #region Attributes
        #endregion

        #region Properties
        public override BitmapImage Thumbnail
        {
            get { return (null); }
        }
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

        #region Methods
        #endregion
    }
}

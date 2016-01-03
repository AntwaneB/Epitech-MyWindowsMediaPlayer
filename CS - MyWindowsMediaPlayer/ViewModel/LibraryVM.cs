using MyWindowsMediaPlayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.ViewModel
{
    class LibraryVM : ViewModelBase
    {
        #region Attributes
        protected Library _library = null;
        #endregion

        #region Properties
        public Library Library
        {
            get { return (_library); }
        }
        #endregion

        public LibraryVM()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyWindowsMediaPlayer.Model;
using MyWindowsMediaPlayer.Utils;
using System.Windows.Input;

namespace MyWindowsMediaPlayer.ViewModel
{
    class MainWindowVM
    {
        #region Attributes
        private Media _currentMedia = null;

        private DelegateCommand _loadCommand;
        private DelegateCommand _playCommand;
        private DelegateCommand _pauseCommand;
        private DelegateCommand _stopCommand;
        #endregion

        #region Properties
        public ICommand LoadCommand
        {
            get { return (_loadCommand); }
        }
        #endregion


    }
}

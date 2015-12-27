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

        private DelegateCommand _loadCommand = null;
        private DelegateCommand _playCommand = null;
        private DelegateCommand _pauseCommand = null;
        private DelegateCommand _stopCommand = null;
        #endregion

        #region Properties
        public ICommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                    _loadCommand = new DelegateCommand(OnLoad, CanLoad);

                return (_loadCommand);
            }
        }
        public ICommand PlayCommand
        {
            get { return (_playCommand); }
        }
        public ICommand PauseCommand
        {
            get { return (_pauseCommand); }
        }
        public ICommand StopCommand
        {
            get { return (_stopCommand); }
        }
        #endregion

        #region Delegate Commands
        public void OnLoad(object arg)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            var result = dialog.ShowDialog();
        }

        public bool CanLoad(object arg)
        {
            return (true);
        }
        #endregion
    }
}

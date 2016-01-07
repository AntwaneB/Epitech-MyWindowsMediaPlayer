using MyWindowsMediaPlayer.Model;
using MyWindowsMediaPlayer.Service;
using MyWindowsMediaPlayer.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace MyWindowsMediaPlayer.ViewModel
{
    abstract class LibraryVM<T> : ViewModelBase where T : Media
    {
        #region Attributes
        protected IWindowService _windowService = null;
        protected IPlayerService _playerService = null;

        protected Library<T> _library = null;

        protected ICommand _manageLibraryCommand = null;
        private ICommand _selectMediaCommand = null;
        #endregion

        #region Properties
        public Library<T> Library
        {
            get { return (_library); }
        }

        public ICommand ManageLibraryCommand
        {
            get
            {
                if (_manageLibraryCommand == null)
                    _manageLibraryCommand = new DelegateCommand(OnManageLibrary, CanManageLibrary);

                return (_manageLibraryCommand);
            }
        }
        public ICommand SelectMediaCommand
        {
            get
            {
                if (_selectMediaCommand == null)
                    _selectMediaCommand = new DelegateCommand(OnSelectMedia, CanSelectMedia);

                return (_selectMediaCommand);
            }
        }
        #endregion

        #region Commands
        public abstract void OnManageLibrary(object arg);

        public bool CanManageLibrary(object arg)
        {
            return (_library != null);
        }

        public void OnSelectMedia(object arg)
        {
            _playerService.SetMedia(arg as Media);
            _playerService.Play();
        }

        public bool CanSelectMedia(object arg)
        {
            return (_library.Items.Count > 0);
        }
        #endregion

        public LibraryVM()
        {
        }

        public LibraryVM(IWindowService windowService, IPlayerService playerService)
        {
            _windowService = windowService;
            _playerService = playerService;
        }
    }
}

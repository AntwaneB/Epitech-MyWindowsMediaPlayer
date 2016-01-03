using MyWindowsMediaPlayer.Model;
using MyWindowsMediaPlayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyWindowsMediaPlayer.ViewModel
{
    class LibraryVM : ViewModelBase
    {
        #region Attributes
        protected IWindowService _windowService = null;

        protected Library _library = null;

        protected ICommand _manageLibraryCommand = null;
        #endregion

        #region Properties
        public Library Library
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
        #endregion

        #region Commands
        public void OnManageLibrary(object arg)
        {
            if (_windowService != null)
                _windowService.CreateWindow(this);
        }

        public bool CanManageLibrary(object arg)
        {
            return (_library != null);
        }
        #endregion

        public LibraryVM()
        {
        }

        public LibraryVM(IWindowService windowService)
        {
            _windowService = windowService;
        }
    }
}

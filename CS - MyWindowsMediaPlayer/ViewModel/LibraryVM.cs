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

        protected Library<T> _library = null;

        protected ICommand _manageLibraryCommand = null;
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
        #endregion

        #region Commands
        public abstract void OnManageLibrary(object arg);

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

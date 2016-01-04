using MyWindowsMediaPlayer.Model;
using MyWindowsMediaPlayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MyWindowsMediaPlayer.ViewModel
{
    class LibraryVM : ViewModelBase
    {
        #region Attributes
        protected IWindowService _windowService = null;

        protected Library _library = null;

        protected ICommand _manageLibraryCommand = null;
        protected ICommand _addFolderCommand = null;
        #endregion

        #region Properties
        public Library Library
        {
            get { return (_library); }
        }
        public List<Uri> Folders
        {
            get { return (_library.Folders); }
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
        public ICommand AddFolderCommand
        {
            get
            {
                if (_addFolderCommand == null)
                    _addFolderCommand = new DelegateCommand(OnAddFolder, CanAddFolder);

                return (_addFolderCommand);
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

        public void OnAddFolder(object arg)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();

            _library.Folders.Add(new Uri(dialog.SelectedPath));
            NotifyPropertyChanged("Library");
        }

        public bool CanAddFolder(object arg)
        {
            return (true);
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

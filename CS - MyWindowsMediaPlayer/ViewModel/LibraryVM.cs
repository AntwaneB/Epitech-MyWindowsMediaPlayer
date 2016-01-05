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
    class LibraryVM<T> : ViewModelBase where T : Media
    {
        #region Attributes
        protected IWindowService _windowService = null;

        protected Library<T> _library = null;

        protected ICommand _manageLibraryCommand = null;
        protected ICommand _addFolderCommand = null;
        protected ICommand _removeFolderCommand = null;
        #endregion

        #region Properties
        public Library<T> Library
        {
            get { return (_library); }
        }
        public ICollectionView Folders
        {
            get { return (new ListCollectionView(_library.Folders)); }
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
        public ICommand RemoveFolderCommand
        {
            get
            {
                if (_removeFolderCommand == null)
                    _removeFolderCommand = new DelegateCommand(OnRemoveFolder, CanRemoveFolder);

                return (_removeFolderCommand);
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

            _library.AddFolder(new Uri(dialog.SelectedPath));
            NotifyPropertyChanged("Folders");
            NotifyPropertyChanged("Library");
        }

        public bool CanAddFolder(object arg)
        {
            return (true);
        }

        public void OnRemoveFolder(object arg)
        {
            string folder = ((Uri)arg).ToString();


            _library.RemoveFolder(new Uri(folder));
            NotifyPropertyChanged("Folders");
            NotifyPropertyChanged("Library.Items");
        }

        public bool CanRemoveFolder(object arg)
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

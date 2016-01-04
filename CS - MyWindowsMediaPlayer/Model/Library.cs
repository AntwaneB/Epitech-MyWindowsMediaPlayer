using MyWindowsMediaPlayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.Model
{
    [Serializable]
    class Library : PropertyChangedBase
    {
        #region Attributes
        private List<Uri> _folders = new List<Uri>();
        private List<Media> _items = new List<Media>();
        #endregion

        #region Properties
        public List<Uri> Folders
        {
            get { return (_folders); }
            set
            {
                _folders = value;
                loadItems();
            }
        }

        public List<Media> Items
        {
            get { return (_items); }
        }
        #endregion

        #region Ctor / Dtor
        public Library()
        {
        }

        public Library(List<Uri> folders)
        {
            _folders.AddRange(folders);
        }
        #endregion

        #region Methods
        private void loadItems()
        {

        }

        public void AddFolder(Uri folder)
        {
            if (!_folders.Contains(folder))
            {
                _folders.Add(folder);
                OnPropertyChanged("Folders");
            }
        }

        public void RemoveFolder(Uri folder)
        {
            if (_folders.Contains(folder))
            {
                _folders.Remove(folder);
                OnPropertyChanged("Folders");
            }
        }
        #endregion
    }
}

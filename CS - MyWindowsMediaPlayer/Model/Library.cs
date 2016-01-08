using MyWindowsMediaPlayer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.Model
{
    [Serializable]
    class Library<T> : PropertyChangedBase where T : Media
    {
        #region Attributes
        private List<Uri> _folders = new List<Uri>();
        private List<T> _items = new List<T>();
        #endregion

        #region Properties
        public List<Uri> Folders
        {
            get { return (_folders); }
            set
            {
                _folders = value;
                LoadItems();
            }
        }

        public List<T> Items
        {
            get { return (_items); }
        }

        public List<string> Extensions;
        #endregion

        #region Ctor / Dtor
        public Library()
        {
            LoadItems();
        }

        public Library(List<Uri> folders)
        {
            _folders.AddRange(folders);
            LoadItems();
        }
        #endregion

        #region Methods
        public void LoadItems()
        {
            var items = new List<string>();

            foreach (var folder in _folders)
            {
                try
                {
                    if (Directory.Exists(folder.LocalPath))
                        items.AddRange(Directory.GetFiles(folder.LocalPath, "*.*").Where(s => Extensions.Any(e => s.EndsWith(e))));
                } catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Unable to load media from folder \"" + folder + "\": " + e.ToString());
                }
            }

            var medias = new List<T>();
            foreach (var item in items)
            {
                Uri path = new Uri(item);

                var existingMedias = _items.Where(i => i.Path == path);
                if (existingMedias.Count() == 0)
                    medias.Add((T)Media.Factory.make(item));
                else
                    medias.AddRange(existingMedias);
            }
            _items = medias;
            
            OnPropertyChanged("Items");
        }

        public void AddFolder(Uri folder)
        {
            if (!_folders.Contains(folder))
            {
                _folders.Add(folder);
                LoadItems();
                OnPropertyChanged("Folders");
            }
        }

        public void RemoveFolder(Uri folder)
        {
            if (_folders.Contains(folder))
            {
                _folders.Remove(folder);
                LoadItems();
                OnPropertyChanged("Folders");
            }
        }

        #endregion
    }
}

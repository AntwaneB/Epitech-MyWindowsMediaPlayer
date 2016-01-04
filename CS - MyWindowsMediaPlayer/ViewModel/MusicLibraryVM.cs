﻿using MyWindowsMediaPlayer.Model;
using MyWindowsMediaPlayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.ViewModel
{
    class MusicLibraryVM : LibraryVM<Music>
    {
        public MusicLibraryVM(IWindowService windowService)
            : base(windowService)
        {
            _library = new Library<Music>();
            _library.Extensions = Music.Extensions;
            _library.Folders = new List<Uri>() { new Uri(@"E:\Projets\CS - MyWindowsMediaPlayer\Example Medias\"), new Uri(@"E:\C# - MyWindowsMediaPlayer\Example Medias") };
        }
    }
}

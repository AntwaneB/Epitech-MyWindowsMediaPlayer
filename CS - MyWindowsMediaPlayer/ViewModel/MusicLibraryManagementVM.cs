using MyWindowsMediaPlayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.ViewModel
{
    class MusicLibraryManagementVM : LibraryManagementVM<Music>
    {
        public MusicLibraryManagementVM(Library<Music> library)
            : base(library)
        {
        }
    }
}

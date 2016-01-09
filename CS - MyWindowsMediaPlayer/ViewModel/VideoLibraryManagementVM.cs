using MyWindowsMediaPlayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.ViewModel
{
    class VideoLibraryManagementVM : LibraryManagementVM<Video>
    {
        public VideoLibraryManagementVM(Library<Video> library)
            : base(library)
        {
        }
    }
}

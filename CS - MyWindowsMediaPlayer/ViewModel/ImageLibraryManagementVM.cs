using MyWindowsMediaPlayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.ViewModel
{
    class ImageLibraryManagementVM : LibraryManagementVM<Image>
    {
        public ImageLibraryManagementVM(Library<Image> library)
            : base(library)
        {
        }
    }
}

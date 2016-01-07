using MyWindowsMediaPlayer.Model;
using MyWindowsMediaPlayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.ViewModel
{
    class ImageLibraryVM : LibraryVM<Image>
    {
        public ImageLibraryVM(IWindowService windowService, IPlayerService playerService)
            : base(windowService, playerService)
        {
            _library = new Library<Image>();
            _library.Extensions = Image.Extensions;
            _library.Folders = new List<Uri>() { new Uri(@"E:\Projets\CS - MyWindowsMediaPlayer\Example Medias\"), new Uri(@"E:\C# - MyWindowsMediaPlayer\Example Medias") };
        }

        public override void OnManageLibrary(object arg)
        {
            if (_windowService != null)
                _windowService.CreateWindow(new ImageLibraryManagementVM(_library));
        }
    }
}

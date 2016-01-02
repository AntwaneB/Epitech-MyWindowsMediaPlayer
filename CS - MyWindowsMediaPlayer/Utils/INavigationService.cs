using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer.Utils
{
    interface INavigationService
    {
        void Navigate(string page);
        void Navigate(string page, object parameter);
    }
}

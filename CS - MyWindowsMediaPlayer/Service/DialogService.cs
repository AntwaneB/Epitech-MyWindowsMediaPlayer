using MyWindowsMediaPlayer.View.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyWindowsMediaPlayer.Service
{
    class DialogService
    {
        public bool ConfirmationDialog(string message, string title)
        {
            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes || result == MessageBoxResult.OK)
                return (true);
            else
                return (false);
        }

        public string InputDialog(string message, string title)
        {
            var dialog = new TextBoxDialog(title, message);

            if (dialog.ShowDialog() == true)
                return (dialog.Input);
            else
                return (null);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MyWindowsMediaPlayer.Model
{
    [Serializable]
    class Music : Media
    {
        #region Attributes
        #endregion

        #region Properties
        public override string Name
        {
            get
            {
                var file = TagLib.File.Create(_path.LocalPath);
                if (!string.IsNullOrEmpty(file.Tag.Title))
                    return (file.Tag.Title);
                else
                    return (base.Name);
            }
        }

        public override BitmapImage Thumbnail
        {
            get
            {
                try
                {
                    var file = TagLib.File.Create(_path.LocalPath);
                    if (file.Tag.Pictures.Length >= 1)
                    {
                        var bin = (byte[])(file.Tag.Pictures[0].Data.Data);
                        System.Drawing.Image bmp = System.Drawing.Image.FromStream(new MemoryStream(bin)).GetThumbnailImage(100, 100, null, IntPtr.Zero);
                        MemoryStream ms = new MemoryStream();
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        ms.Position = 0;
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = ms;
                        bi.EndInit();

                        return (bi);
                    }
                    else
                        return (null);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Unable to load thumbnail image:" + e.ToString());
                    return (null);
                }
            }
        }
        public override string Information
        {
            get
            {
                try
                {
                    var file = TagLib.File.Create(_path.LocalPath);
                    if (file.Tag.AlbumArtists.Length > 0)
                        return (file.Tag.Album + " - " + string.Join(", ", file.Tag.Artists));
                    else
                        return (file.Tag.Album);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Unable to load informations:" + e.ToString());
                    return (null);
                }
            }
        }
        #endregion

        #region Ctor / Dtor
        public Music() : base()
        {
        }

        public Music(string path)
            : base(path)
        {
        }
        #endregion

        #region Methods
        #endregion
    }
}

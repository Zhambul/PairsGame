﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PairsGame.Properties;

namespace PairsGame.Bl
{
    public class Util
    {
        public static ImageBrush Convert(Bitmap bitmap)
        {
            return new ImageBrush
            {
                ImageSource = Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions())
            };
        }
    }
}

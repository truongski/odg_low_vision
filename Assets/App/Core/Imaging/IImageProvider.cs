using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace App.Core.Imaging
{
    public interface IImageProvider
    {
        Color[] GetPixels(int x, int y, int width, int height);

        int width { get; }
        int height { get; }
    }
}

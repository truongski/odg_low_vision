using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace App.Core.Imaging
{
    public class WebCamTextureProvider : MonoBehaviour, IImageProvider
    {
        public WebCamTexture texture { get; private set; }

        public int width
        {
            get { return texture.width; }
        }

        public int height
        {
            get { return texture.height; }
        }

        public Color[] GetPixels(int x, int y, int width, int height)
        {
            return texture.GetPixels(x, y, width, height);
        }

        private void Start()
        {
            texture = new WebCamTexture();
            texture.Play();
        }
    }
}

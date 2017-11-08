
using App.Core.Imaging;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.Behaviours
{
    public class TextureTransformer : MonoBehaviour
    {
        [SerializeField]
        private IImageProvider source;

        [SerializeField]
        private Material target;

        private Texture2D bufferTexture;

        public Vector2 max;
        public Vector2 min;
        public Vector2 textureSize;
        public Vector2 center;
        public int width;
        public int height;

        private void Start()
        {
            bufferTexture = new Texture2D(0, 0);

            source = FindObjectOfType<WebCamTextureProvider>();
        }

        private void Update()
        {
            width = Mathf.Clamp(width, 0, source.width);
            height = Mathf.Clamp(height, 0, source.height);
            float halfWidth = (width / 2f);
            float halfHeight = (height / 2f);
            min.x = halfWidth;
            min.y = halfHeight;
            max.x = source.width - halfWidth;
            max.y = source.height - halfHeight;
            textureSize.x = source.width;
            textureSize.y = source.height;

            center.x = Mathf.Clamp(center.x, min.x, max.x);
            center.y = Mathf.Clamp(center.y, min.y, max.y);

            if (bufferTexture.width != width || bufferTexture.height != height)
            {
                bufferTexture.Resize(width, height);
                Debug.LogFormat("Resizing buffer to ({0}, {1})", bufferTexture.width, bufferTexture.height);
            }

            if (width * height == 0)
            {
                return;
            }

            int left = Mathf.RoundToInt(center.x - halfWidth);
            int bottom = Mathf.RoundToInt(center.y - halfHeight);

            Color[] pixels = source.GetPixels(left, bottom, width, height);
            bufferTexture.SetPixels(pixels);
            bufferTexture.Apply();

            target.mainTexture = bufferTexture;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace App.Core.Imaging
{
    public class VideoRecorder : MonoBehaviour
    {
        [SerializeField]
        private Camera targetCamera;
        private Texture2D tex;
        private RenderTexture rt;

        private Rect rect;

        private int state = 0;

        public string path;
        public int maxFrameCount;
        public int frameCount;
        public float startTime;
        public float fps;

        private void Start()
        {
            tex = new Texture2D(256, 256);
            rt = new RenderTexture(256, 256, 24);

            rect = new Rect(0, 0, 256, 256);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            startTime = Time.time;
        }

        private void Update()
        {
            if (frameCount > maxFrameCount)
            {
                return;
            }

            fps = frameCount / (Time.time - startTime);

            if (state == 0)
            {
                targetCamera.targetTexture = rt;
                targetCamera.Render();

                RenderTexture.active = rt;
                tex.ReadPixels(rect, 0, 0);
                state = 1;
            }
            else if (state == 1)
            {
                SaveFrame(tex);
                state = 0;
            }
        }

        private void SaveFrame(Texture2D tex)
        {
            using (FileStream fs = new FileStream(Path.Combine(path, frameCount + ".jpg"), FileMode.Create, FileAccess.Write))
            {
                byte[] jpg = tex.EncodeToJPG(80);
                fs.Write(jpg, 0, jpg.Length);
                frameCount++;
            }
        }
    }
}

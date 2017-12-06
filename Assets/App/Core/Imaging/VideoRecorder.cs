using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace App.Core.Imaging
{
    public class VideoRecorder : MonoBehaviour
    {
        [SerializeField]
        private Camera targetCamera;
        [SerializeField]
        private Camera[] targetCameras;
        private Texture2D tex;
        private RenderTexture rt;

        private Rect rect;

        private UdpClient sender;
        private IPEndPoint sendTo;
        public string hostOrAddress;
        public int port;
        private int state = 0;

        public string path;
        public int maxFrameCount;
        public int frameCount;
        public float startTime;
        public float fps;

        private void Start()
        {
            tex = new Texture2D(640, 480);
            rt = new RenderTexture(640, 480, 24);

            rect = new Rect(0, 0, 640, 480);
            sender = new UdpClient();
            sendTo = new IPEndPoint(IPAddress.Parse(hostOrAddress), port);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            startTime = Time.time;
        }

        private void Update()
        {
            fps = frameCount / (Time.time - startTime);

            if (state == 0)
            {
                for (int i = 0; i < targetCameras.Length; i++)
                {
                    RenderTexture oldTex = targetCameras[i].targetTexture;
                    Rect oldRect = targetCameras[i].rect;
                    targetCameras[i].targetTexture = rt;
                    targetCameras[i].rect = new Rect(0, 0, 1, 1);
                    targetCameras[i].Render();

                    RenderTexture.active = rt;
                    tex.ReadPixels(rect, 0, 0);
                    targetCameras[i].targetTexture = oldTex;
                    targetCameras[i].rect = oldRect;
                }

                state = 1;
            }
            else if (state == 1)
            {
                SendFrame(tex);
                frameCount++;
                state = 0;
            }
        }

        private void SendFrame(Texture2D texture)
        {
            byte[] jpg = tex.EncodeToJPG(60);
            sender.Send(jpg, jpg.Length, sendTo);
        }

        private void SaveFrame(Texture2D tex)
        {
            using (FileStream fs = new FileStream(Path.Combine(path, frameCount + ".jpg"), FileMode.Create, FileAccess.Write))
            {
                byte[] jpg = tex.EncodeToJPG(80);
                fs.Write(jpg, 0, jpg.Length);
            }
        }
    }
}

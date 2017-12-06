using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.Streaming
{
    public class UdpJpegReceiverBehaviour : MonoBehaviour
    {
        [SerializeField]
        private RawImage targetImage;
        private UdpJpegReceiver receiver;
        private Queue<Action> actionQueue;
        public int listenPort;
        private object actionLock = new object();
        private void Start()
        {
            actionQueue = new Queue<Action>();
            receiver = new UdpJpegReceiver(listenPort);
            targetImage.texture = new Texture2D(800, 600);
            receiver.onDataReceived += OnDataReceive;
        }

        private void OnDataReceive(byte[] data)
        {
            lock (actionLock)
            {
                if (actionQueue.Count < 10)
                {
                    actionQueue.Enqueue(() =>
                    {
                        (targetImage.texture as Texture2D).LoadImage(data);
                    });
                }
                else
                {
                    actionQueue.Clear();
                }
            }
        }

        private void Update()
        {
            lock (actionLock)
            {
                Action action = null;
                if (actionQueue.Count > 0)
                {
                    action = actionQueue.Dequeue();
                }

                if (action != null)
                {
                    action();
                }
            }
        }

        private void OnDestroy()
        {
            if (receiver != null)
            {
                receiver.Dispose();
            }            
        }
        private void OnApplicationQuit()
        {
            if (receiver != null)
            {
                receiver.Dispose();
            }
        }
    }
}
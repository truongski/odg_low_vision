using UnityEngine;
using UnityEngine.UI;

namespace App.Streaming
{
    public class UdpJpegReceiverBehaviour : MonoBehaviour
    {
        [SerializeField]
        private RawImage targetImage;
        private UdpJpegReceiver receiver;

        private void Start()
        {
            receiver = new UdpJpegReceiver(1500);
            targetImage.texture = new Texture2D(256, 256);
        }

        private void OnDataReceive(byte[] data)
        {
            (targetImage.texture as Texture2D).LoadImage(data);
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
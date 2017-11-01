
using UnityEngine;

namespace Core
{
    public class WebCamBehaviour : MonoBehaviour
    {
        private WebCamTexture texture;
        [SerializeField]
        private Material targetMaterial;
        private void Start()
        {
            texture = new WebCamTexture();
            
            texture.Play();

            targetMaterial.mainTexture = texture;
        }
    }
}
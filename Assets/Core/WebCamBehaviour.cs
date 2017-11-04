
using UnityEngine;

namespace Core
{
    public class WebCamBehaviour : MonoBehaviour
    {
        private WebCamTexture texture;
        [SerializeField]
        private Material targetMaterial;
		private Texture2D tex; 
		Color[] colors = new Color[900];

        private void Start()
        {
			tex = new Texture2D(256, 256);
            texture = new WebCamTexture();

            texture.Play();


//
//			colors = texture.GetPixels(100, 100, 200,200);
//			tex.SetPixels (0, 0, 200, 200, colors);
//			tex.Apply();
//
//			targetMaterial.mainTexture = tex;

        }

		void FixedUpdate () {
			
			colors = texture.GetPixels(100, 100, 200,200);
			tex.SetPixels (0, 0, 200, 200, colors);
			tex.Apply();

			targetMaterial.mainTexture = tex;
		}
    }
}
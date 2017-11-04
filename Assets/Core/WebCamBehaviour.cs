
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class WebCamBehaviour : MonoBehaviour
    {
        private WebCamTexture texture;
        [SerializeField]
        private Material targetMaterial;
		private Texture2D tex; 
		Color[] colors = new Color[900];
		int startPosX = 0;
		int startPosY = 0;
		public Slider sliderX;
		public Slider sliderY;
	

        private void Start()
        {
			tex = new Texture2D(200, 200);
            texture = new WebCamTexture();
            texture.Play();


        }

		void FixedUpdate () {
			sliderX.maxValue = texture.width - 200;
			sliderY.maxValue = texture.height - 200;
			colors = texture.GetPixels(startPosX, startPosY, 200,200);
			tex.SetPixels (0, 0, 200, 200, colors);
			tex.Apply();

			targetMaterial.mainTexture = tex;
		}






		public void setX (){
			startPosX = (int)sliderX.value;
		}

		public void setY (){
			startPosY = (int)sliderY.value;
		}
    }


}
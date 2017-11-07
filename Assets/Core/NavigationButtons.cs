using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	public class NavigationButtons : MonoBehaviour 
	{

		public Transform viewCameraTransform;
		public float sensitivity = 0.5f;
		public GameObject menu;

		public void ZoomIn()
		{
			Vector3 forward = viewCameraTransform.transform.forward * 1;
			this.transform.position += forward;
		}

		public void ZoomOut()
		{
			Vector3 forward = viewCameraTransform.transform.forward * -1;
			this.transform.position += forward;
		}

		public void TranslateX(float x)
		{
			Vector3 right = viewCameraTransform.right * x;
			this.transform.position += right;
		}

		public void TranslateY(float y)
		{
			Vector3 up = viewCameraTransform.up * y;
			this.transform.position += up;
		}

		public void ToggleMenu(bool toggle)
		{
			menu.SetActive (toggle);
		}
	}
}
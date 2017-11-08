using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Behaviours
{
	public class ObjectNavigator : MonoBehaviour 
	{
		public Transform referenceTransform;
		public float sensitivity = 0.5f;

        /// <summary>
        /// Translate object with respect to referenceTransform.
        /// </summary>
        /// <param name="translation"></param>
		public void Translate(Vector3 translation)
		{
			this.transform.position += referenceTransform.TransformVector(translation);
		}
	}
}
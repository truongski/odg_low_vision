using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace App.Core
{
    public class CanvasFix : MonoBehaviour
    {
        private RectTransform rect;
        [SerializeField]
        private Camera target;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }

        private void Update()
        {
            Vector3 bottomLeft = target.ViewportToWorldPoint(new Vector3(0, 0, 1));
            Vector3 topRight = target.ViewportToWorldPoint(Vector3.one);
            rect.sizeDelta = new Vector2(
                (topRight.x - bottomLeft.x) / this.transform.localScale.x, 
                (topRight.y - bottomLeft.y) / this.transform.localScale.y);
        }
    }
}

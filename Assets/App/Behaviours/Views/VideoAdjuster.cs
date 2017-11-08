using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace App.Behaviours.Views
{
    public class VideoAdjuster : MonoBehaviour
    {
        [SerializeField]
        private Slider horizontal;
        [SerializeField]
        private Slider vertical;
        [SerializeField]
        private Slider size;
        [SerializeField]
        private TextureTransformer textureRect;

        private void Start()
        {
            horizontal.onValueChanged.AddListener((value) =>
            {
                textureRect.center.x = value;
            });

            vertical.onValueChanged.AddListener((value) =>
            {
                textureRect.center.y = value;
            });

            size.onValueChanged.AddListener((value) =>
            {
                textureRect.height = (int)value;
                textureRect.width = (int)value;
            });
        }

        private void Update()
        {
            horizontal.maxValue = textureRect.max.x;
            horizontal.minValue = textureRect.min.x;
            horizontal.value = textureRect.center.x;

            vertical.maxValue = textureRect.max.y;
            vertical.minValue = textureRect.min.y;
            vertical.value = textureRect.center.y;

            size.maxValue = textureRect.textureSize.y;
            size.minValue = 0;
            size.value = textureRect.height;
        }
    }
}

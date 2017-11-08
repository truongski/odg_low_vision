using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace App.Behaviours.Views
{
    public class NavigationView : MonoBehaviour
    {
        [SerializeField]
        private ObjectNavigator navigator;

        [SerializeField]
        private Button zoomIn;
        [SerializeField]
        private Button zoomOut;
        [SerializeField]
        private Button left;
        [SerializeField]
        private Button right;
        [SerializeField]
        private Button up;
        [SerializeField]
        private Button down;

        private void Awake()
        {
            zoomIn.onClick.AddListener(() => navigator.Translate(Vector3.forward));
            zoomOut.onClick.AddListener(() => navigator.Translate(Vector3.back));
            left.onClick.AddListener(() => navigator.Translate(Vector3.left));
            right.onClick.AddListener(() => navigator.Translate(Vector3.right));
            up.onClick.AddListener(() => navigator.Translate(Vector3.up));
            down.onClick.AddListener(() => navigator.Translate(Vector3.down));
        }
    }
}

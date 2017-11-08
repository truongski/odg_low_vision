using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace App.Behaviours.Views
{
    public class SettingsView : MonoBehaviour
    {
        public bool isShown { get { return this.gameObject.activeInHierarchy; } }

        public void Show(bool show)
        {
            this.gameObject.SetActive(show);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace App.Behaviours.Views
{
    public class HeaderView : MonoBehaviour
    {
        [SerializeField]
        private Button settingsToggler;
        [SerializeField]
        private SettingsView settings;

        private void Awake()
        {
            Assert.IsNotNull(settings, "HeaderView: settings is null");
        }

        private void Start()
        {
            settingsToggler.onClick.AddListener(ToggleSettings);
        }

        public void ToggleSettings()
        {
            settings.Show(!settings.isShown);
        }
    }
}

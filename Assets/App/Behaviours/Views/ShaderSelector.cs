using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace App.Behaviours.Views
{
    public class ShaderSelector : MonoBehaviour
    {
        public Shader[] shaders;
        public Shader selectedShader { get { return shaders[shaderIndex]; } }
        public MeshRenderer[] meshRenderers;
        private int shaderIndex;

        [SerializeField]
        private Text currentShader;
        [SerializeField]
        private Button shaderCycler;

        public string shaderCyclerText
        {
            get { return currentShader.text; }
            set { currentShader.text = value; }
        }

        private void Start()
        {
            shaderCycler.onClick.AddListener(() => CycleShader());
            CycleShader();
        }

        private void CycleShader()
        {
            shaderIndex = getNextShaderIndex(shaderIndex);
            shaderCyclerText = selectedShader.name;
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.sharedMaterial.shader = selectedShader;
            }
        }

        private int getNextShaderIndex(int index)
        {
            return (index + 1) >= shaders.Length ? 
                0 :
                index + 1;
        }
    }
}

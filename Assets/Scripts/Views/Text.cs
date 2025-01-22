using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Lumi_Namespace
{
    public class Text : MonoBehaviour
    {
        public TextSO textData;
        public Style style;

        private TextMeshProUGUI textMeshProUGUI;

        private void Awake(){
            Init();
        }

        public void Init() {
            Setup();
            Configure();
        }

        private void Setup() {
            textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
            if (textMeshProUGUI == null) {
        Debug.LogError("TextMeshProUGUI component not found in the children of this object.");
    }
        }

        private void Configure() {

            textMeshProUGUI.color = textData.theme.GetTextColor(style);
            textMeshProUGUI.font = textData.font;
            textMeshProUGUI.fontSize = textData.size;
        }

        void OnValidate() {
            Init();
        }
    }

}

namespace Lumi_Namespace
{   
    public enum Style
    {
        Primary,
        Secondary,
        Tertiary
    }
}
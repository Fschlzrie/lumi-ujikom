using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lumi_Namespace
{
    [CreateAssetMenu(menuName = "CustomUI/ViewSO", fileName = "ViewSO")]
    public class ViewSO : ScriptableObject
    {
        public ThemeSO theme;
        public RectOffset padding;
        public float spacing;
    }    
}


using UnityEngine;
using UnityEditor;
using RockVR.Vive;

namespace RockVR.Vive.Editor
{
    [CustomEditor(typeof(VIVE_RadialMenu))]
    public class VIVE_RadialMenuEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            VIVE_RadialMenu radialMenu = (VIVE_RadialMenu)target;
            if (GUILayout.Button("Regenerate Buttons"))
            {
                radialMenu.RegenerateButtons();
            }
        }
    }
}
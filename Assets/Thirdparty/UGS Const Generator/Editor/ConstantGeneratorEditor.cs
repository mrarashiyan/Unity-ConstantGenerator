/*
 * Unbound Game Studio
 * https://unboundgamestudio.ir/
 * 
 * M.R.Arashiyan
 * m.r.arashiyan@gmail.com
 */

using UnityEditor;
using UnityEngine;

namespace UGS.Editor
{
    [CustomEditor(typeof(UGSConstGenerator))]
    public class ConstantGeneratorEditor : UnityEditor.Editor
    {
         private SerializedProperty constList;
        private void OnEnable()
        {
            constList = serializedObject.FindProperty("ConstCollections");
        }

        public override void OnInspectorGUI() {
            //base.OnInspectorGUI();

            var ie = (UGSConstGenerator)target;

            EditorGUILayout.PropertyField(constList);
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Save Constants"))
            {
                ie.SaveConstCollections();
            }

            if (GUILayout.Button("Clear All Const Files"))
            {
                ie.ClearFolder();
            }
            
            serializedObject.ApplyModifiedProperties();
        }
        
        
        [CustomPropertyDrawer(typeof(UGSConstGenerator.SingleConst))]
        public class SingleConstDrawerUIE : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {

                EditorGUI.BeginProperty(position, label, property);

                var prefixPosition = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
                
                var indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                
                var nameRect = new Rect(position.x, position.y, 100, position.height);
                var unitRect = new Rect(position.x + 110, position.y, 50, position.height);
                var amountRect = new Rect(position.x + 162, position.y, position.width - 160, position.height);

                // Draw fields - passs GUIContent.none to each so they are drawn without labels

                switch (property.FindPropertyRelative("constType").intValue )
                {
                    case (int)UGSConstGenerator.ConstType.INT:
                        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("constValueInt"), GUIContent.none);
                        break;
                    case (int)UGSConstGenerator.ConstType.FLOAT:
                        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("constValueFloat"), GUIContent.none);
                        break;
                    case (int)UGSConstGenerator.ConstType.STRING:
                        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("constValueString"), GUIContent.none);
                        break;
                    case (int)UGSConstGenerator.ConstType.BOOL:
                        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("constValueBool"), GUIContent.none);
                        break;
                        
                }
                EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("constType"), GUIContent.none);
                EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("constKey"), GUIContent.none);
                
                
                EditorGUI.indentLevel = indent;
                
                EditorGUI.EndProperty();
            }
        }
    }
    
    
}

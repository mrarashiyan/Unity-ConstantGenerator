/*
 * Unbound Game Studio
 * https://unboundgamestudio.ir/
 * 
 * M.R.Arashiyan
 * m.r.arashiyan@gmail.com
 */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UGS
{
    [CreateAssetMenu(fileName = "New ConstantGenerator", menuName
        = "UGS/New Constant Generator")]
    public class UGSConstGenerator : ScriptableObject
    {
        [System.Serializable]
        public class ConstCollection
        {
            public string collectionName;
            public List<SingleConst> collectionKeys;
        }

        [System.Serializable]
        public class SingleConst
        {
            public string constKey;
            public ConstType constType;
            public int constValueInt;
            public float constValueFloat;
            public string constValueString;
            public bool constValueBool;
        }

        public enum ConstType
        {
            None,
            STRING,
            INT,
            FLOAT,
            BOOL
        }

        public List<ConstCollection> ConstCollections;
        private string m_FileTemplate;
        private string m_KeyPlaceholder = "/* @@@@  */";
        private string m_pluginPath {
            get
            {
                var g = AssetDatabase.FindAssets("t:Script UGSConstGenerator");
                return Path.GetDirectoryName(AssetDatabase.GUIDToAssetPath(g[0]));
            }
        }

        [ContextMenu("Save Consts")]
        public void SaveConstCollections()
        {
            ClearFolder();
            
            m_FileTemplate = File.ReadAllText(m_pluginPath + @"\ConstFileTemplate.txt");

            foreach (var singleCollection in ConstCollections)
            {
                SaveSingleEnum(singleCollection);
            }

            AssetDatabase.Refresh();
        }

        void SaveSingleEnum(ConstCollection constCollection)
        {
            try
            {
                if (!Directory.Exists(m_pluginPath + "/Files/"))
                    Directory.CreateDirectory(m_pluginPath + "/Files/");
                
                StreamWriter streamWriter = new StreamWriter(m_pluginPath +"/Files/"+ constCollection.collectionName + ".cs");
                var resultEnumText = m_FileTemplate;
                resultEnumText = resultEnumText.Replace("{CollectionName}", constCollection.collectionName);

                foreach (var key in constCollection.collectionKeys)
                {
                    switch (key.constType)
                    {
                        case ConstType.STRING:
                            resultEnumText = resultEnumText.Replace(m_KeyPlaceholder,
                                $"public static string {key.constKey}=\"{key.constValueString}\";\n\r{m_KeyPlaceholder}");
                            break;

                        case ConstType.INT:
                            resultEnumText = resultEnumText.Replace(m_KeyPlaceholder,
                                $"public static int {key.constKey}=" + key.constValueInt + $";\n\r{m_KeyPlaceholder}");
                            break;

                        case ConstType.BOOL:
                            resultEnumText = resultEnumText.Replace(m_KeyPlaceholder,
                                $"public static bool {key.constKey}=" + key.constValueBool.ToString().ToLower() +
                                $";\n\r{m_KeyPlaceholder}");
                            break;

                        case ConstType.FLOAT:
                            resultEnumText = resultEnumText.Replace(m_KeyPlaceholder,
                                $"public static float {key.constKey}=" +
                                key.constValueFloat.ToString().Replace("/", ".") + $"f;\n\r{m_KeyPlaceholder}");
                            break;
                    }
                }

                streamWriter.Write(resultEnumText);
                streamWriter.Close();
            }
            catch (Exception e)
            {
                Debug.LogError("[UGSEnumGenerator] SaveSingleEnum: " + e.Message);
            }
        }

        public void ClearFolder()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(m_pluginPath+"/Files/");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete(); 
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true); 
            }
        }
    }
}
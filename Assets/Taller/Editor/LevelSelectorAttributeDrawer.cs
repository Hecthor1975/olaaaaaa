using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;


[CustomPropertyDrawer(typeof(LevelSelectorAttribute))]
public class LevelSelectorAttributeDrawer : PropertyDrawer
{
    // private 

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            
            EditorGUI.BeginProperty(position, label, property);

            var attrib = this.attribute as LevelSelectorAttribute;

            if (attrib.IsDefaultAttribute())
            {
                property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
            }
            else
            {
                List<string> tagList = new List<string>();
                // if (!Application.isPlaying)
                // {
                tagList.Add("NONE");

                //Debug.Log("count ? " + SceneManager.sceneCount + " " + SceneManager.sceneCountInBuildSettings);
                for (int i = 0, length = SceneManager.sceneCountInBuildSettings; i < length; i++)
                {
                    string path = SceneUtility.GetScenePathByBuildIndex(i);
                    if (path != null && path.Length > 0)
                    {
                        //Debug.Log("path" + path);
                        string sceneName = path.Substring(0, path.Length - 6);
                        sceneName = sceneName.Substring(path.LastIndexOf('/') + 1);
                        //Debug.Log("Adding [ " + i + "] = " + sceneName + " / " + length);
                        // string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
                        // Debug.Log("Adding [" + sceneName + "] ");
                        tagList.Add(sceneName);
                    }
                }
                // }
                //tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);
                string propertyString = property.stringValue;
                int index = -1;
                if (propertyString == "")
                {
                    //The tag is empty
                    index = 0; //first index is the special <notag> entry
                }
                else
                {
                    //check if there is an entry that matches the entry and get the index
                    //we skip index 0 as that is a special custom case
                    for (int i = 1; i < tagList.Count; i++)
                    {
                        if (tagList[i] == propertyString)
                        {
                            index = i;
                            break;
                        }
                    }
                }

                //Draw the popup box with the current selected index
                index = EditorGUI.Popup(position, label.text, index, tagList.ToArray());

                //Adjust the actual string value of the property based on the selection
                if (index == 0)
                {
                    property.stringValue = "";
                }
                else if (index >= 1)
                {
                    property.stringValue = tagList[index];
                }
                else
                {
                    property.stringValue = "";
                }
            }

            EditorGUI.EndProperty();
            
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }

    }

}
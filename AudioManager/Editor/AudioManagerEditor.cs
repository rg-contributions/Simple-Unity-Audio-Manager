using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// Thank god to brownboot67 for his advice
/// https://forum.unity.com/threads/custom-editor-not-saving-changes.424675/
/// </summary>
[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        AudioManager myScript = (AudioManager)target;

        string editorMessage = myScript.GetEditorMessage();
        if (editorMessage != "")
        {
            EditorGUILayout.HelpBox(editorMessage, MessageType.Info);
        }

        DrawDefaultInspector();

        List<string> options = new List<string>();

        options.Add("None");
        foreach (string s in myScript.GetMusicDictionary().Keys)
        {
            options.Add(s);
        }

        GUIContent content = new GUIContent("Current Track", "Current music that's playing, will play on start if not \"None\"");

        if (serializedObject.FindProperty("currentTrack").stringValue.Equals("")) EditorGUILayout.Popup(content, 0, options.ToArray());
        else serializedObject.FindProperty("currentTrack").stringValue = options[EditorGUILayout.Popup(content, options.IndexOf(serializedObject.FindProperty("currentTrack").stringValue), options.ToArray())];

        serializedObject.ApplyModifiedProperties();

        if (myScript.GetSoundDictionary().Count > 0)
        {
            string list = "List of Sounds:";
            foreach (string s in myScript.GetSoundDictionary().Keys)
            {
                list += "\n" + s;
            }
            EditorGUILayout.HelpBox(list, MessageType.None);
        }

        if (myScript.GetMusicDictionary().Count > 0)
        {
            string musiks = "List of Music:";
            foreach (string m in myScript.GetMusicDictionary().Keys)
            {
                musiks += "\n" + m;
            }
            EditorGUILayout.HelpBox(musiks, MessageType.None);
        }
    }   
}

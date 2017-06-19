using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(RunModeManager))]
public class RunModeManagerInspector : Editor
{
    RunModeManager RMode;

    //在这里方法中就可以绘制面板。
    public override void OnInspectorGUI()
    {
        RMode = target as RunModeManager;
        NGUIEditorTools.BeginContents();
        bool C2S_Log = EditorGUILayout.Toggle("发包C2S_Log", RMode.C2S_Log);
        bool S2C_Log = EditorGUILayout.Toggle("收包S2C_Log", RMode.S2C_Log);
        bool Log = EditorGUILayout.Toggle("普通Log", RMode.Log);
        bool LogError = EditorGUILayout.Toggle("普通LogLogError", RMode.LogError);
        if (GUI.changed)
        {
            RMode.C2S_Log = C2S_Log;
            RMode.S2C_Log = S2C_Log;
            RMode.Log = Log;
            RMode.LogError = LogError;
            EditorUtility.SetDirty(RMode);
        }
        NGUIEditorTools.EndContents();       
    }
}
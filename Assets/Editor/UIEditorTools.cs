using UnityEngine;
using UnityEditor;
using System.IO;
//---------------------------------------------------------------------------------------
// Copyright (c) 点触互动 2016-2017
// Author: Lee
// Date: 2016-6-2
// Description: 
//---------------------------------------------------------------------------------------
public class UIEditorTools: Editor
{
    public static void ExtractSpriteList(UIAtlas atlas)
    {
        var path = AssetDatabase.GetAssetPath(atlas);
        Debug.Log(path);
        var parentFolder = Path.GetDirectoryName(path);
        var folderName = Path.GetFileNameWithoutExtension(path);

        if (!Directory.Exists(parentFolder + "/" + folderName))
            AssetDatabase.CreateFolder(parentFolder, folderName);

        folderName = parentFolder + "/" + folderName + "/";

        for (var i = 0; i < atlas.spriteList.Count; i++)
        {
            var spriteData = atlas.spriteList[i];

            EditorUtility.DisplayProgressBar("Hold on", "Extract Sprite " + spriteData.name,
                1.0f*i/atlas.spriteList.Count);

            var se = UIAtlasMaker.ExtractSprite(atlas, spriteData.name);

            if (se != null)
            {
                var assetPath = folderName + spriteData.name + ".png";
                File.WriteAllBytes(assetPath, se.tex.EncodeToPNG());
                AssetDatabase.ImportAsset(assetPath);

                var textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                textureImporter.textureType = TextureImporterType.GUI;
                textureImporter.textureFormat = TextureImporterFormat.ARGB32;
                textureImporter.SaveAndReimport();

                if (se.temporaryTexture)
                    Object.DestroyImmediate(se.tex);
            }
        }

        EditorUtility.ClearProgressBar();
    }
    [MenuItem("菜单/UIAtlas/Extract Sprite List")]
    static void ExtractSprites()
    {

        Transform[] trans = Selection.GetTransforms(SelectionMode.Deep);

        for (int i = 0; i < trans.Length; i++)
        {
            Transform t = trans[i];
            UIAtlas atlas = t.GetComponent<UIAtlas>();
            ExtractSpriteList(atlas);

        }

  





        /*  
        //设置进度条  
        EditorUtility.DisplayProgressBar("设置AssetName名称", "正在设置AssetName名称中...", 0.50f);
        EditorUtility.ClearProgressBar();

        //路径  
        string fullPath = "Assets/Resources/Atlas";

        //获取指定路径下面的所有资源文件  
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            
            for (int i = 0; i < files.Length; i++)
            {

                if (files[i].Name.EndsWith(".prefab"))
                {
                    string path = files[i].DirectoryName;
                  

                    path = path.Substring(7).Replace("\\","/")+"/"+files[i].Name;

                    Debug.Log(path);
                    GameObject tmp = AssetDatabase.LoadAssetAtPath("Atlas/Game/Game.prefab", typeof(GameObject)) as GameObject;
                    UIAtlas atlas = tmp.GetComponent<UIAtlas>();
                    ExtractSpriteList(atlas);
                }
            }
        }
        */




    }


    [MenuItem("菜单/Collider/ResizeCollider")]
    static void SetCollider()
    {
        Transform[] trans = Selection.GetTransforms(SelectionMode.Deep);
        MyLogger.Log(" LLL : " + trans.Length);
        for (int i = 0; i < trans.Length; i++)
        {

            BoxCollider box = trans[i].GetComponent<BoxCollider>();
            UISprite boxSpr = trans[i].GetComponent<UISprite>();
            if (box != null && boxSpr != null)
            {
                if (box.size.x <= 60)
                {
                    box.size = new Vector3(80, 80, 0);
                    boxSpr.autoResizeBoxCollider = false;
                }
                MyLogger.Log("  " + box.size);
            }

        }
    }
    [MenuItem("菜单/Button/Add UIButtonoffset")]
    static void SetButton()
    {
        Transform[] trans = Selection.GetTransforms(SelectionMode.Deep);
        MyLogger.Log(" LLL : " + trans.Length);
        for (int i = 0; i < trans.Length; i++)
        {

            BoxCollider box = trans[i].GetComponent<BoxCollider>();
            if (box != null)
            {
                CButton cTmp = trans[i].gameObject.GetComponent<CButton>();
                if (cTmp != null)
                    DestroyImmediate(cTmp, true);
                if (box.size.x < 500)
                {
                   // PTTools.GetOrAddComp<UIButtonOffset>(trans[i].gameObject);
                }
                MyLogger.Log("  " + box.size);
            }

        }
    }


    [MenuItem("菜单/Collider/AddMask")]
    static void AddMask()
    {
        Transform[] trans = Selection.GetTransforms(SelectionMode.Deep);
        for (int i = 0; i < trans.Length; i++)
        {
            if (trans[i].name.EndsWith("indow"))
            {

                UIWidget  widget = NGUITools.AddWidget<UIWidget>(trans[i].gameObject);
                widget.depth = -7;
                widget.width = 1280;
                widget.height = 720;
                widget.autoResizeBoxCollider = true;
                widget.name = "_Mask";
                widget.SetAnchor(trans[i]);
                NGUITools.AddWidgetCollider(widget.gameObject);
                //NGUITools.AddWidgetCollider(trans[i].gameObject);
                
                MyLogger.Log("  " + trans[i].parent.name);
            }

        }
    }
    [MenuItem("菜单/NGUI/NguiMeshView")]

    static public void NguiMeshView()
    {
        foreach (var panel in UIPanel.list)
        {
            foreach (var dc in panel.drawCalls)
            {
                if (dc.gameObject.hideFlags != HideFlags.DontSave)
                {
                    dc.gameObject.hideFlags = HideFlags.DontSave;
                }
                else
                {
                    dc.gameObject.hideFlags = HideFlags.HideAndDontSave;
                }
            }
        }
    }
}
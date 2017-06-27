using UnityEngine;
using System.Collections;
using UnityEditor;

public class UIPathTool : Editor{

	[MenuItem("NGUI/FindPath")]
	static void FindPath () {

        if (Selection.gameObjects.Length > 0)
        {
            GameObject go = Selection.gameObjects[0];
            string path = FindParentPath(go);
            Debug.Log(path);
        }

	}

    static string FindParentPath(GameObject go)
    {
        string path = "";

        if (go.transform.parent != null)
        {
            path += FindParentPath(go.transform.parent.gameObject);
        }

        if (go.transform.parent == null)
        {
            path += go.name;
        }
        else
        {
            path += "/" + go.name;
        }
        

        return path;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

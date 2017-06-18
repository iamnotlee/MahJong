using UnityEngine;
using System.Collections;

public class ClockArrowItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private int cacheUid = 0;
    public void Init(int uid)
    {
        cacheUid = uid;
    }
    public void RefreshState(int uid)
    {
        gameObject.SetActive(cacheUid == uid);
    }

}

using UnityEngine;
using System.Collections;

public class MsgHintPanel : MonoBehaviour {

    public TweenAlpha tweenAlpha;
    public UILabel msgContent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Show(string content,float delayTime)
    {
        tweenAlpha.ResetToBeginning();
        tweenAlpha.duration = 1.0f;
        tweenAlpha.delay = 1.0f;
        tweenAlpha.PlayForward();
        msgContent.text = content;
        Destroy(gameObject, delayTime);
    }

}

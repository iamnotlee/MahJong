
using UnityEngine;
using System.Collections;

public class CButton : MonoBehaviour {

    public enum EEffect {
        None,
        Scale
    }

    //按钮音效
    public string AudioName;

    //按钮效果
    public EEffect Effect = EEffect.Scale;


    TweenScale mTweenScal;

    Vector3 InitScale;
	void Start () {
        InitScale = transform.localScale;
	}
	
    void OnClick () {
        //if (string.IsNullOrEmpty(AudioName))
        //    AudioManager.Instance.Audio_UIPlay("Audio/UI/");
        //else
        //    AudioManager.Instance.Audio_UIPlay("Audio/UI/");
    }

    void OnPress ( bool isPress ) {

        if (Effect == EEffect.Scale) {
            if (mTweenScal == null) {
                mTweenScal = gameObject.AddComponent<TweenScale>();
            }

            if (isPress) {
                mTweenScal.from = InitScale;
                mTweenScal.to = InitScale * 0.95f;
                mTweenScal.duration = 0.1f;
            } else {
                mTweenScal.from = transform.localScale;
                mTweenScal.to = InitScale;
                mTweenScal.animationCurve.AddKey(0.3f, 1.1f);
                mTweenScal.duration = 0.1f;
            }

            mTweenScal.ResetToBeginning();
            mTweenScal.PlayForward();
        }

    }

   
}

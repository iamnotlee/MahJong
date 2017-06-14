using UnityEngine;
using System.Collections;

public abstract class MahJongBaseLogic : MonoBehaviour {


    public Transform ThreeParent;
    public Transform GridParent;
    public Transform SingleParent;

    public  int ThreeLength = 155;
    public  int SingleLenght = 76;
    public int leftP = -525;
    public int offset = 16;
    void Start () {
	
	}
	

    public virtual void RefreshPosition()
    {
        RefreshGridPositon();
        RefreshSinglePosition();
    }

    private void RefreshGridPositon()
    {
        int count = ThreeParent.childCount;
        GridParent.localPosition = new Vector3(-525 + count * ThreeLength, 0, 0);
    }

    private void RefreshSinglePosition()
    {
        int count = ThreeParent.childCount;
        int cout = GridParent.childCount;
        SingleParent.localPosition = new Vector3(-525 + count * ThreeLength + cout * SingleLenght + 16, 0, 0);
    }
}

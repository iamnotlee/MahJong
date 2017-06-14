using UnityEngine;
using System.Collections;

public abstract class MahJongBaseLogic : MonoBehaviour {


    public enum Direction
    {
        Horizontal,
        Vertial,
    }
    public Transform ThreeParent;
    public Transform GridParent;
    public Transform SingleParent;
    public Direction direction = Direction.Horizontal;
    public  int ThreeLength = 155;
    public  int SingleLenght = 76;
    public int OriginValue = -525;
    public int Offset = 16;
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
        float valueF = OriginValue + count*ThreeLength;
        if (direction == Direction.Horizontal)
        {
            GridParent.localPosition = new Vector3(valueF, 0, 0);
        }
        else
        {
            GridParent.localPosition = new Vector3(0, valueF, 0);
        }

    }

    private void RefreshSinglePosition()
    {

        int count = ThreeParent.childCount;
        int cout = GridParent.childCount;
        float valueF = OriginValue + count*ThreeLength + cout*SingleLenght + Offset;
        if (direction == Direction.Horizontal)
        {
            SingleParent.localPosition = new Vector3(valueF, 0, 0);
        }
        else
        {
            SingleParent.localPosition = new Vector3(0, valueF, 0);
        }

       
    }
}

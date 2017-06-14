using UnityEngine;
using System.Collections;

public abstract class MahJongBaseLogic : MonoBehaviour
{


    public enum Direction
    {
        Horizontal,
        Vertial,
    }
    public enum ProgressType
    {
        Plus,
        Reduce
    }
    /// <summary>
    /// 杠、碰、吃麻将父物体
    /// </summary>
    public Transform GangMahjongParent;
    /// <summary>
    /// 手牌父物体
    /// </summary>
    public Transform HandMajongParent;
    /// <summary>
    /// 发牌父物体
    /// </summary>
    public Transform ReceiveMahjongParent;
    /// <summary>
    /// 麻将方向
    /// </summary>
    public Direction direction = Direction.Horizontal;
    public ProgressType progress = ProgressType.Plus;
    /// <summary>
    /// 杠、碰、吃牌型长度
    /// </summary>
    public int ThreeLength = 155;
    /// <summary>
    /// 单张牌型长度
    /// </summary>
    public int SingleLenght = 76;
    /// <summary>
    /// 
    /// </summary>
    public float OriginVec = -525;
    /// <summary>
    /// 偏移量
    /// </summary>
    public int Offset = 16;
    public int GangParentOff = 0;
    void Start()
    {

    }

    /// <summary>
    /// 刷新麻将位置
    /// </summary>
    public virtual void ResetMahJongPos()
    {
        ResetHandMahJongPos();
        ResetReceeveMahJongPos();
    }
    /// <summary>
    /// 刷新手牌位置
    /// </summary>
    private void ResetHandMahJongPos()
    {
        int count = GangMahjongParent.childCount;

        float valueF = 0;
        if(progress == ProgressType.Plus)
        {
            valueF = OriginVec+ count * ThreeLength;
        }
        else
        {
            valueF = OriginVec - count * ThreeLength;
        }
        if (direction == Direction.Horizontal)
        {
            HandMajongParent.localPosition = new Vector3(valueF, 0, 0);
        }
        else
        {
            HandMajongParent.localPosition = new Vector3(0, valueF, 0);
        }

    }
    /// <summary>
    /// 刷新发牌位置
    /// </summary>
    private void ResetReceeveMahJongPos()
    {

        int count = GangMahjongParent.childCount;
        int cout = HandMajongParent.childCount;
        float valueF = count * ThreeLength + cout * SingleLenght + Offset;
        if(progress == ProgressType.Plus)
        {
            valueF = OriginVec + count * ThreeLength + cout * SingleLenght + Offset;
        }
        else
        {
            valueF = OriginVec - (count * ThreeLength + cout * SingleLenght + Offset);
        }
        if (direction == Direction.Horizontal)
        {
            ReceiveMahjongParent.localPosition = new Vector3(valueF, 0, 0);
        }
        else
        {
            ReceiveMahjongParent.localPosition = new Vector3(0, valueF, 0);
        }


    }
}

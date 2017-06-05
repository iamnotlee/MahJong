using UnityEngine;
using System.Collections;



public class UIButtonSetting : MonoBehaviour
{

    public enum ButtonType
    {
        Normal,
        Close,
    }

    public bool isNeedScale = true;
    public bool isNeedChangeColor = true;
    public ButtonType buttonType;
    UISprite[] childsSpr;
    UILabel[] childLab;
    Color[] labColor;
    Color[] oriColor;
    private Color pressedColor;
    private bool isPressed;
    void Start()
    {
        pressedColor = new Color((float)158 / (float)255, (float)158 / (float)255, (float)158 / (float)255);
        SaveOriColor();
        SetNeedScale();
        SetNeedChangeColor();
        if (isNeedScale)
        {
            SetButtonScale();
        }
    }

    public void SaveOriColor()
    {
        if (childsSpr == null)
        {
            childsSpr = transform.GetComponentsInChildren<UISprite>();
        }
        if (childLab == null)
        {
            childLab = transform.GetComponentsInChildren<UILabel>();
        }
        InitColor();
    }

    void InitColor()
    {
        labColor = new Color[childLab.Length];
        for (int i = 0; i < childLab.Length; i++)
        {
            labColor[i] = childLab[i].color;
        }
        oriColor = new Color[childsSpr.Length];
        for (int i = 0; i < oriColor.Length; i++)
        {
            oriColor[i] = childsSpr[i].color;
        }
    }

    public void SaveColor(bool press)
    {
        if (press && !isPressed)
        {
            for (int i = 0; i < childLab.Length; i++)
            {
                labColor[i] = childLab[i].color;
            }
            for (int i = 0; i < oriColor.Length; i++)
            {
                oriColor[i] = childsSpr[i].color;
            }
        }
    }

    void SetNeedScale()
    {
        if (transform.parent.gameObject.GetComponent<UIGrid>() != null ||
            transform.parent.gameObject.GetComponent<UITable>() != null ||
            transform.parent.gameObject.GetComponent<UISlider>() != null)
        {
            isNeedScale = false;
        }
    }

    void SetNeedChangeColor()
    {
        if (transform.parent.gameObject.GetComponent<UIGrid>() != null ||
           transform.parent.gameObject.GetComponent<UITable>() != null ||
           gameObject.GetComponent<UIPlayTween>()
       )
        {
            isNeedChangeColor = false;
        }
    }

    void SetColor()
    {

    }

    void SetAni()
    {

    }

    void ChangeColor(bool pressed)
    {
        for (int i = 0; i < childsSpr.Length; i++)
        {
            UISprite v = childsSpr[i];
            
            if (pressed)
            {
                if (v.color.r != 0)
                    v.color = pressedColor;
            }
            else
            {
                v.color = oriColor[i];
            }
        }

        for (int i = 0; i < childLab.Length; i++)
        {
            UILabel v = childLab[i];
            if (pressed)
            {
                if (v.color.r != 0)
                    v.color = pressedColor;
            }
            else
            {
                v.color = labColor[i];
            }
        }
    }

    void SetButtonScale()
    {
        if (gameObject.GetComponent<UIButtonOffset>() == null)
        {
            gameObject.AddComponent<UIButtonOffset>();
        }
    }

    protected virtual void OnPress(bool pressed)
    {
        if (pressed)
        {
            SaveColor(pressed);          
        }      
        isPressed = pressed;
        if (isNeedChangeColor)
        {
            ChangeColor(pressed);
        }
    }
}

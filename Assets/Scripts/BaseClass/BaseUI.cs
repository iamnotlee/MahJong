



/*********************************************
** Copyright (c) BOLUO 2016-2017
** Author: slove
** Date: 2016-6-2
** Description: NPC 商店的服务器数据
*********************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseUI : MonoBehaviour
{


    /// <summary>
    /// 点击屏蔽类型
    /// </summary>
    public enum EnumMaskType
    {
        /// <summary>
        /// 没有，不屏蔽点击事件
        /// </summary>
        None,
        /// <summary>
        /// 半透明
        /// </summary>
        HalfTranslucent,
        /// <summary>
        /// 透明
        /// </summary>
        Translucent

    }
    /// <summary>
    /// UI使用到的panels
    /// </summary>
    public List<UIPanel> UiPanels;
    /// <summary>
    /// UI动画
    /// </summary>
    public GameObject UiWindow;
    /// <summary>
    /// 动画时间
    /// </summary>
    public float UiAnimationTime = 0.35f;
    /// <summary>
    /// UI点击屏蔽类型
    /// </summary>
    public EnumMaskType UiMaskType = EnumMaskType.HalfTranslucent;
    #region 缓存 gameObject & transfrom

    private Transform _cachedTransform;
    /// <summary>
    /// Gets the cached transform.
    /// </summary>
    /// <value>The cached transform.</value>
    public Transform CachedTransform
    {
        get
        {
            if (!_cachedTransform)
            {
                _cachedTransform = this.transform;
            }
            return _cachedTransform;
        }
    }

    private GameObject _cachedGameObject;
    /// <summary>
    /// Gets the cached game object.
    /// </summary>
    /// <value>The cached game object.</value>
    public GameObject CachedGameObject
    {
        get
        {
            if (!_cachedGameObject)
            {
                _cachedGameObject = this.gameObject;
            }
            return _cachedGameObject;
        }
    }
    #endregion

    #region 类型 &状态
    /// <summary>
    /// The state.
    /// </summary>
    protected EnumObjectState _State = EnumObjectState.None;

    /// <summary>
    /// Occurs when state changed.
    /// </summary>
    public event StateChangedEvent StateChanged;

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    /// <value>The state.</value>
    public EnumObjectState State
    {
        protected set
        {
            if (value != _State)
            {
                EnumObjectState oldState = _State;
                _State = value;
                if (null != StateChanged)
                {
                    StateChanged(CachedGameObject, _State, oldState);
                }
            }
        }
        get { return this._State; }
    }

    /// <summary>
    /// Gets the type of the user interface.
    /// </summary>
    /// <returns>The user interface type.</returns>
    public abstract EnumUiType GetUiType();
    ///// <summary>
    ///// 本地化
    ///// </summary>
    //public abstract void Localize();//longzhi

    #endregion

    /// <summary>
    /// 是否动态加载的
    /// </summary>
    private bool _isLoad = true;

    # region MonoBehaviour Method

    void Start()
    {
        _isLoad = false;
        OnStart();
    }

    void Awake()
    {
        this.State = EnumObjectState.Initial;
        OnAwake();
    }
    void OnEnable()
    {
        //Localize();//longzhi
        OnAcitve();
    }
    void OnDisable()
    {
        OnInActive();
    }
    void Update()
    {
        if (EnumObjectState.Ready == this._State)
        {
            OnUpdate(Time.deltaTime);
        }
    }
    #endregion

    #region 对外接口
    /// <summary>
    /// Release this Instance.
    /// </summary>
    public void Release()
    {
        this.State = EnumObjectState.Closing;
        PlayAniReverse();
        Destroy(CachedGameObject, UiAnimationTime);
        OnRelease();
    }
    public void SetUiWhenOpening(params object[] uiParams)
    {
        SetUiParams(uiParams);
        PlayAniForward();
        CoroutineController.Instance.StartCoroutine(AsyncOnLoadData());
    }
    #endregion

    #region 可以重写的方法
    protected virtual void OnStart()
    {
        MaskType();
    }

    protected virtual void OnAwake()
    {
        this.State = EnumObjectState.Loading;
        //播放音乐
        this.OnPlayOpenUiAudio();
    }

    protected virtual void OnAcitve()
    {
        if (!_isLoad)
        {
            this.OnPlayOpenUiAudio();
        }

    }
    protected virtual void OnInActive()
    {
        if (!_isLoad)
        {
            this.OnPlayCloseUiAudio();
        }
    }
    protected virtual void OnUpdate(float deltaTime)
    {

    }

    protected virtual void OnRelease()
    {
        this.OnPlayCloseUiAudio();
    }


    /// <summary>
    /// 播放打开界面音乐
    /// </summary>
    protected virtual void OnPlayOpenUiAudio()
    {

    }
    /// <summary>
    /// 播放关闭界面音乐
    /// </summary>
    protected virtual void OnPlayCloseUiAudio()
    {

    }
    protected virtual void SetUiParams(params object[] uiParams)
    {
        this.State = EnumObjectState.Loading;
    }
    protected virtual void OnLoadData()
    {

    }
    #endregion

    /// <summary>
    /// 数据加载
    /// </summary>
    /// <returns></returns>
    private IEnumerator AsyncOnLoadData()
    {
        yield return new WaitForSeconds(0);
        if (this.State == EnumObjectState.Loading)
        {
            this.OnLoadData();
            this.State = EnumObjectState.Ready;
        }
    }

    # region  触摸吞噬统一添加,动画封装
    private UITexture _mask;

    void MaskType()
    {
        if (_mask == null)
        {
            _mask = NGUITools.AddWidget<UITexture>(gameObject);
            _mask.name = "_mask";
            _mask.mainTexture = new Texture2D(2, 2);
            _mask.color = new Color(0, 0, 0, 0.5f);
            _mask.depth = -17;
            _mask.width = Screen.width * 2;
            _mask.height = Screen.height * 2;
            NGUITools.AddWidgetCollider(_mask.gameObject);
        }
        switch (UiMaskType)
        {
            case EnumMaskType.None:
                _mask.gameObject.SetActive(false);
                break;
            case EnumMaskType.HalfTranslucent:
                _mask.gameObject.SetActive(true);
                _mask.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
                break;
            case EnumMaskType.Translucent:
                _mask.gameObject.SetActive(true);
                _mask.color = new Color(0.0f, 0.0f, 0.0f, 0.01f); ;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 播放打开动画
    /// </summary>
    protected virtual void PlayAniForward()
    {
        TweenScale tt = UiWindow.GetComponent<TweenScale>();
        if (tt == null)
            tt = UiWindow.AddComponent<TweenScale>();
        tt.from = new Vector3(0.1f, 0.1f, 0.1f);
        tt.to = Vector3.one;
        tt.duration = UiAnimationTime;
        tt.animationCurve.AddKey(0.7f, 1.07f);
        //tt.method = UITweener.Method.Linear;
        tt.PlayForward();
    }
    /// <summary>
    /// 播放关闭动画
    /// </summary>
    protected virtual void PlayAniReverse()
    {
        TweenScale tt = UiWindow.GetComponent<TweenScale>();
        tt.PlayReverse();
    }
    /// <summary>
    /// 延迟隐藏
    /// </summary>
    /// <returns></returns>
    public IEnumerator WaitForHide()
    {
        PlayAniReverse();
        yield return new WaitForSeconds(UiAnimationTime);
        CachedGameObject.SetActive(false);
    }
    #endregion

    #region Editor Method 赋值panel
    [ContextMenu("Init Parameters")]
    public void InitParameters()
    {
        //if (CachedGameObject.GetComponent<UIPanel>() == null)
        //    CachedGameObject.AddComponent<UIPanel>();
        UiPanels = new List<UIPanel>(gameObject.GetComponentsInChildren<UIPanel>(true));
        UiWindow = this.transform.GetChild(0).gameObject;

        //UiPanels.Sort(SortPanels);
#if Debug
        Debug.LogError(" 获取的panel数量是： " + UiPanels.Count + " Panel对应的名字分别是： ");
        for (int i = 0; i < UiPanels.Count; i++)
        {
            Debug.LogError("" + UiPanels[i].gameObject.name);
        }
#endif
    }

    private static int SortPanels(UIPanel p1, UIPanel p2)
    {
        return p1.depth - p2.depth;
    }
    #endregion

}



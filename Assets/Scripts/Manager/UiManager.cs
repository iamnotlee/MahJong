using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// UIs 管理类
/// </summary>
public class UiManager : Singleton<UiManager>
{
    #region UI数据类
    /// <summary>
    /// UI数据类
    /// </summary>
    class UiInfoData
    {
        /// <summary>
        /// UI 类型
        /// </summary>
        /// <value></value>
        public EnumUiType UiType { get; private set; }
        /// <summary>
        /// UI路径
        /// </summary>
        /// <value></value>
        public string Path { get; private set; }
        /// <summary>
        /// 是否缓存UI
        /// </summary>
        public bool IsNeedCache { get; private set; }
        /// <summary>
        /// UI 参数
        /// </summary>
        /// <value></value>
        public object[] UiParams { get; private set; }
        public UiInfoData(EnumUiType uiType, bool isNeedCache, string path, params object[] uiParams)
        {
            this.UiType = uiType;
            this.Path = path;
            this.UiParams = uiParams;
            this.IsNeedCache = isNeedCache;
            //this.ScriptType = UIPathDefines.GetUIScriptByType(this.UIType);
        }
    }
    #endregion
    #region 变量定义
    /// <summary>
    /// 当前已经打开的UI字典
    /// </summary>
    private Dictionary<EnumUiType, GameObject> _dicOpenUIs = null;
    /// <summary>
    /// 缓存的ui字典（即：关闭的时候没有销毁）
    /// </summary>
    private Dictionary<EnumUiType, GameObject> _dicCacheUIs = null;
    /// <summary>
    /// 将要打开uis的堆数据
    /// </summary>
    private Stack<UiInfoData> _stackOpenUIs = null;
    /// <summary>
    /// 返回导航堆
    /// </summary>
    private Stack<UiInfoData> _stackBackUIs = null;
    #endregion
    #region 初始化
    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init()
    {
        _dicOpenUIs = new Dictionary<EnumUiType, GameObject>();
        _stackOpenUIs = new Stack<UiInfoData>();
        _dicCacheUIs = new Dictionary<EnumUiType, GameObject>();
        _stackBackUIs = new Stack<UiInfoData>();
    }
    #endregion

    #region  对外接口

    #region 获取已经打开的UIs的GameObject
    /// <summary>
    ///获取UIL类型
    /// </summary>
    /// <returns>The U.</returns>
    /// <param name="uiType">_ui type.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public T GetUi<T>(EnumUiType uiType) where T : BaseUI
    {
        GameObject retObj = GetUiObject(uiType);
        if (retObj != null)
        {
            return retObj.GetComponent<T>();
        }
        return null;
    }

    /// <summary>
    /// Gets the user interface object.
    /// </summary>
    /// <returns>The user interface object.</returns>
    /// <param name="uiType">_ui type.</param>
    public GameObject GetUiObject(EnumUiType uiType)
    {
        GameObject retObj = null;
        if (!_dicOpenUIs.TryGetValue(uiType, out retObj))
            throw new Exception("dicOpenUIs TryGetValue Failure! _uiType :" + uiType.ToString());
        return retObj;
    }
    #endregion
    /// <summary>
    /// ui是否已经打开
    /// </summary>
    /// <param name="uiTypes"></param>
    /// <returns></returns>
    public bool CheckIsOpening(EnumUiType[] uiTypes)
    {
        int count = 0;
        for (int i = 0; i < uiTypes.Length; i++)
        {
            if (_dicOpenUIs.ContainsKey(uiTypes[i]))
                count++;
        }
        return count > 0;
    }
    #region 打开UI

    /// <summary>
    /// 打开某以UI
    /// </summary>
    /// <param name="uiType">UI类型</param>
    /// <param name="uiObjParams">打开ui传入的数据 object parameters</param>
    public void OpenUi(EnumUiType uiType, params object[] uiParams)
    {
        EnumUiType[] uiTypes = new EnumUiType[1];
        uiTypes[0] = uiType;
        OpenUi(false, false, false, uiTypes, uiParams);
    }
    /// <summary>
    ///打开UI并且缓存
    /// </summary>
    /// <param name="uiType"></param>
    public void OpenUiAndCache(EnumUiType uiType, params object[] uiParams)
    {
        EnumUiType[] uiTypes = new EnumUiType[1];
        uiTypes[0] = uiType;
        OpenUi(false, true, false, uiTypes, uiParams);
    }
    /// <summary>
    /// 打开ui，并关闭其他UIs（当前已经打开的，即：dicOpenUI）
    /// </summary>
    /// <param name="uiType">EnumUIType</param>
    /// <param name="uiObjParams">参数</param>
    public void OpenUiCloseOthers(EnumUiType uiType, params object[] uiObjParams)
    {
        EnumUiType[] uiTypes = new EnumUiType[1];
        uiTypes[0] = uiType;
        OpenUi(true, false, false, uiTypes, uiObjParams);
    }

    /// <summary>
    /// 打开界面。
    /// </summary>同时打开多个，先传入的在最上面
    /// <param name="uiTypes">User interface types.</param>
    public void OpenUi(EnumUiType[] uiTypes)
    {
        OpenUi(false, false, false, uiTypes, null);
    }


    #endregion


    #region 关闭UI
    /// <summary>
    /// 关闭界面。
    /// </summary>
    /// <param name="uiType">类型.</param>
    public void CloseUi(EnumUiType uiType)
    {
        GameObject uiObj = null;
        if (!_dicOpenUIs.TryGetValue(uiType, out uiObj))
        {
            MyLogger.LogError("关闭失败！当前已打开的UI中没有该UI!!!, EnumUIType :" + uiType.ToString());
            return;
        }
        CloseUi(uiType, uiObj);
    }

    /// <summary>
    /// Closes the U.
    /// </summary>
    /// <param name="uiTypes">_ui types.</param>
    public void CloseUi(EnumUiType[] uiTypes)
    {
        for (int i = 0; i < uiTypes.Length; i++)
        {
            CloseUi(uiTypes[i]);
        }
    }

    /// <summary>
    /// 关闭所有UI界面
    /// </summary>
    public void CloseUiAll()
    {
        List<EnumUiType> keyList = new List<EnumUiType>(_dicOpenUIs.Keys);
        foreach (EnumUiType uiType in keyList)
        {
            GameObject uiObj = _dicOpenUIs[uiType];
            CloseUi(uiType, uiObj);
        }
        _dicOpenUIs.Clear();
    }

    #endregion

    #endregion


    #region 加载管理


    /// <summary>
    /// 打开UI
    /// </summary>
    /// <param name="isCloseOthers">是否关闭其他</param>
    /// <param name="isNeedCache">是否需要缓存</param>
    /// <param name="uiTypes">类型</param>
    /// <param name="uiParams">ui参数</param>
    private void OpenUi(bool isCloseOthers, bool isNeedCache, bool _isNeedBack, EnumUiType[] uiTypes, params object[] uiParams)
    {
        // 关闭其他UI
        if (isCloseOthers)
        {
            CloseUiAll();
        }
        // 打开ui.
        for (int i = 0; i < uiTypes.Length; i++)
        {
            EnumUiType uiType = uiTypes[i];
            if (IsCanOpenCache(uiType, uiParams))
            {
#if _Debug
                Debug.LogError(" 获取缓存UI成功!_uiType:" + uiType);
#endif
            }
            else
            {
                LoadFromMery(isNeedCache, uiType, uiParams);
            }

        }
    }
    /// <summary>
    /// 关闭UI
    /// </summary>
    /// <param name="uiType"></param>
    /// <param name="uiObj"></param>
    private void CloseUi(EnumUiType uiType, GameObject uiObj)
    {
        //Debug.Log(" EnumUIType :" + uiType.ToString());

        if (uiObj == null)
        {
            _dicOpenUIs.Remove(uiType);
        }
        else
        {
            BaseUI baseUi = uiObj.GetComponent<BaseUI>();
            if (_dicCacheUIs.ContainsKey(uiType)) // 缓存UI关闭
            {
                _dicOpenUIs.Remove(uiType);
                CoroutineController.Instance.StartCoroutine(baseUi.WaitForHide());
                //_uiObj.SetActive(false);
            }
            else
            {
                _dicOpenUIs.Remove(uiType);
                if (baseUi != null)
                {
                    baseUi.Release();
                    //_dicOpenUIs.Remove(uiType);
                }
                else
                {
                    Object.Destroy(uiObj);
                }
            }
        }
    }
    /// <summary>
    /// 是否可以从缓存中加载
    /// </summary>
    /// <param name="uiType"></param>
    /// <param name="uiParams"></param>
    /// <returns></returns>
    private bool IsCanOpenCache(EnumUiType uiType, params object[] uiParams)
    {
        GameObject uiObj = null;
        // 先判断缓存列表里有，就直接显示，没有就load
        if (_dicCacheUIs.TryGetValue(uiType, out uiObj))
        {

            if (uiObj != null)
            {
                BaseUI baseUi = uiObj.GetComponent<BaseUI>();
                if (baseUi != null)
                {
                    NormalizePanelDepths(baseUi.UiPanels);
                    _dicOpenUIs.Add(uiType, baseUi.CachedGameObject);

                    baseUi.CachedGameObject.SetActive(true);                
                    baseUi.SetUiWhenOpening(uiParams);

                    return true;
                }
            }

        }
#if Debug
        //Debug.LogError(" 没有缓存该UI!_uiType:" + uiType);
#endif

        return false;
    }
    /// <summary>
    /// 从资源中加载
    /// </summary>
    /// <param name="isNeedCache"></param>
    /// <param name="uiType"></param>
    /// <param name="uiParams"></param>
    private void LoadFromMery(bool isNeedCache, EnumUiType uiType, params object[] uiParams)
    {
        // load UIs
        if (!_dicOpenUIs.ContainsKey(uiType))
        {
            string path = uiType.ToString();
            path = path.Replace('_', '/');
            path = string.Format("UIPrefabs/{0}", path);

            // ui 数据压入堆
            _stackOpenUIs.Push(new UiInfoData(uiType, isNeedCache, path, uiParams));
        }
        else
        {
            #region Debug

            Debug.LogError("当前ui类型已经存在，不能重复添加");

            #endregion

        }
        if (_stackOpenUIs.Count > 0)
        {
            CoroutineController.Instance.StartCoroutine(AsyncLoadData(isNeedCache));
        }
    }
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    private IEnumerator<int> AsyncLoadData(bool isNeedCache)
    {
        if (_stackOpenUIs != null && _stackOpenUIs.Count > 0)
        {
            UiInfoData uiInfoData = null;
            Object prefabObj = null;
            GameObject uiObject = null;
            do
            {
                uiInfoData = _stackOpenUIs.Pop();
                prefabObj = Resources.Load(uiInfoData.Path);
                if (prefabObj != null)
                     {
                         uiObject = Object.Instantiate(prefabObj) as GameObject;
                         if (uiObject != null)
                         {
                          
                             // 设置ui父物体
                             SetUIParent(isNeedCache, uiObject.transform);
                             BaseUI baseUi = uiObject.GetComponent<BaseUI>();
                             if (null != baseUi)
                             {
                                 NormalizePanelDepths(baseUi.UiPanels);
                                 _dicOpenUIs.Add(uiInfoData.UiType, uiObject);
                                 baseUi.SetUiWhenOpening(uiInfoData.UiParams);
                             }

                             // 如果需要缓存
                             if (isNeedCache)
                             {
                                 _dicCacheUIs.Add(uiInfoData.UiType, uiObject);
                             }
                         }
                     }
                     else
                     {
                               Debug.LogError("加载失败");
                     }
                
            } while (_stackOpenUIs.Count > 0);
        }
        yield return 0;
    }
    /// <summary>
    /// 设置ui父物体
    /// </summary>
    /// <param name="isCanCache"></param>
    /// <param name="uiTrans"></param>
    private void SetUIParent(bool isCanCache, Transform uiTrans)
    {
        if (!isCanCache)
        {
            if (UIsContainer.Instance.NormalUIParent != null)
                uiTrans.parent = UIsContainer.Instance.NormalUIParent;
        }
        else
            uiTrans.parent = UIsContainer.Instance.CacheUIParent;
        uiTrans.localScale = Vector3.one;
    }

    #endregion

    #region NGUI Depth 动态排序
    /// <summary>
    /// 获取panel的最处值
    /// </summary>
    /// <returns></returns>
    private int GetPanelStartDepth()
    {
        int startDepth = 100;
        if (_dicOpenUIs == null || _dicOpenUIs.Count <= 0)
        {
            startDepth = 100;
#if Debug
            //Debug.LogError(" 当前打开的UI个数为0！StartDepth = " + startDepth);
#endif
        }
        else
        {
            List<GameObject> openUIs = new List<GameObject>(_dicOpenUIs.Values);
            int lastObjIndex = openUIs.Count - 1;
            BaseUI baseUi = openUIs[lastObjIndex].GetComponent<BaseUI>();
            int lastPanelIndex = baseUi.UiPanels.Count - 1;
            startDepth = baseUi.UiPanels[lastPanelIndex].depth;
#if Debug
            Debug.LogError(" 当前打开的UI个数为0！StartDepth = " + startDepth);
#endif
        }
        return startDepth;
    }
    /// <summary>
    /// uipanels排序
    /// </summary>
    /// <param name="panels"></param>
    private void NormalizePanelDepths(List<UIPanel> panels)
    {
        if (panels.Count == 0)
        {
#if _Debug
            Debug.LogError(" UI'spanel数组是0，设置Depth失败");
#endif
        }
        else
        {
            int startDepth = GetPanelStartDepth();

            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].depth = startDepth + (i + 1) * 3;
            }
        }

    }
    #endregion

}





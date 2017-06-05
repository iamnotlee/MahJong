using UnityEngine;
using System.Collections;

public class MahJongModel : Singleton<MahJongModel> {

	
    public void RegistCallBack()
    {
        //CDataListManager.instance.RegisterDelegate<RPCreateRoom>(CreateRoom);

    }
    void CreateRoom(PB_BaseData baseData)
    {
        //RPCreateRoom rp = baseData.GetObj<RPCreateRoom>();
        //if (rp != null)
        //{
        //    //28682766
        //    UiManager.Instance.CloseUi(EnumUiType.Room_CreateRoomUI);
        //    UnityEngine.SceneManagement.SceneManager.LoadScene("game");
        //    Debug.Log(rp.roomId);
        //}
    }

}

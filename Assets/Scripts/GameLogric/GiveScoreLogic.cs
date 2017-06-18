using UnityEngine;
using System.Collections;

public class GiveScoreLogic : MonoBehaviour {

    public GameObject[] ScoreBtns;
	void Start () {
        for (int i = 0; i < ScoreBtns.Length; i++)
        {
            UIEventListener.Get(ScoreBtns[i]).onClick = delegate (GameObject go)
            {
                string[] strs = go.name.Split('_');
                int score = 1;
                if (strs.Length >= 2)
                {
                    score = GameUtils.StringToInt(strs[0]);
                    MahJongModel.Instance.RqScore(score);
                }
            };
        }
	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdiomListObj : MonoBehaviour
{
    public GameManagement gameManagement;
    /// <summary>
    /// 成语物体列表
    /// </summary>
    public List<GameObject> idiomObjList;
    /// <summary>
    /// 成语文本列表
    /// </summary>
    public List<Text> idiomTextList;
    /// <summary>
    /// 倒计时
    /// </summary>
    public int time;
    void Update()
    {
        /*五秒后清除重置*/
        if(time > 0)
        {
            time--;
            CheckIdiom();
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                idiomTextList[i].text = "";
            }
        }
        for (int i = 0; i<4; i++)
        {
            if(idiomTextList[i].text.Length == 0)
            {
                idiomObjList[i].SetActive(false);
            }
            else
            {
                idiomObjList[i].SetActive(true);
            }
        }
    }
    public void AddIdiom(string idiom)
    {
        time = 300;
        for (int i = 0; i < 4; i++)
        {
            if (idiomTextList[i].text.Length == 0)
            {
                idiomTextList[i].text = idiom;
                return;
            }
        }
    }
    public void CheckIdiom()
    {
        string allIdiomStr = "";
        for (int i = 0; i < 4; i++)
        {
            allIdiomStr = allIdiomStr + idiomTextList[i].text;
        }
        switch (allIdiomStr)
        {
            case "火上浇油":
                gameManagement.skillManagement.AddFuelToTheFlames(gameManagement.playerController.gameObject);
                time = 0;
                break;
        }
    }
}

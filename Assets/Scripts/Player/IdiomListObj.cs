using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdiomListObj : MonoBehaviour
{
    public GameManagement gameManagement;
    /// <summary>
    /// ���������б�
    /// </summary>
    public List<GameObject> idiomObjList;
    /// <summary>
    /// �����ı��б�
    /// </summary>
    public List<Text> idiomTextList;
    /// <summary>
    /// ����ʱ
    /// </summary>
    public int time;
    void Update()
    {
        /*������������*/
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
            case "���Ͻ���":
                gameManagement.skillManagement.AddFuelToTheFlames(gameManagement.playerController.gameObject);
                time = 0;
                break;
        }
    }
}
